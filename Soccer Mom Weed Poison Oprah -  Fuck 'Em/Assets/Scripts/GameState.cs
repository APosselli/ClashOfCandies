using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameObject scoreTextObject;
    public GameObject livesTextObject;
    public GameObject candyTextObject; // TODO: Remove when we add art assets
    public GameObject gameOverTextObject;
    private UnityEngine.UI.Text scoreText;
    private UnityEngine.UI.Text livesText;
    private UnityEngine.UI.Text candyText;

    private static GameState globalInstance;
    private int score = 0;
    private int lives = 3;
    private bool waitingForRelease = false;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextObject.GetComponent<UnityEngine.UI.Text>();
        livesText = livesTextObject.GetComponent<UnityEngine.UI.Text>();
        candyText = candyTextObject.GetComponent<UnityEngine.UI.Text>();

        globalInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
            return;

        if (!FingerInput.GetInputPresent())
            return;

        if (FingerInput.GetFingerPressed())
            waitingForRelease = true;

        if (waitingForRelease && FingerInput.GetFingerReleased())
            ResetLevel();
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

        scoreText.text = "Score: " + score.ToString();
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "Non-fatal mistakes remaining: " + lives.ToString();
        if (lives <= 0)
            InvokeGameOver();
    }

    public void SetCurrentCandy(bool isPoison)
    {
        if (isPoison)
            candyText.text = "Candy: Poison";
        else
            candyText.text = "Candy: Edible";
    }

    public void InvokeGameOver()
    {
        gameOver = true;
        gameOverTextObject.SetActive(true);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool GameOver
    {
        get { return gameOver; }
    }
}
