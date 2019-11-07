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
    public bool isPoison { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        int poisonInt = Random.Range(0, 2);

        // TODO: Remove after merging with Jieyi's candy generation script
        if (poisonInt == 0)
        {
            SetIsPoison(true);
        }
        else
        {
            SetIsPoison(false);
        }

        minSwipeDistance = Screen.width * 30 / 100;
        cam = Camera.main;

        if (FingerInput.GetInputPresent() && FingerInput.GetFingerDown())
            needsRelease = true;

        objectPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.Instance.GameOver)
            return;

        if (!FingerInput.GetInputPresent() || !FingerInput.GetFingerDown())
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objectPos, Time.deltaTime * centerSpeed);

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

        if (Mathf.Abs(touchPos.x - fingerPos.x) >= minSwipeDistance)
        {
            if (touchPos.x - fingerPos.x > 0)
            {
                if (isPoison)
                {
                    GameState.Instance.AddToScore(1);
                }
                else
                {
                    GameState.Instance.DecrementLives();
                }

                if (GameState.Instance.GameOver)
                {
                    Destroy(gameObject);
                    return;
                }

                // TODO: Remove after merging with Jieyi's candy generation script
                Instantiate(gameObject, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));

                Destroy(gameObject);
            }
            else
            {
                if (isPoison)
                {
                    Destroy(gameObject);
                    GameState.Instance.InvokeGameOver();
                    return;
                }

                GameState.Instance.AddToScore(1);
                // TODO: Remove after merging with Jieyi's candy generation script
                Instantiate(gameObject, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));

                Destroy(gameObject);
            }
        }

        gameObject.transform.position = objectPos + new Vector3(cam.ScreenToWorldPoint(fingerPos).x - cam.ScreenToWorldPoint(touchPos).x, 0f, 0f);
    }

    public void SetIsPoison(bool isPoison)
    {
        this.isPoison = isPoison;
        GameState.Instance.SetCurrentCandy(isPoison);
    }
}
