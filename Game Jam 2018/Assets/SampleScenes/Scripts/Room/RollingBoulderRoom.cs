using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RollingBoulderRoom : Room {
	private const float duration = 5.0f;
	[SyncVar (hook = "OnChangeTimerFillAmount")]
	public float TimerGuageFillAmount;
	public Image TimerGuage;

	public override void Initialize ()
	{
		base.Initialize ();
		StartCoroutine (TimerCoroutine ());
	}

	public void OnChangeTimerFillAmount(float newValue)
	{
		TimerGuageFillAmount = newValue;
		TimerGuage.fillAmount = TimerGuageFillAmount;
	}

	IEnumerator TimerCoroutine()
	{
		float startTime = Time.deltaTime;

		while (Time.time < startTime + duration) {
			TimerGuageFillAmount = 1.0f - ((Time.time - startTime) / duration);
			if (Input.GetMouseButton (0)) {
				RpcSetDone ();	
			}
			yield return null;
		}
		NetworkGameManager.instance.Damage ();
	}
}
