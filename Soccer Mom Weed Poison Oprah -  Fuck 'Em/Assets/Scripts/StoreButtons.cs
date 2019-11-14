using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreButtons : MonoBehaviour
{

    private int candyCost = 9;

    // Start is called before the first frame update
    void Start()
    {
        if (GameState.Instance.GameOver)
        {
            UnityEngine.UI.Text continueText = transform.Find("ContinueButton/Text").GetComponent<UnityEngine.UI.Text>();
            continueText.text = "Retry";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyCandy()
    {
        if (GameMetaInfo.Instance.Money > candyCost)
        {
            GameMetaInfo.Instance.Money -= candyCost;
            GameMetaInfo.Instance.PremiumCandy++;
        }
    }

    public void ContinueGame()
    {
        StoreScript.storeActive = false;
        if (GameState.Instance.GameOver)
        {
            GameState.Instance.GameOver = false;
            SceneManager.LoadScene("NewsScene");
            return;
        }
        GameMetaInfo.Instance.SwitchPlayer();
        SceneManager.LoadScene("Selection");
    }
}
