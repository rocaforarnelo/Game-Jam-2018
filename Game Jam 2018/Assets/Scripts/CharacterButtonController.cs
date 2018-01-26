using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButtonController : MonoBehaviour {
	public ExamplePlayerScript localPlayer;

	public void SetLocalPlayer(ExamplePlayerScript localPlayer)
	{
		this.localPlayer = localPlayer;
	}
}
