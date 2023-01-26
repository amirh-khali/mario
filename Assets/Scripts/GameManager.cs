using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    public Text livesText;
    public Text coinsText;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;


        NewGame();
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;

        LoadLevel(1);
    }

    public void GameOver()
    {
        // TODO: show game over screen

        NewGame();
    }

    public void LoadLevel(int stage)
    {
        this.stage = stage;

        SceneManager.LoadScene($"Stage_{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0)
        {
            LoadLevel(stage);
        }
        else
        {
            GameOver();
        }
    }

    public void AddCoin()
    {
        coins++;

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
        lives++;
    }

    private void UpdateUI()
    {
        coinsText.text = coins.ToString();
        livesText.text = lives.ToString();
    }

}
