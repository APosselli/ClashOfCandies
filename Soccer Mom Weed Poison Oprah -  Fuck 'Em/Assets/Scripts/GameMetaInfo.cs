﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetaInfo : MonoBehaviour
{
    static private GameMetaInfo metaInfoInstance;
    private int player1PremiumCandy;
    private int player2PremiumCandy;
    private bool isPlayer1 = true;
    private string player1Name = "Player 1";
    private string player2Name = "Player 2";

    void Awake()
    {
        if (metaInfoInstance == null)
        {
            metaInfoInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public GameMetaInfo Instance
    {
        get { return metaInfoInstance; }
    }

    public int PremiumCandy
    {
        get
        {
            if (isPlayer1)
                return player1PremiumCandy;
            else
                return player2PremiumCandy;
        }

        set
        {
            if (isPlayer1)
                player1PremiumCandy = value;
            else
                player2PremiumCandy = value;
        }
    }

    public string PlayerName
    {
        get
        {
            if (isPlayer1)
                return player1Name;
            else
                return player2Name;
        }
    }

    public string OtherPlayerName
    {
        get
        {
            if (!isPlayer1)
                return player1Name;
            else
                return player2Name;
        }
    }

    public void SwitchPlayer()
    {
        isPlayer1 = !isPlayer1;
    }

    public void SetPlayer1()
    {
        isPlayer1 = true;
    }

    public int CandiesInLevel
    {
        get; set;
    }
}