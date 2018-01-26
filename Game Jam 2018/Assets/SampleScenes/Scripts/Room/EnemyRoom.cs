using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyRoom : Room {
	public int HpPerLevel;
	public Sprite Icon;
	public string EnemyName;
	[SyncVar (hook = "OnHpChange")]
	public int Hp;
	
	public override void Initialize ()
	{
		base.Initialize ();
		Hp = HpPerLevel * NetworkGameManager.instance.Level;
		EnemyCanvas.sInstance.Initialize (this);
	}

	void OnHpChange(int newValue)
	{
		Hp = newValue;
		EnemyCanvas.sInstance.InitializeHpValueLabel (Hp);
	}

	private void CheckDead()
	{
		if (Hp == 0) {
			RpcSetDone ();
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