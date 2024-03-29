﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGenerator : MonoBehaviour
{
    public int number = 20;
    private Queue<GameObject> candyBag = new Queue<GameObject>();
    public Shader shader;
    public Color goodEdgeColor;
    public Color badEdgeColor;
    public float alphaThreshold = 0.1f;
    public float offsetUV = 0.01f;
    private Material replacematGood;
    private Material replacematBad;
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

        replacematGood = new Material(shader);
        replacematGood.SetFloat("_OffsetUV", offsetUV);
        replacematGood.SetFloat("_AlphaThreshold", alphaThreshold);
        replacematGood.SetColor("_EdgeColor", goodEdgeColor);
        replacematBad = new Material(shader);
        replacematBad.SetFloat("_OffsetUV", offsetUV);
        replacematBad.SetFloat("_AlphaThreshold", alphaThreshold);
        replacematBad.SetColor("_EdgeColor", badEdgeColor);

        for (int i = 0; i < number - 1; i++)
        {
            int index = Random.Range(0, GameMetaInfo.candyList.Count);
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            GameObject newCandy = Instantiate(GameMetaInfo.candyList[index], GameObject.Find("CandyBag").transform);
            newCandy.transform.localPosition = offset;
            newCandy.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            newCandy.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            newCandy.GetComponent<SpriteRenderer>().sortingOrder = number - 1 - i;
            candyBag.Enqueue(GameMetaInfo.candyList[index]);
        }
        GameObject initCurCandy = Instantiate(GameMetaInfo.candyList[Random.Range(0, GameMetaInfo.candyList.Count)], GameObject.Find("CurrentCandy").transform);
        initCurCandy.GetComponent<SpriteRenderer>().sortingOrder = number;
        initCurCandy.transform.Find("highlight").gameObject.SetActive(true);
        initCurCandy.transform.Find("highlight").gameObject.GetComponent<SpriteRenderer>().sortingOrder = number;
        if (initCurCandy.tag == "good")
        {
            initCurCandy.GetComponent<SpriteRenderer>().material = replacematGood;
        }
        else if (initCurCandy.tag == "bad")
        {
            initCurCandy.GetComponent<SpriteRenderer>().material = replacematBad;
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
        preNextCandy.GetComponent<SpriteRenderer>().sortingOrder = number;
        preNextCandy.transform.Find("highlight").gameObject.SetActive(true);
        preNextCandy.transform.Find("highlight").gameObject.GetComponent<SpriteRenderer>().sortingOrder = number;
        if (preNextCandy.tag == "good")
        {
            preNextCandy.GetComponent<SpriteRenderer>().material = replacematGood;
        }
        else if (preNextCandy.tag == "bad")
        {
            preNextCandy.GetComponent<SpriteRenderer>().material = replacematBad;
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
        preNextCandy.GetComponent<SpriteRenderer>().sortingOrder = number;
        preNextCandy.transform.Find("highlight").gameObject.SetActive(true);
        preNextCandy.transform.Find("highlight").gameObject.GetComponent<SpriteRenderer>().sortingOrder = number;
        if (preNextCandy.tag == "good")
        {
            preNextCandy.GetComponent<SpriteRenderer>().material = replacematGood;
        }
        else if (preNextCandy.tag == "bad")
        {
            preNextCandy.GetComponent<SpriteRenderer>().material = replacematBad;
        }
    }

    public void RemoveLast()
    {
        GameObject currentCandy = GameObject.Find("CurrentCandy").transform.GetChild(0).gameObject;
        Destroy(currentCandy);
        GameState.Instance.CompleteLevel();
        GameMetaInfo.Instance.CandiesInLevel += GameState.Instance.GetCandiesToAdd();
    }
}
