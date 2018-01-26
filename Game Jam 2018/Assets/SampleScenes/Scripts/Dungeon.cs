using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Dungeon : NetworkBehaviour {
	[SyncVar]
	public int CurrentRoomIndex;
	static public Dungeon sInstance = null;

	void Awake()
	{
		sInstance = this;
	}

	public void Initialize()
	{
		GetRoomInstance ();
	}

	private int GetRoomIndex()
	{
		return Random.Range (0, NetworkGameManager.instance.RoomPrefabs.Length);
	}

	private Room GetRoomInstance()
	{
		Room currentRoomInstance = Instantiate<Room> (NetworkGameManager.instance.RoomPrefabs [GetRoomIndex()], Vector2.zero, Quaternion.identity);
		currentRoomInstance.transform.SetParent (this.gameObject.transform);
		currentRoomInstance.Dungeon = this;
		NetworkServer.Spawn (currentRoomInstance.gameObject);
		return currentRoomInstance;
	}

	public void SetRoomDone()
	{
		CurrentRoomIndex++;
		NetworkGameManager.instance.ResetRoom ();
		Initialize ();
	}
}
