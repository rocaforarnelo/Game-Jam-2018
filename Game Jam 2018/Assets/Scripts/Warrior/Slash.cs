using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : WarriorSkillAction
{

    Vector2 touchPress;
    Vector2 touchRelease;

	#if UNITY_ANDROID
	public override void Update()
    {
        SlashDiagonalInput();
    }
	#endif
    void SlashDiagonalInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((touchPress.y - touchRelease.y) < -2f && ((touchPress.x - touchRelease.x) > 2f || (touchPress.x - touchRelease.x) < -2f) ||
                (touchPress.y - touchRelease.y) > 2f && ((touchPress.x - touchRelease.x) > 2f || (touchPress.x - touchRelease.x) < -2f))
            {
                AudioController.Instance.PlayWarriorSfx(3);
                SetDone();
            }
        }
    }
}
