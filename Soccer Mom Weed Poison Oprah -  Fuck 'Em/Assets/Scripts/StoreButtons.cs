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
            AudioManager.UI.Play();
            transform.Find("RemindPanel").gameObject.SetActive(true);
        }
        else if (GameMetaInfo.Instance.Money >= candyCost)
        {
            AudioManager.purchase.Play();
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
            AudioManager.store.Stop();
            AudioManager.menu.Play();
            SceneManager.LoadScene("NewsScene");
            return;
        }
        AudioManager.store.Stop();
        AudioManager.menu.Play();
        GameMetaInfo.Instance.SwitchPlayer();
        SceneManager.LoadScene("Selection");
    }

    public void GoToBuyCoin()
    {
        transform.Find("PurchasePanel").gameObject.SetActive(true);
        transform.Find("RemindPanel").gameObject.SetActive(false);
    }

    public void GoBackToStore()
    {
        transform.Find("RemindPanel").gameObject.SetActive(false);
        transform.Find("PurchasePanel").gameObject.SetActive(false);
    }

    public void SelectPack1()
    {
        transform.Find("RemindPanel2").gameObject.SetActive(true);
        transform.Find("PurchasePanel").gameObject.SetActive(false);
    }

    public void SelectPack2()
    {
        transform.Find("RemindPanel3").gameObject.SetActive(true);
        transform.Find("PurchasePanel").gameObject.SetActive(false);
    }

    public void BuyPack1()
    {
        GameMetaInfo.Instance.Money += 500;
    }

    public void BuyPack2()
    {
        GameMetaInfo.Instance.Money += 3500;
    }

    public void BackToPurchase()
    {
        transform.Find("PurchasePanel").gameObject.SetActive(true);
        transform.Find("RemindPanel2").gameObject.SetActive(false);
        transform.Find("RemindPanel3").gameObject.SetActive(false);
    }

    public void PlayPurchase()
    {
        AudioManager.purchase.Play();
    }

    public void PlayUI()
    {
        AudioManager.UI.Play();
    }
}
