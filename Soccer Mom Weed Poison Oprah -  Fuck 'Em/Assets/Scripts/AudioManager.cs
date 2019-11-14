using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance;
    public static AudioSource menu;
    public static AudioSource store;
    public static AudioSource gameplay;
    public static AudioSource gameOver;
    public static AudioSource badSwipe;
    public static AudioSource goodSwipe;
    public static AudioSource levelFinish;
    public static AudioSource purchase;
    public static AudioSource slap;
    public static AudioSource UI;
    public static AudioSource premCandy;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponents<AudioSource>()[0];
        store = GetComponents<AudioSource>()[1];
        gameplay = GetComponents<AudioSource>()[2];
        gameOver = GetComponents<AudioSource>()[3];
        badSwipe = GetComponents<AudioSource>()[4];
        goodSwipe = GetComponents<AudioSource>()[5];
        levelFinish = GetComponents<AudioSource>()[6];
        purchase = GetComponents<AudioSource>()[7];
        slap = GetComponents<AudioSource>()[8];
        UI = GetComponents<AudioSource>()[9];
        premCandy = GetComponents<AudioSource>()[10];

        menu.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
