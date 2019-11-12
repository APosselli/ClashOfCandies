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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyCandy()
    {
        if (StoreScript.money > 0 && GameMetaInfo.Instance.PremiumCandy < 20)
        {
            StoreScript.money -= candyCost;
            GameMetaInfo.Instance.PremiumCandy++;
        }
    }

    public void ContinueGame()
    {
        GameMetaInfo.Instance.SwitchPlayer();
        GameState.ResetLevel();
    }
}
