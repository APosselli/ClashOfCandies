using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerInput : MonoBehaviour
{
    static private bool useMouse = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    static public bool GetInputPresent()
    {
        useMouse = Input.touchCount <= 0;
        return Input.touchCount > 0 || Input.mousePresent;
    }

    static public bool GetFingerPressed()
    {
        if (useMouse)
        {
            return Input.GetMouseButtonDown(0);
        }

        return Input.GetTouch(0).phase == TouchPhase.Began;
    }

    static public bool GetFingerDown()
    {
        if (useMouse)
        {
            return Input.GetMouseButton(0);
        }

        return Input.touchCount > 0;
    }

    static public bool GetFingerReleased()
    {
        if (useMouse)
        {
            return Input.GetMouseButtonUp(0);
        }

        return Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    static public Vector2 GetFingerPosition()
    {
        if (useMouse)
        {
            return Input.mousePosition;
        }

        return Input.GetTouch(0).position;
    }
}
