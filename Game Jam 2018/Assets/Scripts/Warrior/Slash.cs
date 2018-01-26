using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : WarriorSkillAction
{

    Vector2 touchPress;
    Vector2 touchRelease;

    public override void Update()
    {
        SlashDiagonalInput();
    }

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
                SetDone();
            }
        }
    }
}
