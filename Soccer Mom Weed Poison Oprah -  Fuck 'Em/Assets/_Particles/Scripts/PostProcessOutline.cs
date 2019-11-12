using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessOutline : MonoBehaviour
{
    private GameObject[] goodObjs = null;
    private GameObject[] badObjs = null;

    public Color goodEdgeColor = Color.red;
    public Color badEdgeColor = Color.red;
    public float alphaThreshold = 0.1f;
    public float offsetUV = 0.01f;
    public Shader shader;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        goodObjs = GameObject.FindGameObjectsWithTag("good");
        Material replacematGood = new Material(Shader.Find("Custom/WithOutlineShader"));
        replacematGood.SetFloat("_OffsetUV", offsetUV);
        replacematGood.SetFloat("_AlphaThreshold", alphaThreshold);
        replacematGood.SetColor("_EdgeColor", goodEdgeColor);
        badObjs = GameObject.FindGameObjectsWithTag("bad");
        Material replacematBad = new Material(Shader.Find("Custom/WithOutlineShader"));
        replacematBad.SetFloat("_OffsetUV", offsetUV);
        replacematBad.SetFloat("_AlphaThreshold", alphaThreshold);
        replacematBad.SetColor("_EdgeColor", goodEdgeColor);

        if (replacematGood != null)
        {
            foreach( var obj in goodObjs)
            {
                obj.GetComponent<SpriteRenderer>().material = replacematGood;
            }  
        }
        if (replacematBad != null)
        {
            foreach (var obj in badObjs)
            {
                obj.GetComponent<SpriteRenderer>().material = replacematBad;
            }
        }
    }
}
