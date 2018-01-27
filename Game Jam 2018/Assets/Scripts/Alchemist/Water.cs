using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Ingredient {

	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the
	// filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation,
	// or at least according to Brady! ;)
	float shakeDetectionThreshold = 2.0f;
	public int MaxShakeCount = 10;
	private int currentShakeCount;
	float lowPassFilterFactor;
	Vector3 lowPassValue;

	#if UNITY_ANDROID
	public override void Update()
    {
        ShakeEvent();
    }
	#endif

//    void ShakeEvent()
//    {
//        if (Input.acceleration.x >= .3f && !shakeLeft)
//        {
//            ShakeCount++;
//            shakeLeft = !shakeLeft;
//        }
//        else if (Input.acceleration.x <= -.3f && shakeLeft)
//        {
//            ShakeCount++;
//            shakeLeft = !shakeLeft;
//        }
//
//        if (ShakeCount > 100)
//        {
//            SetDone();
//        }
//    }



	void Start()
	{
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
	}

	void ShakeEvent()
	{
		Vector3 acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		Vector3 deltaAcceleration = acceleration - lowPassValue;

		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
		{
			currentShakeCount++;
			if (currentShakeCount >= MaxShakeCount) {
				SetDone ();
				currentShakeCount = 0;
			}
			Debug.Log("Shake event detected at time "+Time.time);
		}
	}
}
