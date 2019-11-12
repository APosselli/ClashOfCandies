using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsScript : MonoBehaviour
{
    public GameObject newsBoard;
    public GameObject news1;
    public GameObject news2;
    private bool newsDisplayed = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (FingerInput.GetFingerDown() && !newsDisplayed)
        {
            newsBoard.SetActive(true);
            news1.SetActive(true);
            news2.SetActive(true);

        }
    }
}
