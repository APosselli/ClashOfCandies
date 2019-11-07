using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    public float centerSpeed = 3f;
    private Camera cam;
    private Vector3 touchPos;
    private Vector3 fingerPos;
    private Vector3 objectPos;
    private float minSwipeDistance;
    private bool needsRelease = false;
    private int swipeTime = 0;
    private bool swiped = false;
    public bool isPoison { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        /*
        int poisonInt = Random.Range(0, 2);
        if (poisonInt == 0)
        {
            SetIsPoison(true);
        }
        else
        {
            SetIsPoison(false);
        }
        */

        minSwipeDistance = Screen.width * 30 / 100;
        cam = Camera.main;

        if (FingerInput.GetInputPresent() && FingerInput.GetFingerDown())
            needsRelease = true;

        //objectPos = gameObject.transform.position;
        objectPos = GameObject.Find("CurrentCandy").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.GameOver)
            return;

        if (!FingerInput.GetInputPresent() || !FingerInput.GetFingerDown())
        {
            swiped = false;
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objectPos, Time.deltaTime * centerSpeed);
            if (swipeTime < CandyGenerator.number)
            {
                GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
                currentCandy.transform.position = objectPos;
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
        }

        if (Mathf.Abs(touchPos.x - fingerPos.x) >= minSwipeDistance && !swiped && swipeTime < CandyGenerator.number)
        {
            if (touchPos.x - fingerPos.x > 0)
            {
                GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
                if (currentCandy.tag == "bad")
                {
                    //Debug.Log("save child");
                    GameState.Instance.AddToScore(1);
                }
                else if (currentCandy.tag == "good")
                {
                    //Debug.Log("miss");
                    GameState.Instance.DecrementLives();
                }

                if (GameState.Instance.GameOver)
                {
                    //Destroy(gameObject);
                    return;
                }

                if (swipeTime == CandyGenerator.number - 1)
                {
                    CandyGenerator.RemoveLast();
                    swipeTime++;
                    swiped = true;
                }
                if (swipeTime == CandyGenerator.number - 2)
                {
                    CandyGenerator.RemoveLastInBag();
                    swipeTime++;
                    swiped = true;
                }
                if (swipeTime < CandyGenerator.number - 2)
                {
                    CandyGenerator.RemoveCandyInBag();
                    swipeTime++;
                    swiped = true;
                }
                //Debug.Log(swipeTime);
                //Instantiate(gameObject, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
                //Destroy(gameObject);
            }
            else
            {
                GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
                if (currentCandy.tag == "bad")
                {
                    Debug.Log("child killed");
                    //Destroy(gameObject);
                    GameState.Instance.InvokeGameOver();
                    return;
                }

                //Debug.Log("correct");
                GameState.Instance.AddToScore(1);

                if (swipeTime == CandyGenerator.number - 1)
                {
                    CandyGenerator.RemoveLast();
                    swipeTime++;
                    swiped = true;
                }
                if (swipeTime == CandyGenerator.number - 2)
                {
                    CandyGenerator.RemoveLastInBag();
                    swipeTime++;
                    swiped = true;
                }
                if (swipeTime < CandyGenerator.number - 2)
                {
                    CandyGenerator.RemoveCandyInBag();
                    swipeTime++;
                    swiped = true;
                }
                //Debug.Log(swipeTime);
                //Instantiate(gameObject, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
                //Destroy(gameObject);
            }
        }

        //gameObject.transform.position = objectPos + new Vector3(cam.ScreenToWorldPoint(fingerPos).x - cam.ScreenToWorldPoint(touchPos).x, 0f, 0f);
        if (swipeTime < CandyGenerator.number)
        {
            GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
            currentCandy.transform.position = objectPos + new Vector3(cam.ScreenToWorldPoint(fingerPos).x - cam.ScreenToWorldPoint(touchPos).x, 0f, 0f);
        }
    }

    public void SetIsPoison(bool isPoison)
    {
        this.isPoison = isPoison;
        GameState.Instance.SetCurrentCandy(isPoison);
    }
}
