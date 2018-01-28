using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NameConstants {
	public static readonly string[] Actions = new string[]{
		"Prepare", "Cast", "Do"
	};

	public static readonly string[] CharacterNames = new string[]{
		"Alchemist", "Wizard", "Warrior"
	};

	public static readonly string[] WizardSkillNames = new string[]{
		"Meteor", "Tornado", "Tsunami"
	};

	public static readonly string[] AlchemistkillNames = new string[]{
		"Moon Shine", "Ambrosia", "Plague Bomb"
	};

	public static readonly string[] WarriorSkillNames = new string[]{
		"Rising Dragon Slash", "Swashbuckle", "Retaliate"
	};

	public static string[][] SkillNames = new string[][] {
		AlchemistkillNames, WizardSkillNames, WarriorSkillNames
	};
}
