using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour {
	public PlayerCharacter PlayerCharacter;
	public string Name;
	public int Index;

	public virtual void Reset()
	{
		
	}

	public virtual void SetDone()
	{
		PlayerCharacter.CurrentSkill.SetSkillActionDone (Index);
	}
}
