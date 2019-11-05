using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    private Camera camera;
    private Vector3 touchPos;
    private Vector3 fingerPos;
    private Vector3 objectPos;
    private float minSwipeDistance;
    private bool needsRelease = false;

    // Start is called before the first frame update
    void Start()
    {
        minSwipeDistance = Screen.width * 30 / 100;
        camera = Camera.main;

        if (FingerInput.GetInputPresent() && FingerInput.GetFingerDown())
            needsRelease = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FingerInput.GetInputPresent() || !FingerInput.GetFingerDown())
        {
            if (FingerInput.GetInputPresent() && !needsRelease && FingerInput.GetFingerReleased())
            {
                gameObject.transform.position = objectPos;
            }

            needsRelease = false;
            return;
        }

        if (needsRelease)
            return;

        fingerPos = FingerInput.GetFingerPosition();

        if (FingerInput.GetFingerPressed())
        {
            touchPos = fingerPos;
            objectPos = gameObject.transform.position;
        }

        if (Mathf.Abs(touchPos.x - fingerPos.x) >= minSwipeDistance)
        {
            if (touchPos.x - fingerPos.x > 0)
            {
                Instantiate(gameObject, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
                Destroy(gameObject);
            }
            else
            {
                Instantiate(gameObject, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
                Destroy(gameObject);
            }
        }

        gameObject.transform.position = objectPos + new Vector3(camera.ScreenToWorldPoint(fingerPos).x - camera.ScreenToWorldPoint(touchPos).x, 0f, 0f);
    }
}
