using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyScript EnemyScript;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public int score = 0;
    public int highScore = 0;

    public bool isStage1 = true;
    public bool isStage2 = false;
    public bool isStage3 = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        highScoreText.text = highScore.ToString();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetStage(2);
            RestartStage();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        scoreText.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        Debug.Log("Score: " + score + " | High Score: " + highScore);
    }

    public void SetStage(int stage)
    {
        isStage1 = (stage == 1);
        isStage2 = (stage == 2);
        isStage3 = (stage == 3);
    }

    public void RestartStage()
    {
        EnemyScript.SpawnEnemies();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
