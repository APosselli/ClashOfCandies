using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewsScript : MonoBehaviour
{
    public GameObject newsBoard;
    public GameObject startButton;
    public GameObject news1;
    public GameObject news2;
    private bool newsDisplayed = false;
    private List<GameObject> badCandyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        /* for (int i = 0; i < 9; i++)
        {
            int good = Random.Range(0, 2);
            if (good == 0)
                GameMetaInfo.candyList[i].tag = "good";
            else
                GameMetaInfo.candyList[i].tag = "bad";
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && !newsDisplayed)
        {
            newsDisplayed = true;
            newsBoard.SetActive(true);
            news1.SetActive(true);
            news2.SetActive(true);
            startButton.SetActive(true);
            foreach (GameObject candy in GameMetaInfo.candyList)
            {
                if (candy.tag == "bad")
                    badCandyList.Add(candy);
            }
            for (int i = 0; i < badCandyList.Count; i++)
            {
                GameObject badCandy = Instantiate(badCandyList[i], GameObject.Find("BadCandy" + (i + 1).ToString()).transform);
                badCandy.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene("SwipeDemo");
    }
}
