using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameObject scoreTextObject;
    public GameObject livesTextObject;
   public GameObject slapTextObject;
    public GameObject levelTextObject;
    public GameObject gameOverTextObject;
    public GameObject premCandyTextObject;
    public float rewardEndTime = 40f;
    public float candiesPerSecond = 0.5f;
    private UnityEngine.UI.Text scoreText;
    private UnityEngine.UI.Text livesText;
    private UnityEngine.UI.Text levelText;
    private UnityEngine.UI.Text gameOverText;
    private static UnityEngine.UI.Text slapText;
    private static UnityEngine.UI.Text premCandyText;

    private static GameState globalInstance;
    private int score = 0;
    private int lives = 3;
    public static int slaps;
    private float timeElapsed = 0f;
    private bool waitingForRelease = false;
    private bool gameOver = false;
    private bool betweenLevels = true;
    public static bool levelFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        slaps = 3;
        scoreText = scoreTextObject.GetComponent<UnityEngine.UI.Text>();
        livesText = livesTextObject.GetComponent<UnityEngine.UI.Text>();
        levelText = levelTextObject.GetComponent<UnityEngine.UI.Text>();
        gameOverText = gameOverTextObject.GetComponent<UnityEngine.UI.Text>();
        slapText = slapTextObject.GetComponent<UnityEngine.UI.Text>();
        premCandyText = premCandyTextObject.GetComponent<UnityEngine.UI.Text>();

        levelText.text = GameMetaInfo.Instance.PlayerName + "'s turn.\nTap to begin...";
        gameOverText.text = "";

        globalInstance = this;
        Time.timeScale = 0f;

        scoreText.text = "Candies sorted: 0";
        premCandyText.text = "x " + GameMetaInfo.Instance.PremiumCandy.ToString();
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
            /*if ((betweenLevels && levelFinished) || gameOver)
                ResetLevel();*/
            if (betweenLevels)
            {
                levelText.text = "";
                waitingForRelease = false;
                betweenLevels = false;
                scoreText.text = "Candies sorted: " + score.ToString() + "/" + GameMetaInfo.Instance.CandiesInLevel;
                Time.timeScale = 1f;
                AudioManager.menu.Stop();
                AudioManager.gameplay.Play();
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

    public static void UpdatePremiumCandy()
    {
        int premCandyNum = GameMetaInfo.Instance.PremiumCandy;
        premCandyText.text = "x " + premCandyNum.ToString();
    }

    public static void UpdateSlap()
    {
        slaps--;
        slapText.text = "Remaining Slaps: " + slaps.ToString();
    }

    public void InvokeGameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
        AudioManager.gameplay.Stop();
        AudioManager.gameOver.Play();
        AudioManager.menu.Play();
        gameOverText.text = "Game Over!\n" + GameMetaInfo.Instance.OtherPlayerName + " Wins!";
        GameObject.Find("Canvas").transform.Find("RetryButton").gameObject.SetActive(true);
        //GameMetaInfo.Instance.CandiesInLevel = 0;
        //GameMetaInfo.Instance.SetPlayer1();
    }

    public void CompleteLevel()
    {
        levelText.text = "Level Complete!";
        Time.timeScale = 0f;
        AudioManager.gameplay.Stop();
        AudioManager.levelFinish.Play();
        AudioManager.menu.Play();
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
        set { gameOver = value; }
    }

    public int GetCandiesToAdd()
    {
        float timeRemaining = rewardEndTime - timeElapsed;
        if (timeRemaining < 0)
            timeRemaining = 0f;

        return (int)(timeRemaining * candiesPerSecond);
    }
}
