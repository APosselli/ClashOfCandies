using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGenerator : MonoBehaviour
{
    public int number = 20;
    public int levelCandyIncrease = 5;
    public List<GameObject> candyList;
    private Queue<GameObject> candyBag = new Queue<GameObject>();
    private int premCandyNum;
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

        /*replacematGood = new Material(Shader.Find("Custom/WithOutlineShader"));
        replacematGood.SetFloat("_OffsetUV", offsetUV);
        replacematGood.SetFloat("_AlphaThreshold", alphaThreshold);
        replacematGood.SetColor("_EdgeColor", goodEdgeColor);
        replacematBad = new Material(Shader.Find("Custom/WithOutlineShader"));
        replacematBad.SetFloat("_OffsetUV", offsetUV);
        replacematBad.SetFloat("_AlphaThreshold", alphaThreshold);
        replacematBad.SetColor("_EdgeColor", badEdgeColor);*/

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
        //initCurCandy.transform.Find("highlight").gameObject.SetActive(true);
        //initCurCandy.transform.Find("highlight").gameObject.GetComponent<SpriteRenderer>().sortingOrder = number - 1;
        if (initCurCandy.tag == "good")
        {
            //initCurCandy.GetComponent<SpriteRenderer>().material = replacematGood;
            ParticleSystem goodEffect = initCurCandy.transform.GetChild(0).GetComponent<ParticleSystem>();
            goodEffect.Play(true);
            //GameState.Instance.SetCurrentCandy(false);
        }
        else if (initCurCandy.tag == "bad")
        {
            //initCurCandy.GetComponent<SpriteRenderer>().material = replacematBad;
            ParticleSystem badEffect = initCurCandy.transform.GetChild(1).GetComponent<ParticleSystem>();
            badEffect.Play(true);
            //GameState.Instance.SetCurrentCandy(true);
        }
        GameObject initNextCandy = Instantiate(candyBag.Dequeue(), GameObject.Find("NextCandy").transform);
        initNextCandy.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        premCandyNum = GameMetaInfo.Instance.PremiumCandy;
        for (int i = 0; i < GameMetaInfo.maxPremiumCandy; i++)
        {
            if (i > premCandyNum - 1)
            {
                GameObject.Find("PremiumCandy").transform.GetChild(i).gameObject.SetActive(false);
            }
        }
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
        //preNextCandy.transform.Find("highlight").gameObject.SetActive(true);
        //preNextCandy.transform.Find("highlight").gameObject.GetComponent<SpriteRenderer>().sortingOrder = number - 1;
        if (preNextCandy.tag == "good")
        {
            //preNextCandy.GetComponent<SpriteRenderer>().material = replacematGood;
            ParticleSystem goodEffect = preNextCandy.transform.GetChild(0).GetComponent<ParticleSystem>();
            goodEffect.Play(true);
            //GameState.Instance.SetCurrentCandy(false);
        }
        else if (preNextCandy.tag == "bad")
        {
            //preNextCandy.GetComponent<SpriteRenderer>().material = replacematBad;
            ParticleSystem badEffect = preNextCandy.transform.GetChild(1).GetComponent<ParticleSystem>();
            badEffect.Play(true);
            //GameState.Instance.SetCurrentCandy(true);
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
        //preNextCandy.transform.Find("highlight").gameObject.SetActive(true);
        //preNextCandy.transform.Find("highlight").gameObject.GetComponent<SpriteRenderer>().sortingOrder = number - 1;
        if (preNextCandy.tag == "good")
        {
            //preNextCandy.GetComponent<SpriteRenderer>().material = replacematGood;
            ParticleSystem goodEffect = preNextCandy.transform.GetChild(0).GetComponent<ParticleSystem>();
            goodEffect.Play(true);
            //GameState.Instance.SetCurrentCandy(false);
        }
        else if (preNextCandy.tag == "bad")
        {
            //preNextCandy.GetComponent<SpriteRenderer>().material = replacematBad;
            ParticleSystem badEffect = preNextCandy.transform.GetChild(1).GetComponent<ParticleSystem>();
            badEffect.Play(true);
            //GameState.Instance.SetCurrentCandy(true);
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
