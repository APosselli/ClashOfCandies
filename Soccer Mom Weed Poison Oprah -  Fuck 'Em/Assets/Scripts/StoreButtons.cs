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
        if (StoreScript.money > 0 && StoreScript.premCandyNum < 20)
        {
            StoreScript.money -= candyCost;
            StoreScript.premCandyNum++;
        }
    }

    public void ContinueGame()
    {
        StoreScript.storeActive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
