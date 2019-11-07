using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGenerator : MonoBehaviour
{
    public int number = 20;
    private int intialNumber;
    public int levelCandyIncrease = 5;
    public List<GameObject> candyList;
    private Queue<GameObject> candyBag = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (GameMetaInfo.Instance.CandiesInLevel == 0)
        {
            GameMetaInfo.Instance.CandiesInLevel = number;
        }
        else
        {
            number = GameMetaInfo.Instance.CandiesInLevel;
        }

        for (int i = 0; i < number - 1; i++)
        {
            int index = Random.Range(0, candyList.Count);
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            GameObject newCandy = Instantiate(candyList[index], GameObject.Find("CandyBag").transform);
            newCandy.transform.localPosition = offset;
            newCandy.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            candyBag.Enqueue(candyList[index]);
        }
        GameObject initCurCandy = Instantiate(candyList[Random.Range(0, candyList.Count)], GameObject.Find("CurrentCandy").transform);
        initCurCandy.GetComponent<SpriteRenderer>().sortingOrder = 2;
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

    public void RemoveCandyInBag()
    {
        GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
        Destroy(currentCandy);
        GameObject nextCandyInBag = GameObject.Find("CandyBag").transform.GetChild(0).gameObject;
        Destroy(nextCandyInBag);
        GameObject preNextCandy = GameObject.Find("NextCandy").transform.GetChild(0).gameObject;
        preNextCandy.transform.parent = GameObject.Find("CurrentCandy").transform;
        preNextCandy.transform.localPosition = new Vector3(0f, 0f, 0f);
        preNextCandy.transform.localScale = new Vector3(0.2f, 0.2f, 2f);
        preNextCandy.GetComponent<SpriteRenderer>().sortingOrder = 2;
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

    public void RemoveLastInBag()
    {
        GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
        Destroy(currentCandy);
        GameObject nextCandyInBag = GameObject.Find("CandyBag").transform.GetChild(0).gameObject;
        Destroy(nextCandyInBag);
        GameObject preNextCandy = GameObject.Find("NextCandy").transform.GetChild(0).gameObject;
        preNextCandy.transform.parent = GameObject.Find("CurrentCandy").transform;
        preNextCandy.transform.localPosition = new Vector3(0f, 0f, 0f);
        preNextCandy.transform.localScale = new Vector3(0.2f, 0.2f, 2f);
        preNextCandy.GetComponent<SpriteRenderer>().sortingOrder = 2;
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

    public void RemoveLast()
    {
        GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
        Destroy(currentCandy);

        GameMetaInfo.Instance.CandiesInLevel = GameMetaInfo.Instance.CandiesInLevel + levelCandyIncrease;
        GameState.Instance.CompleteLevel();
    }
}
