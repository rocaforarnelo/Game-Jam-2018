using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillActionPanel : MonoBehaviour {
	public Text Name;
	public Image Panel;

	public void Initialize(SkillAction skillAction)
	{
		Name.text = skillAction.Name;
	}

	public void Reset()
	{
		Panel.color = Color.white;
		Name.text = string.Empty;
	}

	public void SetDone()
	{
		Panel.color = Color.green;
	}
}
