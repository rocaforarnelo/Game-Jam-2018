using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCircleNode : MonoBehaviour {
	public SpellCircle SpellCircle;
	public SpriteRenderer SpriteRenderer;
	public Text IndexLabel;
	public int Index;
	public bool IsDone;

	void Start()
	{
		IndexLabel.text = Index.ToString();
	}

	void OnMouseEnter()
	{
		if (IsDone)
			return;
		CheckIfCurrentNode ();
	}

	private void CheckIfCurrentNode()
	{
		Debug.Log ("Check");
		if (Index == SpellCircle.CurrentNodeIndex) {
			SetDone ();
			SpellCircle.SetSpellCircleNodeDone ();
		}
		else
		{
			SpellCircle.Reset ();
		}
	}

	public void SetDone()
	{
		SpriteRenderer.color = Color.green;
		IsDone = true;
	}

	public void Reset()
	{
		IsDone = false;
		SpriteRenderer.color = Color.white;	
	}
		
	public void SetCurrentNode()
	{
		SpriteRenderer.color = Color.red;
	}
}
