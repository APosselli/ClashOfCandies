using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    public static bool storeActive = false;
    private UnityEngine.UI.Text moneyText;
    private UnityEngine.UI.Text candyText;

    // TODO: replace with gamestate variable after merging with Anothoy's work
    public static int money = 999;
    public static int premCandyNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = transform.Find("StorePanel").Find("MoneyText").gameObject.GetComponent<UnityEngine.UI.Text>();
        candyText = transform.Find("StorePanel").Find("PremCandyText").gameObject.GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentCandyCnt = GameObject.Find("CurrentCandy").transform.childCount;
        if (currentCandyCnt == 0 && !storeActive)
        {
            transform.Find("StorePanel").gameObject.SetActive(true);
            storeActive = true;
        }
        if (storeActive)
        {
            moneyText.text = "Money: " + money.ToString();
            candyText.text = "Premium Candy: " + premCandyNum.ToString();
        }
    }
}
