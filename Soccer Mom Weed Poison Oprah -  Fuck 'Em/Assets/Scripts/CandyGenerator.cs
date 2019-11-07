using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGenerator : MonoBehaviour
{
    public static int number = 20;
    public List<GameObject> candyList;
    private static Queue<GameObject> candyBag = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < number - 1; i++)
        {
            int index = Random.Range(0, candyList.Count);
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            GameObject newCandy = Instantiate(candyList[index], GameObject.Find("CandyBag").transform);
            newCandy.transform.localPosition = offset;
            newCandy.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            newCandy.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            newCandy.GetComponent<SpriteRenderer>().sortingOrder = number - 1 - i;
            candyBag.Enqueue(candyList[index]);
        }
        GameObject initCurCandy = Instantiate(candyList[Random.Range(0, candyList.Count)], GameObject.Find("CurrentCandy").transform);
        initCurCandy.GetComponent<SpriteRenderer>().sortingOrder = number;
        //initCurCandy.transform.localPosition = new Vector3(0f, 0f, 0f);
        if (initCurCandy.tag == "good")
        {
            ParticleSystem goodEffect = initCurCandy.transform.GetChild(0).GetComponent<ParticleSystem>();
            Debug.Log(goodEffect);
            goodEffect.Play(true);
            GameState.Instance.SetCurrentCandy(false);
            
        }
        else if (initCurCandy.tag == "bad")
        {
            GameState.Instance.SetCurrentCandy(true);
        }
        GameObject initNextCandy = Instantiate(candyBag.Dequeue(), GameObject.Find("NextCandy").transform);
        initNextCandy.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
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
        preNextCandy.transform.localScale = new Vector3(0.2f, 0.2f, 2f);
        preNextCandy.GetComponent<SpriteRenderer>().sortingOrder = number;
        if (preNextCandy.tag == "good")
        {
            ParticleSystem goodEffect = preNextCandy.transform.GetChild(0).GetComponent<ParticleSystem>();
            Debug.Log(goodEffect);
            goodEffect.Play(true);
            GameState.Instance.SetCurrentCandy(false);
        }
        else if (preNextCandy.tag == "bad")
        {
            GameState.Instance.SetCurrentCandy(true);
        }
        GameObject newNextCandy = Instantiate(candyBag.Dequeue(), GameObject.Find("NextCandy").transform);
        newNextCandy.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
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
        preNextCandy.transform.localScale = new Vector3(0.2f, 0.2f, 2f);
        preNextCandy.GetComponent<SpriteRenderer>().sortingOrder = number;
        if (preNextCandy.tag == "good")
        {
            ParticleSystem goodEffect = preNextCandy.transform.GetChild(0).GetComponent<ParticleSystem>();
            goodEffect.Play(true);
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
