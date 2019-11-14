using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    public float centerSpeed = 3f;
    public int screenSwipePercentage = 30;
    private Camera cam;
    private Vector3 touchPos;
    private Vector3 fingerPos;
    private Vector3 objectPos;
    private float minSwipeDistance;
    private bool needsRelease = false;
    private int swipeTime = 0;
    private bool swiped = false;
    private bool startPosSet = false;
    private GameObject currentCandy;
    private CandyGenerator candyGenerator;
    private ParticleSystem goodEffect;
    private ParticleSystem badEffect;

    // Start is called before the first frame update
    void Start()
    {
        minSwipeDistance = Screen.width * screenSwipePercentage / 100;
        cam = Camera.main;

        if (FingerInput.GetInputPresent() && FingerInput.GetFingerDown())
            needsRelease = true;

        currentCandy = GameObject.Find("CurrentCandy");
        candyGenerator = GameObject.Find("CandyGenerator").GetComponent<CandyGenerator>();
        goodEffect = GameObject.Find("Particle_Good").GetComponent<ParticleSystem>();
        badEffect = GameObject.Find("Particle_Bad").GetComponent<ParticleSystem>();
        objectPos = currentCandy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.GameOver || GameState.Instance.BetweenLevels || swipeTime >= candyGenerator.number)
            return;

        // If the user isn't touching the screen, the candy should be released
        if (!FingerInput.GetInputPresent() || !FingerInput.GetFingerDown())
        {
            swiped = false;

            // Gradually move the candy to the center of the screen
            currentCandy.transform.position = Vector3.Lerp(currentCandy.transform.position, objectPos, Time.deltaTime * centerSpeed);

            needsRelease = false;
            return;
        }

        if (needsRelease)
            return;

        fingerPos = FingerInput.GetFingerPosition();

        if (FingerInput.GetFingerPressed())
        {
            touchPos = fingerPos;
            startPosSet = true;
        }

        if (!startPosSet)
            return;

        if (Mathf.Abs(touchPos.x - fingerPos.x) >= minSwipeDistance && !swiped && swipeTime < candyGenerator.number)
        {
            GameObject candyChild = currentCandy.transform.GetChild(0).gameObject;
            // The user swiped left
            if (touchPos.x - fingerPos.x > 0)
            {
                if (candyChild.tag == "bad")
                {
                    // The user correctly disposed of a bad candy
                    badEffect.Play(true);
                    AudioManager.goodSwipe.Play();
                    GameState.Instance.AddToScore(1);
                }
                else if (candyChild.tag == "good")
                {
                    // The user incorrectly disposed of a good candy
                    goodEffect.Play(true);
                    AudioManager.badSwipe.Play();
                    GameState.Instance.DecrementLives();
                }

                // Check if the user disposed of too many good candies
                if (GameState.Instance.GameOver)
                {
                    return;
                }
            }
            else // The user swiped right
            {
                // The user incorrectly saved a bad candy, poisoning their child and triggering a game over
                if (candyChild.tag == "bad")
                {
                    badEffect.Play(true);
                    GameState.Instance.InvokeGameOver();
                    return;
                }

                // The user correctly saved a good candy
                goodEffect.Play(true);
                AudioManager.goodSwipe.Play();
                GameState.Instance.AddToScore(1);
            }

            // Check if the last candy was swiped
            if (swipeTime == candyGenerator.number - 1)
            {
                candyGenerator.RemoveLast();
                swipeTime++;
                swiped = true;
            }
            else
            {
                if (swipeTime == candyGenerator.number - 2)
                {
                    candyGenerator.RemoveLastInBag();
                    swipeTime++;
                    swiped = true;
                }
                if (swipeTime < candyGenerator.number - 2)
                {
                    candyGenerator.RemoveCandyInBag();
                    swipeTime++;
                    swiped = true;
                }

                // Set current candy to be the new candy pulled from the bag
                currentCandy = GameObject.Find("CurrentCandy");
                currentCandy.transform.position = objectPos;
                needsRelease = true;
            }
        }

        // Update the candy position if the user is swiping
        if (swipeTime < candyGenerator.number && !needsRelease)
        {
            currentCandy.transform.position = objectPos + new Vector3(cam.ScreenToWorldPoint(fingerPos).x - cam.ScreenToWorldPoint(touchPos).x, 0f, 0f);
        }
    }
}
