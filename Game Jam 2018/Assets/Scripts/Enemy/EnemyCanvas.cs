using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemyCanvas : NetworkBehaviour {
	private const float actionDuration = 10;
	public Image EnemyIcon, HpGuage, TimerGuage, EnemyBlock, EnemySprite;
	public Text EnemyNameValueLabel, HpValueLabel, EnemyActionValueLabel, ActionLifeValueLabel;
	static public EnemyCanvas sInstance = null;
	[SyncVar]
	public bool Attacking;
	[SyncVar(hook = "OnActionLifeChange")]
	public int ActionLife;
	[SyncVar (hook = "OnChangeTimerFillAmount")]
	public float TimerGuageFillAmount;
	[SyncVar (hook = "OnChangeEnemyActionString")]
	public string EnemyActionString;
	[SyncVar (hook = "OnChangeEnemySpriteString")]
	public string EnemySpriteName;
	private IEnumerator actionTimerCoroutine;

	void Awake()
	{
		sInstance = this;
	}

	public void Initialize(EnemyRoom enemyRoom)
	{
		if (isServer) {
			RpcChangeSprite (enemyRoom.EnemyName);
		}
		Attacking = true;
		EnemyBlock.gameObject.SetActive (false);
		InitializeLabels (enemyRoom);
		InitializeImages ();
		if (isServer) {
			RpcActionTimerCaller ();
		}
	}

	public void OnChangeEnemySpriteString(string newValue)
	{
		EnemySpriteName = newValue;
		Sprite enemySprite = Resources.Load<Sprite> (EnemySpriteName);
		EnemyIcon.sprite = enemySprite;
		EnemySprite.sprite = enemySprite;
	}

	public void RpcChangeSprite(string spriteName)
	{
		EnemySpriteName = spriteName;
	}

	public void OnChangeTimerFillAmount(float newValue)
	{
		TimerGuageFillAmount = newValue;
		TimerGuage.fillAmount = TimerGuageFillAmount;
	}

	public void OnChangeEnemyActionString(string newValue)
	{
		EnemyActionString = newValue;
		EnemyActionValueLabel.text = EnemyActionString;
	}

	public void DecreaseActionLife()
	{
		if (!isServer)
			return;
		ActionLife--;
	}

	public void OnActionLifeChange(int newValue)
	{
		ActionLife = newValue;
		ActionLifeValueLabel.text = ActionLife.ToString ();
		if (ActionLife == 0) {
			ActionSuccesful ();
		}
	}

	public void ActionSuccesful()
	{
		NetworkGameManager.instance.ResetPlayers ();
		if (isServer) {
			StopActionTimer ();
		}
		if (!Attacking) {
			(EnemyRoom.instance as EnemyRoom).Damage ();
		} 
		EndAction ();
	}

	private void InitializeImages()
	{
		TimerGuageFillAmount = 1.0f;
	}
	[Server]
	public void AssignSkills()
	{
		List<NetworkPlayerCharacter> networkPlayerCharacters = NetworkGameManager.sNetworkPlayerCharacters;
		for (int i = 0; i < networkPlayerCharacters.Count; i++) {
			int randomSkillIndex = Random.Range (0, PlayerCharacter.SkillCount);
			networkPlayerCharacters[i].CmdAssignSkill (randomSkillIndex);
		}
	}
	[ClientRpc]
	public void RpcActionTimerCaller()
	{
		NetworkGameManager.instance.ResetPlayers ();
		ActionLife = NetworkGameManager.sNetworkPlayerCharacters.Count;
		EnemyActionString = GetEnemyActionString ();
		if (isServer) {
			StartTimer ();
		}
		AssignSkills ();
	}
	[Server]
	private void StartTimer()
	{
		actionTimerCoroutine = ActionTimer ();
		StartCoroutine (actionTimerCoroutine);
	}

	[Server]
	public void StopActionTimer()
	{
		if (actionTimerCoroutine != null) {
			StopCoroutine (actionTimerCoroutine);
		}
	}

	IEnumerator ActionTimer()
	{
		float startTime = Time.time;

		while (Time.time < startTime + actionDuration) {
			TimerGuageFillAmount = 1.0f - ((Time.time - startTime) / actionDuration);
			yield return null;
		}
		EndTimerAction ();
	}

	private string GetEnemyActionString()
	{
		if (Attacking) {
			return EnemyNameValueLabel.text + " is ATTACKING";
		} else {
			return EnemyNameValueLabel.text +	 " is DEFENDING";
		}
	}

	public void EndTimerAction()
	{
		if (Attacking) {
			(Room.instance as EnemyRoom).Attack ();
		}
		EndAction ();
	}

	public void EndAction()
	{
		Attacking = !Attacking;
		if (isServer) {
			RpcActionTimerCaller ();
		}
	}

	private void InitializeLabels(EnemyRoom enemyRoom)
	{
		EnemyNameValueLabel.text = enemyRoom.EnemyName;
		HpValueLabel.text = enemyRoom.Hp.ToString();

	}
	[ClientRpc]
	public void RpcInitializeHpValueLabel(int hp)
	{
		HpValueLabel.text = hp.ToString ();
	}

	public void Reset()
	{
		EnemyBlock.gameObject.SetActive (true);
	}
}
