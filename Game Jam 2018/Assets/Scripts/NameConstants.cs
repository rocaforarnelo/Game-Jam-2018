using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NameConstants {

	public static readonly string[] CharacterNames = new string[]{
		"Alchemist", "Wizard", "Warrior"
	};

	public static readonly string[] WizardSkillNames = new string[]{
		"Meteor", "Tornado", "Tsunami"
	};

	public static readonly string[] AlchemistkillNames = new string[]{
		"Fortify Destruction", "Dynamite", "StaminaPotion"
	};

	public static readonly string[] WarriorSkillNames = new string[]{
		"Jumpig Dragon Slash", "Swashbuckle", "Holy Shield"
	};

	public static string[][] SkillNames = new string[][] {
		AlchemistkillNames, WizardSkillNames, WarriorSkillNames
	};
}
