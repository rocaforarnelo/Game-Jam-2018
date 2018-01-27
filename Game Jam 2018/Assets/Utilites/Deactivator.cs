using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour {

	public void DeactivateSelf()
	{
		gameObject.SetActive (false);	
	}
}
