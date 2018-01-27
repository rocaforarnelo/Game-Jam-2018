using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour {
	public NetworkPlayerCharacter NetworkPlayerCharacter;
	public int Index;
	public const int SkillCount = 3;
	private readonly Vector2 instantiationPosition = new Vector2 (15.0f, 8.0f);
	public Skill CurrentSkill;
	public Skill[] Skills;
	public SkillAction[] SkillActions;
	public SkillAction CurrentSkillAction;
	public SkillActionPanel[] SkillActionPanels;
	public GameObject SkillButtonsPanel, SkillActionButtonsPanel, RequiredSkillActionsPanel;
	public Text ActionValueLabel;

	void Start()
	{
		
	}

	public void SetActionValueLabel(string value)
	{
		ActionValueLabel.text = value;
	}

	public void Initialize()
	{
		ResetSkillActionPanels ();
		InitializeSkillActionPanels ();
	}

	public void SetSkillActionDone(int index)
	{
		SkillActionPanels [index].SetDone ();
		DestroyCurrentSkillAction ();
	}

	public void DestroyCurrentSkillAction()
	{
		if (CurrentSkillAction != null) {
			Destroy (CurrentSkillAction.gameObject);
		}
	}

	public void InitializeSkillActionPanels()
	{
		SkillAction[] currentSkillActions = CurrentSkill.SkillActions;
		for (int i = 0; i < currentSkillActions.Length; i++) {
			SkillActionPanels [i].Initialize (currentSkillActions [i]);
		}
	}

	public void ResetSkillActionPanels()
	{
		for (int i = 0; i < SkillActionPanels.Length; i++) {
			SkillActionPanels [i].Reset ();
		}
	}

	public virtual void SetCurrentSkillAction(int index)
	{
		if (CurrentSkill == null)
			return;
		DestroyCurrentSkillAction ();
		SkillAction currentSkillActionInstance = Instantiate<SkillAction> (SkillActions [index], instantiationPosition, Quaternion.identity);
		CurrentSkillAction = currentSkillActionInstance;
		CurrentSkillAction.PlayerCharacter = this;
	}
	//TODO MAGIC STRINGS	
	public void SetCurrentSkill(int index)
	{
		RequiredSkillActionsPanel.gameObject.SetActive (true);
		RequiredSkillActionsPanel.GetComponent<Animator> ().SetTrigger ("Enter");
		SkillActionButtonsPanel.SetActive (true);
		SkillActionButtonsPanel.GetComponent<Animator> ().SetTrigger ("Enter");
		DestroyCurrentSkill ();
		InstantiateCurrentSkill (index);
		Initialize ();
	}

	private void DestroyCurrentSkill()
	{
		if (CurrentSkill != null) {
			Destroy (CurrentSkill.gameObject);
		}
	}

	public void SetSkillDone()
	{
		ResetSkillActionPanels ();
		SkillButtonsPanel.SetActive (true);
	}

	private void InstantiateCurrentSkill(int index)
	{	
		Skill currentSkillInstance = Instantiate <Skill>(Skills [index], Vector2.zero, Quaternion.identity);
		CurrentSkill = currentSkillInstance;
		CurrentSkill.PlayerCharacter = this;
	}
}
