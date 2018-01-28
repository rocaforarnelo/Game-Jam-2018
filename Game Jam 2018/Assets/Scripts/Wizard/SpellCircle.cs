using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircle : SkillAction {
	private readonly Vector2 scale = new Vector2(8.0f, 8.0f);
	public SpellCircleNode[] SpellCircleNodes;
	public int CurrentNodeIndex;
	public GameObject EndAnimation;
	
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

	public override void SetDone ()
	{
		InstantiateEndAnimation ();
		base.SetDone ();
	}

	private void InstantiateEndAnimation()
	{
		GameObject endAnimationInstance = Instantiate (EndAnimation, transform.position, Quaternion.identity) as GameObject;
		endAnimationInstance.transform.localScale = scale;
	}
}
