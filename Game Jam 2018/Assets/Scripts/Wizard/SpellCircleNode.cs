using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCircleNode : MonoBehaviour {
	private readonly string[] romanNumerals = new string[]{
		"I", "II", "III", "IV", "V", "VI"
	};
	public SpellCircle SpellCircle;
	public SpriteRenderer SpriteRenderer;
	public Text IndexLabel;
	public int Index;
	public bool IsDone;

	void Start()
	{
		IndexLabel.text = romanNumerals[Index];
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
	}

	public void SetDone()
	{
		IndexLabel.color = Color.black;
		IsDone = true;
	}

	public void Reset()
	{
		IsDone = false;
		IndexLabel.color = Color.white;	
	}
		
	public void SetCurrentNode()
	{
		IndexLabel.color = Color.green;	
	}
}
