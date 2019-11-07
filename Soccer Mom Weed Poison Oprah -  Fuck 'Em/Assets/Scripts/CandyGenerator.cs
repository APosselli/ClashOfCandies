using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGenerator : MonoBehaviour
{
    public static int number = 20;
    public List<GameObject> candyList;
    private static Queue<GameObject> candyBag = new Queue<GameObject>();
    private int swapTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < number - 1; i++)
        {
            int index = Random.Range(0, candyList.Count);
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            GameObject newCandy = Instantiate(candyList[index], GameObject.Find("CandyBag").transform);
            newCandy.transform.localPosition = offset;
            newCandy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            candyBag.Enqueue(candyList[index]);
        }
        GameObject initCurCandy = Instantiate(candyList[Random.Range(0, candyList.Count)], GameObject.Find("CurrentCandy").transform);
        if (initCurCandy.tag == "good")
        {
            GameState.Instance.SetCurrentCandy(false);
        }
        else if (initCurCandy.tag == "bad")
        {
            GameState.Instance.SetCurrentCandy(true);
        }
        GameObject initNextCandy = candyBag.Dequeue();
        Instantiate(initNextCandy, GameObject.Find("NextCandy").transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void RemoveCandyInBag()
    {
        GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
        Destroy(currentCandy);
        GameObject nextCandyInBag = GameObject.Find("CandyBag").transform.GetChild(0).gameObject;
        Destroy(nextCandyInBag);
        GameObject preNextCandy = GameObject.Find("NextCandy").transform.GetChild(0).gameObject;
        preNextCandy.transform.parent = GameObject.Find("CurrentCandy").transform;
        preNextCandy.transform.localPosition = new Vector3(0f, 0f, 0f);
        preNextCandy.GetComponent<SpriteRenderer>().sortingOrder = 2;
        if (preNextCandy.tag == "good")
        {
            GameState.Instance.SetCurrentCandy(false);
        }
        else if (preNextCandy.tag == "bad")
        {
            GameState.Instance.SetCurrentCandy(true);
        }
        GameObject newNextCandy = candyBag.Dequeue();
        Instantiate(newNextCandy, GameObject.Find("NextCandy").transform);
    }

    public static void RemoveLastInBag()
    {
        GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
        Destroy(currentCandy);
        GameObject nextCandyInBag = GameObject.Find("CandyBag").transform.GetChild(0).gameObject;
        Destroy(nextCandyInBag);
        GameObject preNextCandy = GameObject.Find("NextCandy").transform.GetChild(0).gameObject;
        preNextCandy.transform.parent = GameObject.Find("CurrentCandy").transform;
        preNextCandy.transform.localPosition = new Vector3(0f, 0f, 0f);
        preNextCandy.GetComponent<SpriteRenderer>().sortingOrder = 2;
        if (preNextCandy.tag == "good")
        {
            GameState.Instance.SetCurrentCandy(false);
        }
        else if (preNextCandy.tag == "bad")
        {
            GameState.Instance.SetCurrentCandy(true);
        }
    }

    public static void RemoveLast()
    {
        GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
        Destroy(currentCandy);
    }
}
