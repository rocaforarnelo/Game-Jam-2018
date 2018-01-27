using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Dungeon : NetworkBehaviour {
	private const int DungeonLife = 1;
	[SyncVar]
	public int CurrentRoomIndex;
	static public Dungeon sInstance = null;

	void Awake()
	{
		sInstance = this;
	}
	[Server]
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
		Debug.Log ("Instantiate");
		Room[] roomPrefabs = GetRoomPrefabs ();
		Room currentRoomInstance = Instantiate<Room> (roomPrefabs[GetRoomIndex()], Vector2.zero, Quaternion.identity);
		currentRoomInstance.transform.SetParent (this.gameObject.transform);
		currentRoomInstance.Dungeon = this;
		Debug.Log (currentRoomInstance.gameObject);
		NetworkServer.Spawn (currentRoomInstance.gameObject);
		return currentRoomInstance;
	}
	public Room[] GetRoomPrefabs()
	{
		if (CurrentRoomIndex == DungeonLife) {
			return NetworkGameManager.instance.BossRoomPrefabs;
		}
		return NetworkGameManager.instance.RoomPrefabs;
	}

	[ClientRpc]
	public void RpcSetRoomDone()
	{
		CurrentRoomIndex++;
		NetworkGameManager.instance.ResetRoom ();
		if (CurrentRoomIndex == DungeonLife + 1) {
			CurrentRoomIndex = 0;
			if (isServer) {
				NetworkGameManager.instance.RpcLevelUp ();
			}
		} else {
			if (isServer) {
				Initialize ();
			}
			Room.instance.RpcInitialize ();
		}
	}
}
