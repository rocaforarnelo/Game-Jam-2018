using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : WarriorSkillAction
{
	private const float yOffset = 2.0f;
    Vector2 touchPress;
    Vector2 touchRelease;

	#if UNITY_ANDROID
	public override void Update()
    {
        HorizontalInput();
    }
	#endif

    void HorizontalInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            touchPress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (((touchPress.x - touchRelease.x) < -2f || (touchPress.x - touchRelease.x) > 2f) &&
				((touchPress.y - touchRelease.y) > -yOffset && (touchPress.y - touchRelease.y) < yOffset))
            {
                AudioController.Instance.PlayWarriorSfx(4);
                SetDone();
            }
        }
    }
}
