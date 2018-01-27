using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : WarriorSkillAction
{

    Vector2 touchPress;
    Vector2 touchRelease;

	#if UNITY_ANDROID
	public override void Update()
    {
        JumpInput();
    }
	#endif
    void JumpInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((touchPress.y - touchRelease.y) < -2f)
            {
                SetDone();
            }
        }
    }
}
