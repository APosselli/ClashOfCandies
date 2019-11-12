using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwipeControls : MonoBehaviour
{
    // Start is called before the first frame update
    private float minSwipeDistance;
    private Camera cam;
    //public bool pressed = false;
    private bool needsRelease = false;
    private bool swiped = false;
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

        /*if (FingerInput.GetFingerPressed())
        {
            pressed = true;
        }*/

        if (!FingerInput.GetInputPresent() || !FingerInput.GetFingerDown())
        {
            swiped = false;
            needsRelease = false;
            return;
        }

        if (needsRelease)
            return;

        fingerPos = FingerInput.GetFingerPosition();

        /*if (FingerInput.GetFingerDown())
        {
            fingerPos = FingerInput.GetFingerPosition();
        }*/

        if (FingerInput.GetFingerPressed())
        {
            touchPos = fingerPos;
        }

        //if (Mathf.Abs(touchPos.y - fingerPos.y) >= minSwipeDistance && pressed)
        if (Mathf.Abs(touchPos.y - fingerPos.y) >= minSwipeDistance && !swiped)
        {
            //pressed = false;
            //if(touchPos.y >= Screen.height / 2)
            if (touchPos.y - fingerPos.y < 0)
            {
                handController.slap();
                swiped = true;
            }
            //else if(touchPos.y < Screen.height / 2)
            else
            {
                handController.premiumCandy();
                swiped = true;
            }
        }
    }
}
