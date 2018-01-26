using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkPlayerCharacter : NetworkBehaviour
{
    [SyncVar]
    public int characterIndex;
    [SyncVar]
    public string playerName;
	[SyncVar (hook = "OnActionValueChange")]
	public string ActionString;
	[SyncVar]
	public int AssignedSkillIndex;
	public GameObject[] UIFields;
	public GameObject DonePanel;
	public PlayerCharacter PlayerCharacter;
    protected bool _canControl = true;
    protected bool _wasInit = false;

    void Awake()
    {
        NetworkGameManager.sNetworkPlayerCharacters.Add(this);
    }

    void Start()
    {
		
    }

	[Command]
	public void CmdDecreaseEnemyCanvasLife()
	{
		EnemyCanvas.sInstance.DecreaseActionLife();
	}

	public void SetDonePanelActive(bool active)
	{
		DonePanel.SetActive (active);

	}

	public void SetDone()
	{
		SetDonePanelActive (true);
	}
	[Command]
	public void CmdAssignSkill(int index)
	{
		AssignedSkillIndex = index;
		int commanderIndex = NetworkGameManager.GetCommanderCharacterIndex (characterIndex);
		NetworkPlayerCharacter commanderNetworkPlayerCharacter = NetworkGameManager.sNetworkPlayerCharacters [0];
		for (int i = 1; i < NetworkGameManager.sNetworkPlayerCharacters.Count; i++) {
			if (NetworkGameManager.sNetworkPlayerCharacters [i].characterIndex == commanderIndex) {
				commanderNetworkPlayerCharacter = NetworkGameManager.sNetworkPlayerCharacters [i];
			}
		}
		commanderNetworkPlayerCharacter.CommandSkill (GetCommandString(characterIndex,index));
	}

	public string GetCommandString(int playerCharacterIndex, int skillIndex)
	{
		return "Tell " + NameConstants.CharacterNames [playerCharacterIndex] + " to use " + NameConstants.SkillNames[playerCharacterIndex][skillIndex];
	}

	public void OnActionValueChange(string newValue)
	{
		if (!isLocalPlayer)
			return;
		ActionString = newValue;
		PlayerCharacter.SetActionValueLabel (ActionString);
	}

	public void CommandSkill(string command)
	{
		ActionString = command;
	}

	private void InstantiateUIFields()
	{
		if (!isLocalPlayer)
			return;
		GameObject uiFields = UIFields[characterIndex];
		uiFields.gameObject.SetActive (true);
		PlayerCharacter = uiFields.GetComponent<PlayerCharacter> ();
		uiFields.transform.SetParent (transform);
	}

    public void Init()
    {
        if (_wasInit)
            return;
        _wasInit = true;
		InstantiateUIFields ();
    }

    void OnDestroy()
    {
        NetworkGameManager.sNetworkPlayerCharacters.Remove(this);
    }

    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer || !_canControl)
            return;
		DetectEnterDungeon();
    }

	private void DetectEnterDungeon()
	{
		if (NetworkGameManager.instance == null)
			return;
		if(!NetworkGameManager.instance.EnteredDungeon && Input.GetMouseButtonDown (0)) {
			EnterDungeon ();
		}
	}


    [ClientCallback]
    void FixedUpdate()
    {
        if (!hasAuthority)
            return;
    }

    [ClientCallback]
    void OnCollisionEnter(Collision coll)
    {
        if (isServer)
            return; // hosting client, server path will handle collision

    }

    [Client]
    public void LocalDestroy()
    {
        if (!_canControl)
            return;//already destroyed, happen if destroyed Locally, Rpc will call that later
	    }

    //this tell the game this should ONLY be called on server, will ignore call on client & produce a warning
    [Server]
    public void Kill()
    {
        RpcDestroyed();
    }

    [Server]
    public void Respawn()
    {
        //EnableSpaceShip(true);
        RpcRespawn();
    }
    // =========== NETWORK FUNCTIONS

    [Command]
    public void CmdFire(Vector3 position, Vector3 forward, Vector3 startingVelocity)
    {
        if (!isClient) //avoid to create bullet twice (here & in Rpc call) on hosting client

        RpcFire();
    }

    //
    [Command]
    public void CmdCollideAsteroid()
    {
        Kill();
    }

    [ClientRpc]
    public void RpcFire()
    {
    }

    [ClientRpc]
    void RpcDestroyed()
    {
        LocalDestroy();
    }

    [ClientRpc]
    void RpcRespawn()
    {
    }

	public void EnterDungeon()
	{
		if (isServer) {
			RpcEnterDungeon ();
		} else {
			CmdEnterDungeon ();
		}
	}

	[Command]
	public void CmdEnterDungeon()
	{
		RpcEnterDungeon ();
	}

	[ClientRpc]
	public void RpcEnterDungeon()
	{
		NetworkGameManager.instance.EnterDungeon ();
		Room.instance.Initialize ();
	}
}
