using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    public static bool storeActive = false;
    private UnityEngine.UI.Text moneyText;
    private UnityEngine.UI.Text candyText;

    private int money;
    private int premCandyNum;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = transform.Find("StorePanel").Find("MoneyText").gameObject.GetComponent<UnityEngine.UI.Text>();
        candyText = transform.Find("StorePanel").Find("PremCandyText").gameObject.GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (storeActive)
        {
            premCandyNum = GameMetaInfo.Instance.PremiumCandy;
            money = GameMetaInfo.Instance.Money;
            moneyText.text = "Coins: " + money.ToString();
            candyText.text = "Premium Candy: " + premCandyNum.ToString();
        }
    }

    public void SetStoreActive()
    {
        storeActive = true;
        AudioManager.menu.Stop();
        AudioManager.store.Play();
        transform.Find("StorePanel").gameObject.SetActive(true);
    }

    public void SetStoreDeactive()
    {
        storeActive = false;
        AudioManager.store.Stop();
        AudioManager.menu.Play();
        transform.Find("StorePanel").gameObject.SetActive(false);
    }
}
