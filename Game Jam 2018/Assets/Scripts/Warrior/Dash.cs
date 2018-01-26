using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : WarriorSkillAction
{
    public int DashInputCount;
    public int DashInputTarget;

    public override void Update()
    {
        DashInput();
    }

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
            SetDone();
        }
    }
}
