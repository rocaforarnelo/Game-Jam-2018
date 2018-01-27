using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;

public class NetworkGameManager : NetworkBehaviour
{
	public GameObject DungeonEntrance;
	private const int roomsPerLevel = 5;
	[SyncVar(hook = "OnLevelChange")]
	public int Level = 1;
	[SyncVar(hook = "OnHpChanged")]
	public int PlayerHp;
	public Text LevelValueLabel, PlayerHpValueLabel, DungeonEntranceLevelValueLabel;
	public Image PlayerHpGuage;
	public GameObject GameOverPanel, ScratchAnimation;
	public Room[] RoomPrefabs;
	public Room[] BossRoomPrefabs;
	public EnemyCanvas EnemyCanvas;
	public Dungeon Dungeon;
	public bool IsTest;
	private int maxHp = 5;
	static public List<NetworkPlayerCharacter> sNetworkPlayerCharacters = new List<NetworkPlayerCharacter>();
    static public NetworkGameManager instance = null;
	[SyncVar]
	public bool EnteredDungeon;

    void Awake()
    {
		instance = this;
    }
		
    void Start()
    {
		if (isServer) {
			Initialize ();
		}
		DungeonEntrance.SetActive (true);
		//EnteredDungeon = false;
		for (int i = 0; i < sNetworkPlayerCharacters.Count; i++) {
			sNetworkPlayerCharacters [i].Init ();
		}
		InitializeLabels ();
    }
	[ClientRpc]
	public void RpcOpenDungeon()
	{
		DungeonEntrance.SetActive (true);
		EnteredDungeon = false;
		DungeonEntranceLevelValueLabel.gameObject.SetActive (true);
	}

	[ClientRpc]
	public void RpcLevelUp()
	{
		Initialize ();
		Level++;
		RpcOpenDungeon ();
	}

	void OnLevelChange(int newLevel)
	{
		Level = newLevel;
		LevelValueLabel.text = Level.ToString ();
		DungeonEntranceLevelValueLabel.text = Level.ToString ();
		if (isServer) {
			RpcOpenDungeon ();
		}
	}

	public void EnterDungeon()
	{
		DungeonEntrance.gameObject.SetActive (false);
		EnteredDungeon = true;
	}

	private void Initialize()
	{
		if (isServer) {
			Dungeon.Initialize ();
		}
	}

	private void InitializeLabels()
	{
		SetPlayerHpValueLabel ();
		SetLevelValueLabel ();
	}

	[Server]
	public void Damage()
	{
		PlayerHp--;
	}

	void OnHpChanged(int newValue)
	{
		PlayerHp = newValue;
		PlayerHpGuage.fillAmount = (float)PlayerHp / (float)maxHp;
		ScratchAnimation.gameObject.SetActive (true);
		InitializeLabels ();
		CheckIfGameOver ();
	}

	private void SetLevelValueLabel()
	{
		LevelValueLabel.text = Level.ToString ();
	}

	private void AddLevel()
	{
		Level++;
		SetLevelValueLabel ();
	}

	private void SetPlayerHpValueLabel()
	{
		PlayerHpValueLabel.text = PlayerHp.ToString ();
	}

	[Server]
	public void Damage(int value)
	{
		PlayerHp -= value;	}

	private void CheckIfGameOver()
	{
		if(PlayerHp <= 0)
		{
			PlayerHp = 0;
			RpcGameOver ();
		}
	}
	[ClientRpc]
	public void RpcGameOver()
	{
		GameOverPanel.SetActive (true);
		if (isServer) {
			ReturnToLobby ();
		}
		//StartCoroutine (ReturnToLobyCoroutine ());
	}

    [ServerCallback]
    void Update()
    {
		if(sNetworkPlayerCharacters.Count == 0 || !NetworkServer.active)
        {
			ReturnToLobby ();
		}
    }

	public void ReturnToLobby()
	{
		LobbyManager.s_Singleton.ServerReturnToLobby();
	}

    public override void OnStartClient()
    {
        base.OnStartClient();

        foreach (Room obj in RoomPrefabs)
        {
            ClientScene.RegisterPrefab(obj.gameObject);
        }
		foreach (Room obj in BossRoomPrefabs) {
			ClientScene.RegisterPrefab (obj.gameObject);
		}
    }

    IEnumerator ReturnToLobyCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        LobbyManager.s_Singleton.ServerReturnToLobby();
    }

    public IEnumerator WaitForRespawn(NetworkPlayerCharacter playerCharacter)
    {
        yield return new WaitForSeconds(4.0f);

        playerCharacter.Respawn();
    }

	public void ResetRoom()
	{
		EnemyCanvas.Reset ();	
	}
		
	public int GetRoomCount()
	{
		return Level * roomsPerLevel;
	}

	public static int GetCommanderCharacterIndex(int characterIndex)
	{
		int ret = characterIndex + 1;
		if (characterIndex == sNetworkPlayerCharacters.Count - 1) {
			ret = 0;
		}
		return ret;
	}

	public void ResetPlayers()
	{
		for (int i = 0; i < sNetworkPlayerCharacters.Count; i++) {
			sNetworkPlayerCharacters [i].SetDonePanelActive (false);
		}
	}
}
