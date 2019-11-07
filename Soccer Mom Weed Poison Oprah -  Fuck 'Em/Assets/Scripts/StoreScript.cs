using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    public static bool storeActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int currentCandyCnt = GameObject.Find("CurrentCandy").transform.childCount;
        Debug.Log(currentCandyCnt);
        if (currentCandyCnt == 0 && !storeActive)
        {
            transform.Find("StorePanel").gameObject.SetActive(true);
            storeActive = true;
        }
    }
}
