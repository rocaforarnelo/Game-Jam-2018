using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircle : SkillAction {
	public SpellCircleNode[] SpellCircleNodes;
	public int CurrentNodeIndex;
	
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			Reset ();
		}
	}

	public override void Reset ()
	{
		base.Reset ();
		CurrentNodeIndex = 0;
		for (int i = 0; i < SpellCircleNodes.Length; i++) {
			SpellCircleNodes[i].Reset ();
		}
	}

	public void SetSpellCircleNodeDone()
	{
		CurrentNodeIndex++;
		if (CurrentNodeIndex == SpellCircleNodes.Length) {
			SetDone ();
		}
	}
}
