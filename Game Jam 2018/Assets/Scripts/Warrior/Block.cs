using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : WarriorSkillAction
{

    public override void Update()
    {
        BlockInput();
    }

    void BlockInput()
    {
        if (Input.touchCount == 2)
        {
            SetDone();
        }
    }
}
