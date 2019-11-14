using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMetaInfo : MonoBehaviour
{
    static private GameMetaInfo metaInfoInstance;
    private int player1PremiumCandy = 1;
    private int player2PremiumCandy = 1;
    private bool isPlayer1 = true;
    private string player1Name = "Player 1";
    private string player2Name = "Player 2";
    public List<GameObject> prefabList;
    public static List<GameObject> candyList = new List<GameObject>();
    private int player1Money = 0;
    private int player2Money = 0;


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
        foreach (GameObject prefab in prefabList)
        {
            candyList.Add(prefab);
        }
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

    public void SetPlayer1Name(string name)
    {
        player1Name = name;
    }

    public void SetPlayer2Name(string name)
    {
        player2Name = name;
    }

    public int Money
    {
        get
        {
            if (isPlayer1)
                return player1Money;
            else
                return player2Money;
        }

        set
        {
            if (isPlayer1)
                player1Money = value;
            else
                player2Money = value;
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

    public static void StartGame()
    {
        Instance.isPlayer1 = true;
        SceneManager.LoadScene("Selection");
    }
}
