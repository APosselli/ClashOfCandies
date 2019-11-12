using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwipeControls : MonoBehaviour
{
    // Start is called before the first frame update
    private float minSwipeDistance;
    private Camera cam;
    public bool pressed = false, justTriggeredSwipeEffect = false;
    public Vector3 handPos, fingerPos, touchPos;
    private GameObject hand;
    public HandController handController;
    void Start()
    {
        minSwipeDistance = Screen.height * 20 / 100;
        cam = Camera.main;

        handController = GameObject.Find("Hand").GetComponent<HandController>();
    }

    // Update is called once per frame
    void Update()
    {
        FingerInput.GetInputPresent();
        if (FingerInput.GetFingerPressed())
        {
            pressed = true;
        }

        if(FingerInput.GetFingerDown())
        {
            fingerPos = FingerInput.GetFingerPosition();
        }

        if (FingerInput.GetFingerPressed())
        {
            touchPos = fingerPos;
        }

        if (Mathf.Abs(touchPos.y - fingerPos.y) >= minSwipeDistance &&  pressed)
        {
            pressed = false;
            if(touchPos.y >= Screen.height / 2)
            {
                handController.slap();
            }
            else if(touchPos.y < Screen.height / 2)
            {
                handController.premiumCandy();
            }
        }
    }
}
