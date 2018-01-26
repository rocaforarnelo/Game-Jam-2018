using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerInputs : SkillAction{

    public Text TestText;
    public Text TestText2;

    public int DashInputCount;
    public int DashInputTarget;

    float a;
    bool shakeLeft;

    Vector2 touchPress;
    Vector2 touchRelease;

    void Update ()
    {
        //DashInput();
        //JumpInput();
        //BashInput();
        //SlashDiagonalInput();
        //HorizontalInput();
        //BlockInput();
    }

    void ShakeEvent()
    {
        if (Input.acceleration.x >= .3f && !shakeLeft)
        {
            a++;
            shakeLeft = !shakeLeft;
        }
        else if (Input.acceleration.x <= -.3f && shakeLeft)
        {
            a++;
            shakeLeft = !shakeLeft;
        }

        if(a > 100)
        {
            // send Done
            TestText.text = a.ToString() + "ShakeDone";
        }
    }

    void FlipEvent()
    {
        if (Input.deviceOrientation == DeviceOrientation.FaceDown)
        {
            //send Done
            //TestText2.text = "Flipped";
        }
    }

    void DashInput()
    {
        if(DashInputCount < DashInputTarget)
        {
            //TestText2.text = DashInputCount.ToString();
            if (Input.GetMouseButtonDown(0))
            {
                DashInputCount++;
            }
        }
        else
        {
            //send Done
            //TestText2.text = "DashDone";
        }
    }

    void JumpInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TestText.text = touchPress.ToString();
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TestText.text = touchPress.ToString();
            if ((touchPress.y - touchRelease.y) < -2f)
            {
                //sendDone
                TestText.text = (touchPress.y - touchRelease.y).ToString();
                TestText2.text = "jump";
            }
        }
    }

    void BashInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TestText.text = touchPress.ToString();
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TestText.text = touchPress.ToString();
            if ((touchPress.y - touchRelease.y) > 2f)
            {
                TestText.text = (touchPress.y - touchRelease.y).ToString();
                TestText2.text = "Bash";
                //sendDone
            }
        }
    }

    void SlashDiagonalInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TestText.text = touchPress.ToString();
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((touchPress.y - touchRelease.y) < -2f && ((touchPress.x - touchRelease.x) > 2f || (touchPress.x - touchRelease.x) < -2f) ||
                (touchPress.y - touchRelease.y) > 2f && ((touchPress.x - touchRelease.x) > 2f || (touchPress.x - touchRelease.x) < -2f))
            {
                //sendDone
                TestText.text = (touchPress.y - touchRelease.y).ToString();
                TestText2.text = "diag";
            }
        }
    }
    
    void HorizontalInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            touchPress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TestText.text = touchPress.ToString();
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TestText.text = touchPress.ToString();
            if (((touchPress.x - touchRelease.x) < -2f || (touchPress.x - touchRelease.x) > 2f) &&
                ((touchPress.y - touchRelease.y) > -.8f && (touchPress.y - touchRelease.y) < .8f))
            {
                //sendDone
                TestText.text = (touchPress.y - touchRelease.y).ToString();
                TestText2.text = "horizontal";
            }
        }
    }

    void BlockInput()
    {
        if(Input.touchCount == 2)
        {
            TestText2.text = "block";
            //sendDone
        }
    }

}
