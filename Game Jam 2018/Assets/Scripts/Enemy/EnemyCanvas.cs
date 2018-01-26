using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemyCanvas : NetworkBehaviour {
	private const float actionDuration = 100;
	public Image EnemyIcon, HpGuage, TimerGuage, EnemyBlock;
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
	private IEnumerator actionTimerCoroutine;

	void Awake()
	{
		sInstance = this;
	}

	public void Initialize(EnemyRoom enemyRoom)
	{
		EnemyIcon.sprite = enemyRoom.Icon;
		EnemyBlock.gameObject.SetActive (false);
		InitializeLabels (enemyRoom);
		InitializeImages ();
		RpcActionTimerCaller ();
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
		StopActionTimer ();
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
		actionTimerCoroutine = ActionTimer ();
		StartCoroutine (actionTimerCoroutine);
		AssignSkills ();
	}

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
		RpcActionTimerCaller ();
	}

	private void InitializeLabels(EnemyRoom enemyRoom)
	{
		EnemyNameValueLabel.text = enemyRoom.EnemyName;
		HpValueLabel.text = enemyRoom.Hp.ToString();

	}

	public void InitializeHpValueLabel(int hp)
	{
		HpValueLabel.text = hp.ToString ();
	}

	public void Reset()
	{
		EnemyBlock.gameObject.SetActive (true);
	}
}
