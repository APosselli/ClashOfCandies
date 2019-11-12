using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwipeControls : MonoBehaviour
{
    // Start is called before the first frame update
    private float minSwipeDistance;
    private Camera cam;
    // public bool pressed = false, justTriggeredSwipeEffect = false;
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
        /*if (!FingerInput.GetFingerDown())
         {
             pressed = true;
         }*/

        if (!FingerInput.GetInputPresent() || !FingerInput.GetFingerDown())
        {
            swiped = false;
        }

            if (FingerInput.GetFingerDown())
        {
            fingerPos = FingerInput.GetFingerPosition();
            Debug.Log("finger"+fingerPos);
        }

        if (FingerInput.GetFingerPressed())
        {
            touchPos = fingerPos;
        }

        if (Mathf.Abs(touchPos.y - fingerPos.y) >= minSwipeDistance && !swiped)
        //if(Mathf.Abs(touchPos.y - fingerPos.y) >= minSwipeDistance && pressed)
        {
            //pressed = false;
            if(touchPos.y - fingerPos.y < 0)
            //if (touchPos.y >= Screen.height / 2)
             {
                Debug.Log("up touch"+touchPos);
                handController.slap();
                swiped = true;
            }
            else if (touchPos.y - fingerPos.y > 0)
            //else if(touchPos.y < Screen.height / 2)
            {
                Debug.Log("down touch" + touchPos);
                handController.premiumCandy();
                swiped = true;
            }
        }
    }
}
