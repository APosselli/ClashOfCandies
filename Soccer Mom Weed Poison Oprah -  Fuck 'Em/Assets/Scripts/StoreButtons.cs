using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreButtons : MonoBehaviour
{

    private int candyCost = 1000;

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
        if (GameMetaInfo.Instance.Money < candyCost)
        {
            transform.Find("RemindPanel").gameObject.SetActive(true);
        }
        else if (GameMetaInfo.Instance.Money >= candyCost)
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
            SceneManager.LoadScene("SwipeDemo");
            return;
        }
        GameMetaInfo.Instance.SwitchPlayer();
        SceneManager.LoadScene("NewsScene");
    }

    public void GoToBuyCoin()
    {
        transform.Find("PurchasePanel").gameObject.SetActive(true);
        transform.Find("RemindPanel").gameObject.SetActive(false);
    }

    public void GoBackToStore()
    {
        transform.Find("RemindPanel").gameObject.SetActive(false);
    }
}
