	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : SkillAction {

    public virtual void Update()
    {
		if (Input.GetMouseButtonDown (0)) {
			SetDone ();
		}
	}
}
		