using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private float roundTime;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text gameOverPointsText;
    [SerializeField]
    private Text gameOverHighscoreText;
    [SerializeField]
    private Text gameOverStarText;
    [SerializeField]
    private GameObject gameOverIsHighScore;
    private PackageManager packageManager;
    private StarManager starManager;
    private void Start()
    {
        packageManager = GameObject.FindWithTag("Player").GetComponent<PackageManager>();
        starManager = GameObject.FindGameObjectWithTag("StarManager").GetComponent<StarManager>();
        Time.timeScale = 1;
    }
    void Update()
    {
        roundTime -= Time.deltaTime;
        timeText.text = roundTime < 10 ? roundTime.ToString("F1") : roundTime.ToString("F0");
        if (roundTime <= 0)
        {
            HandleGameOver();
        }
    }
    void HandleGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        gameOverPointsText.text = "Points: " + packageManager.GetDeliveredPackagesCount();
        PlayerPrefs.SetInt("HighScore", Mathf.Max(PlayerPrefs.GetInt("HighScore"), packageManager.GetDeliveredPackagesCount()));
        gameOverStarText.text = "Stars: " + starManager.GetStarLevel();
        gameOverHighscoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
        gameOverIsHighScore.SetActive(PlayerPrefs.GetInt("HighScore") == packageManager.GetDeliveredPackagesCount());
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
