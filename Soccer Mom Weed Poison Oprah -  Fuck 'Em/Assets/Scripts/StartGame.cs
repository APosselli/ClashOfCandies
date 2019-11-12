using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public UnityEngine.UI.Text player1Text;
    public UnityEngine.UI.Text player2Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        GameMetaInfo.Instance.SetPlayer1();
        GameMetaInfo.Instance.SetPlayer1Name(player1Text.text);
        if (GameMetaInfo.Instance.PlayerName == "")
        {
            GameMetaInfo.Instance.SetPlayer1Name("Player 1");
        }

        GameMetaInfo.Instance.SetPlayer2Name(player2Text.text);
        if (GameMetaInfo.Instance.OtherPlayerName == "")
        {
            GameMetaInfo.Instance.SetPlayer2Name("Player 2");
        }

        GameMetaInfo.StartGame();
    }
}
