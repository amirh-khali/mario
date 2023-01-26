using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int NumberOfStages = 2;

    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    public Text livesText;
    public Text coinsText;

    public Image mainMenuBackground;
    public GameObject startButton;
    public GameObject livesBoard;
    public GameObject coinsBoard;

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

    public void StartGame()
    {
        Application.targetFrameRate = 60;

        NewGame();
    }

    public void NewGame()
    {
        lives = 3;
        coins = 0;
        UpdateUI();

        LoadLevel(1);

        livesBoard.SetActive(true);
        coinsBoard.SetActive(true);
        startButton.SetActive(false);
        mainMenuBackground.enabled = false;
    }

    public void GameOver()
    {
        // TODO: show game over screen

        SceneManager.LoadScene("MainMenu");
        livesBoard.SetActive(false);
        coinsBoard.SetActive(false);
        startButton.SetActive(true);
        mainMenuBackground.enabled = true;
    }

    public void LoadLevel(int stage)
    {
        this.stage = stage;

        SceneManager.LoadScene($"Stage_{stage}");
    }

    public void NextLevel()
    {
        if (stage < NumberOfStages)
            LoadLevel(stage + 1);
        else
            GameOver();
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;
        UpdateUI();

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
        UpdateUI();

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
        lives++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinsText.text = coins.ToString();
        livesText.text = lives.ToString();
    }

}
