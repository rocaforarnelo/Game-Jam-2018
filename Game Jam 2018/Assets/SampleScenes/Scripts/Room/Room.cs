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

	public virtual void RpcInitialize()
	{
		
	}

	[ClientRpc]
	public virtual void RpcSetDone()
	{
		if (isServer) {
			Dungeon.RpcSetRoomDone ();
			NetworkServer.Destroy(gameObject);
		}
	}
}
