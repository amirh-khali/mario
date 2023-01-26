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
    public Text totalCoin;
    public Text highestLevel;

    public Image mainMenuBackground;
    public GameObject startButton;
    public GameObject livesBoard;
    public GameObject coinsBoard;

    public AudioSource backgroundAudio;
    public AudioSource missAudio;
    public AudioSource gameOverAudio;
    public AudioSource nextLevelAudio;

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
        totalCoin.text = "Total Coin: " + PlayerPrefs.GetInt("total_coin", 0);
        highestLevel.text = "Highest Level: " + PlayerPrefs.GetInt("highest_level", 0);
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
        totalCoin.enabled = false;
        highestLevel.enabled = false;
    }

    public void GameOver()
    {
        backgroundAudio.Stop();
        gameOverAudio.Play();

        livesBoard.SetActive(false);
        coinsBoard.SetActive(false);
        startButton.SetActive(true);
        mainMenuBackground.enabled = true;
        totalCoin.enabled = true;
        highestLevel.enabled = true;

        totalCoin.text = "Total Coin: " + PlayerPrefs.GetInt("total_coin", 0);
        highestLevel.text = "Highest Level: " + PlayerPrefs.GetInt("highest_level", 0);

        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel(int stage)
    {
        this.stage = stage;

        if (backgroundAudio.isPlaying)
            backgroundAudio.Stop();
        backgroundAudio.Play();
        SceneManager.LoadScene($"Stage_{stage}");
    }

    public void NextLevel()
    {
        if (stage < NumberOfStages)
        {
            PlayerPrefs.SetInt("highest_level", Mathf.Max(PlayerPrefs.GetInt("highest_level", 0), stage));
            LoadLevel(stage + 1);
        }
        else
            GameOver();
    }

    public void ResetLevel(float delay)
    {
        backgroundAudio.Stop();
        missAudio.Play();
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
        PlayerPrefs.SetInt("total_coin", PlayerPrefs.GetInt("total_coin", 0) + coins);

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
