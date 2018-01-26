using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Skill : NetworkBehaviour {
	public PlayerCharacter PlayerCharacter;
	public SkillAction[] SkillActions;
	public string SkillName;
	public int SkillIndex;
	private int currentSkillActionIndex;

	public void SetSkillActionDone(int index)
	{
		PlayerCharacter.DestroyCurrentSkillAction ();
		if (SkillActions [currentSkillActionIndex].Index == index) {
			PlayerCharacter.SetSkillActionDone (currentSkillActionIndex);
			currentSkillActionIndex++;
			if (currentSkillActionIndex == SkillActions.Length) {
				SetDone ();
			}
		}
	}

	public void SetDone()
	{
		Debug.Log (PlayerCharacter.name);
		PlayerCharacterSetSkillDone (SkillIndex);
		PlayerCharacter.SetSkillDone ();
	}

	public void PlayerCharacterSetSkillDone(int skillIndex)
	{
		if (PlayerCharacter.NetworkPlayerCharacter.AssignedSkillIndex == skillIndex) {
			SetRightSkill ();
			PlayerCharacter.NetworkPlayerCharacter.CmdDecreaseEnemyCanvasLife ();
		}
	}

	public void SetRightSkill()
	{
		PlayerCharacter.NetworkPlayerCharacter.SetDone ();
	}
}
