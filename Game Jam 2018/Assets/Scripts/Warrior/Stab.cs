using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : WarriorSkillAction
{

    Vector2 touchPress;
    Vector2 touchRelease;

    public override void Update()
    {
        HorizontalInput();
    }

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
                ((touchPress.y - touchRelease.y) > -.8f && (touchPress.y - touchRelease.y) < .8f))
            {
                SetDone();
            }
        }
    }
}
