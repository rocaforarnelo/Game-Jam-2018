using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : WarriorSkillAction
{
    public int DashInputCount;
    public int DashInputTarget;

	#if UNITY_ANDROID
	public override void Update()
    {
        DashInput();
    }
	#endif
    void DashInput()
    {
        if (DashInputCount < DashInputTarget)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DashInputCount++;
            }
        }
        else
        {
			DashInputCount = 0;
            SetDone();
        }
    }
}
