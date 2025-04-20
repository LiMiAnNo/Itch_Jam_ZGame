using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f; // Time in seconds
    public Text timerText;            // UI Text for displaying the timer
    public GameObject gameOverUI;     // UI object to show when time ends
    public string mainMenuSceneName = "MainMenu"; // The name of your main menu scene

    private bool timerIsRunning = true;

    void Start()
    {
        gameOverUI.SetActive(false);
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                EndGame();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        Debug.Log("Time's up!");
        gameOverUI.SetActive(true); // Show Game Over UI
        Time.timeScale = 0f;        // Pause game

        // Start coroutine to wait 3 seconds before transitioning
        StartCoroutine(WaitAndReturnToMainMenu());
    }

    IEnumerator WaitAndReturnToMainMenu()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        
        // Optionally, load a Game Over scene:
        // SceneManager.LoadScene("GameOverScene");
        
        // Or load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}