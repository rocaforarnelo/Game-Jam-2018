using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyRoom : Room {
	public int HpPerLevel;
	public string EnemyName;
	[SyncVar (hook = "OnHpChange")]
	public int Hp;
//	[SyncVar]
//	private int maxHp;

	[ClientRpc]
	public override void RpcInitialize ()
	{
		base.RpcInitialize ();
		Hp = HpPerLevel * NetworkGameManager.instance.Level;
		//maxHp = Hp;
		EnemyCanvas.sInstance.Initialize (this);
	}

	void OnHpChange(int newValue)
	{
		Hp = newValue;
		if (isServer) {
			EnemyCanvas.sInstance.RpcInitializeHpValueLabel (Hp);
		}
		CheckDead ();
	}

	private void CheckDead()
	{
		if (Hp == 0) {
			if (isServer) {
				RpcSetDone ();
			}
		}
	}

	public void Damage()
	{
		Hp--;
	}

	[Server]
	public void Attack()
	{
		NetworkGameManager.instance.Damage ();
	}
}