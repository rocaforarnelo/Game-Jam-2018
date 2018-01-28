using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : WarriorSkillAction
{
	#if UNITY_ANDROID
	public override void Update()
    {
        BlockInput();
    }
	#endif
    void BlockInput()
    {
        if (Input.touchCount == 2)
        {
            AudioController.Instance.PlayWarriorSfx(1);
            SetDone();
        }
    }
}
