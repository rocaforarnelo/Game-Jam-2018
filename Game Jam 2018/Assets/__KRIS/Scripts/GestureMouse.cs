using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureMouse : MonoBehaviour {

    public GameObject MouseRenderer;

    private void Update()
    {
        MouseTrail();
    }

    void MouseTrail()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        MouseRenderer.transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            MouseRenderer.SetActive(true);
            MouseRenderer.GetComponent<TrailRenderer>().time = 5;
            MouseRenderer.GetComponent<TrailRenderer>().widthMultiplier = 5f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseRenderer.GetComponent<TrailRenderer>().time = 0f;
            MouseRenderer.GetComponent<TrailRenderer>().widthMultiplier = 0f;
        }
    }

}
