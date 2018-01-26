using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Ingredient {

    float ShakeCount;
    bool shakeLeft;

    public override void Update()
    {
        ShakeEvent();
    }

    void ShakeEvent()
    {
        if (Input.acceleration.x >= .3f && !shakeLeft)
        {
            ShakeCount++;
            shakeLeft = !shakeLeft;
        }
        else if (Input.acceleration.x <= -.3f && shakeLeft)
        {
            ShakeCount++;
            shakeLeft = !shakeLeft;
        }

        if (ShakeCount > 100)
        {
            SetDone();
        }
    }
}
