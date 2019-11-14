using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandySelection : MonoBehaviour
{
    private List<GameObject> candyInstances;
    private List<GameObject> badCandies;
    private List<GameObject> goodCandies;
    private Camera cam;
    private GameObject heldCandy;
    private float timeElapsed = 0f;
    private float timeHeld = 0f;
    private Vector3 grabPos;
    private bool holding = false;
    public UnityEngine.UI.Text messageBox;
    public Canvas canvas;
    public string message1;
    public string message2;
    public float message1Duration = 2f;
    public float message2Duration = 2f;
    public float holdDuration = 2f;
    public float holdDurationOverCandy = 3f;
    public float holdMovementTolerance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        //messageBox.text = message1;

        candyInstances = new List<GameObject>();
        badCandies = new List<GameObject>();
        goodCandies = new List<GameObject>();

        foreach (GameObject candy in GameMetaInfo.candyList)
        {
            GameObject newCandy = Instantiate(candy);
            newCandy.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            candyInstances.Add(newCandy);
            if (newCandy.tag == "bad")
                badCandies.Add(newCandy);
            else
                goodCandies.Add(newCandy);
        }

        cam = Camera.main;
        ArrangeGoodCandies();
        ArrangeBadCandies();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        /*if (timeElapsed > message1Duration && timeElapsed <= message1Duration + message2Duration)
        {
            messageBox.text = message2;
        }
        else*/
        if (timeElapsed > message1Duration + message2Duration)
        {
            canvas.enabled = false;
        }

        if (!FingerInput.GetInputPresent() || !FingerInput.GetFingerDown())
        {
            timeHeld = 0f;
            if (heldCandy == null)
                return;

            if (heldCandy.tag == "good")
            {
                goodCandies.Remove(heldCandy);
            }
            else
            {
                badCandies.Remove(heldCandy);
            }

            if ((cam.WorldToScreenPoint(heldCandy.transform.position).y >= cam.pixelHeight / 2 
                || badCandies.Count >= 9) && goodCandies.Count < 9)
            {
                goodCandies.Add(heldCandy);
                heldCandy.tag = "good";
            }
            else
            {
                badCandies.Add(heldCandy);
                heldCandy.tag = "bad";
            }

            heldCandy = null;
            ArrangeGoodCandies();
            ArrangeBadCandies();
            return;
        }

        Vector3 fingerPos = cam.ScreenToWorldPoint(FingerInput.GetFingerPosition());
        fingerPos.z = 0;
        if (FingerInput.GetFingerPressed())
        {
            holding = true;
            grabPos = fingerPos;
            Vector2 startPoint = fingerPos;
            RaycastHit2D raycastHit = Physics2D.Raycast(startPoint, Vector2.zero);
            if (raycastHit.collider != null)
            {
                heldCandy = raycastHit.collider.gameObject;
            }
        }

        if (heldCandy != null)
        {
            heldCandy.transform.position = fingerPos;
        }

        if (holding)
        {
            if ((grabPos - fingerPos).magnitude > holdMovementTolerance)
            {
                holding = false;
            }
            else
            {
                timeHeld += Time.deltaTime;
                if ((heldCandy != null && timeHeld >= holdDurationOverCandy) ||
                    heldCandy == null && timeHeld >= holdDuration)
                {
                    GoToNewsScreen();
                }
            }
        }
    }

    private void ArrangeGoodCandies()
    {
        int candySquareWidth = cam.pixelWidth / 3;
        int candySquareHeight = cam.pixelHeight / 6;
        for (int i = 0; i < goodCandies.Count; i++)
        {
            Vector3 newPosition = cam.ScreenToWorldPoint(new Vector3(candySquareWidth / 2 + candySquareWidth * (i % 3),
                cam.pixelHeight - (candySquareHeight / 2 + candySquareWidth * (i / 3))));
            newPosition.z = 0;
            goodCandies[i].transform.position = newPosition;
        }
    }

    private void ArrangeBadCandies()
    {
        int candySquareWidth = cam.pixelWidth / 3;
        int candySquareHeight = cam.pixelHeight / 6;
        for (int i = 0; i < badCandies.Count; i++)
        {
            Vector3 newPosition = cam.ScreenToWorldPoint(new Vector3(candySquareWidth / 2 + candySquareWidth * (i % 3),
                cam.pixelHeight - (candySquareHeight / 2 + candySquareWidth * (i / 3) + cam.pixelHeight / 2)));
            newPosition.z = 0;
            badCandies[i].transform.position = newPosition;
        }
    }

    private void GoToNewsScreen()
    {
        for (int i = 0; i < candyInstances.Count; i++)
        {
            GameMetaInfo.candyList[i].tag = candyInstances[i].tag;
        }

        SceneManager.LoadScene("NewsScene");
    }
}
