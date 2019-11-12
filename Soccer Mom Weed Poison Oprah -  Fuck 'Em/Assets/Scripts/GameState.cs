using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameObject scoreTextObject;
    public GameObject livesTextObject;
   // public GameObject candyTextObject;
    public GameObject levelTextObject;
    public GameObject gameOverTextObject;
    public float rewardEndTime = 40f;
    public float candiesPerSecond = 0.5f;
    private UnityEngine.UI.Text scoreText;
    private UnityEngine.UI.Text livesText;
    private UnityEngine.UI.Text levelText;
    private UnityEngine.UI.Text gameOverText;
    // private UnityEngine.UI.Text candyText;

    private static GameState globalInstance;
    private int score = 0;
    private int lives = 3;
    private float timeElapsed = 0f;
    private bool waitingForRelease = false;
    private bool gameOver = false;
    private bool betweenLevels = true;
    public static bool levelFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextObject.GetComponent<UnityEngine.UI.Text>();
        livesText = livesTextObject.GetComponent<UnityEngine.UI.Text>();
        levelText = levelTextObject.GetComponent<UnityEngine.UI.Text>();
        gameOverText = gameOverTextObject.GetComponent<UnityEngine.UI.Text>();
        //candyText = candyTextObject.GetComponent<UnityEngine.UI.Text>();

        levelText.text = GameMetaInfo.Instance.PlayerName + "'s turn.\nTap to begin...";
        gameOverText.text = "";

        globalInstance = this;
        Time.timeScale = 0f;

        scoreText.text = "Candies sorted: 0";
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (!gameOver && !betweenLevels)
            return;

        if (!FingerInput.GetInputPresent())
            return;

        if (FingerInput.GetFingerPressed())
            waitingForRelease = true;

        if (waitingForRelease && FingerInput.GetFingerReleased())
        {
            if ((betweenLevels && levelFinished) || gameOver)
                ResetLevel();
            else if (betweenLevels)
            {
                levelText.text = "";
                waitingForRelease = false;
                betweenLevels = false;
                scoreText.text = "Candies sorted: " + score.ToString() + "/" + GameMetaInfo.Instance.CandiesInLevel;
                Time.timeScale = 1f;
            }
        }
    }

    public static GameState Instance
    {
        get { return globalInstance; }
    }

    public void AddToScore(int points)
    {
        score += points;
        if (score > 999999999 || score < 0)
        {
            score = 999999999;
        }

        scoreText.text = "Candies sorted: " + score.ToString() + "/" + GameMetaInfo.Instance.CandiesInLevel;
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "Allowed Mistakes: " + lives.ToString();
        if (lives <= 0)
            InvokeGameOver();
    }

    /*public void SetCurrentCandy(bool isPoison)
    {
        if (isPoison)
            candyText.text = "Candy: Poison";
        else
            candyText.text = "Candy: Edible";
    }*/

    public void InvokeGameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over!\n" + GameMetaInfo.Instance.OtherPlayerName + " Wins!";
        GameMetaInfo.Instance.CandiesInLevel = 0;
        GameMetaInfo.Instance.SetPlayer1();
        Time.timeScale = 0f;
    }

    public void CompleteLevel()
    {
        levelText.text = "Level Complete!";
        Time.timeScale = 0f;
        GameObject.Find("Canvas").transform.Find("StoreButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("FacebookLoginButton").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("FacebookShareButton").gameObject.SetActive(true);
    }

    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool BetweenLevels
    {
        get { return betweenLevels; }
    }

    public bool GameOver
    {
        get { return gameOver; }
    }

    public int GetCandiesToAdd()
    {
        float timeRemaining = rewardEndTime - timeElapsed;
        if (timeRemaining < 0)
            timeRemaining = 0f;

        return (int)(timeRemaining * candiesPerSecond);
    }
}
