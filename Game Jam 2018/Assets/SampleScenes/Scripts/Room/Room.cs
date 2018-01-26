using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Room : NetworkBehaviour {
	public Dungeon Dungeon;
	static public Room instance = null;

	void Awake()
	{
		instance = this;
	}

	public virtual void Initialize()
	{
		
	}

	[ClientRpc]
	public virtual void RpcSetDone()
	{
		Dungeon.SetRoomDone ();
		NetworkServer.Destroy(gameObject);
	}
}
