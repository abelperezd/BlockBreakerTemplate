using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI guessText;

    //public GameObject scoreManager;

    [Range(0.1f, 3f)] [SerializeField] float gameSpeed = 1f;

    [SerializeField] int lives = 3;
    private TMP_Text livesText;

    private TMP_Text scoreText;

    [SerializeField] int pointsPerBlockDestroyed = 5;
    [SerializeField] int currentScore = 0;

    [SerializeField] bool isAutoplayEnabled;

    private GameObject paddle;
    private GameObject ball;

    private bool ballMoving;

    private bool gameEnded = false;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<Game>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {
        Load();
    }

    private void OnLevelWasLoaded(int level)
    {
        Load();
    }

    private void Load()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "You Win")
            gameEnded = true;
        
        paddle = GameObject.Find("Paddle");
        ball = GameObject.Find("Ball");

        GameObject go;
        go = GameObject.Find("Lives Text");
        if (go)
            livesText = GameObject.Find("Lives Text").GetComponent<TMP_Text>();

        go = GameObject.Find("Score Text");
        if (go)
            scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();

        ballMoving = false;
        if (livesText)
            LivesTextUpdater();
        if (scoreText)
            UpdateScore();
        if (ball)
            ball.GetComponent<Ball>().RestartPosition(paddle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
        {
            Time.timeScale = gameSpeed;
            if (ballMoving == false && lives > 0)
            {
                ball.GetComponent<Ball>().RestartPosition(paddle);
                if (Input.GetMouseButtonDown(0))
                {
                    ballMoving = true;
                    ball.GetComponent<Ball>().StartMoving();
                }
            }
            if (lives <= 0)
            {
                GameObject.Find("Scene Loader").GetComponent<SceneLoader>().GameOver();
                gameEnded = true;
            }
        } 
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        UpdateScore();
    }

    private void UpdateScore() => scoreText.text = "SCORE: " + currentScore.ToString();


    public void RemoveLive()
    {
        lives -= 1;
        LivesTextUpdater();
        ball.GetComponent<Ball>().RestartPosition(paddle);
        ballMoving = false;
    }

    public void LivesTextUpdater()
    {
        livesText.text = "LIVES: " + lives;
    }

    
    public void RestartGame()
    {
        gameEnded = false;
        lives = 3;
        currentScore = 0;
    }

    public bool IsAutoplayEnabled()
    {
        return isAutoplayEnabled;
    }

}
