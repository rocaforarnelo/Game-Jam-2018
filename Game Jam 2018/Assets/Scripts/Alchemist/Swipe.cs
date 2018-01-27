using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : Ingredient
{
	private const float touchThreshold = 2.0f;
    Vector2 touchPress;
    Vector2 touchRelease;

	#if UNITY_ANDROID
    public override void Update()
    {
        SwipeIngredients();
    }
	#endif

    void SwipeIngredients()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchPress = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            touchRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (Mathf.Abs(touchPress.x - touchRelease.x) > touchThreshold || Mathf.Abs(touchPress.y - touchRelease.y) > touchThreshold)
            {
                SetDone();
            }
        }
    }
}
