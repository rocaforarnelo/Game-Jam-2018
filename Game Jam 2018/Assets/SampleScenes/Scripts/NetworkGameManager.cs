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
	[SyncVar]
	public int Level = 1;
	[SyncVar(hook = "OnHpChanged")]
	public int PlayerHp;
	public Text LevelValueLabel, PlayerHpValueLabel;
	public GameObject GameOverPanel;
	public Room[] RoomPrefabs;
	public EnemyCanvas EnemyCanvas;
	public Dungeon Dungeon;
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
		Initialize ();
		DungeonEntrance.SetActive (true);
		for (int i = 0; i < sNetworkPlayerCharacters.Count; i++) {
			sNetworkPlayerCharacters [i].Init ();
		}
		InitializeLabels ();
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
		InitializeLabels ();
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
		PlayerHp -= value;
		CheckIfGameOver ();
	}

	private void CheckIfGameOver()
	{
		if(PlayerHp <= 0)
		{
			PlayerHp = 0;
			SetGameOver ();
		}
	}
	[Server]
	public void SetGameOver()
	{
		GameOverPanel.SetActive (true);	
	}

    [ServerCallback]
    void Update()
    {
		if(sNetworkPlayerCharacters.Count == 0 || !NetworkServer.active)
        {
            StartCoroutine(ReturnToLoby());
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        foreach (Room obj in RoomPrefabs)
        {
            ClientScene.RegisterPrefab(obj.gameObject);
        }
    }

    IEnumerator ReturnToLoby()
    {
        yield return new WaitForSeconds(3.0f);
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
