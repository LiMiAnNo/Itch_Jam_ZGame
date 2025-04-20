using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverManager : MonoBehaviour
{
    public GameObject loseText; // Assign in Inspector
    public float gameTime = 60f; // Set the timer duration (60 seconds)
    public string mainMenuSceneName = "MainMenu"; // The name of your main menu scene
    private float timer;

    void Start()
    {
        timer = gameTime; // Start the timer at 60 seconds
        loseText.SetActive(false); // Hide lose text initially
    }

    void Update()
    {
        // Update timer and check if the time has elapsed
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Decrease timer
        }
        else
        {
            // If timer reaches 0, show "You Lose" text and go to main menu after 3 seconds
            if (!loseText.activeSelf)
            {
                StartCoroutine(ShowLoseScreenAndReturn());
            }
        }
    }

    IEnumerator ShowLoseScreenAndReturn()
    {
        loseText.SetActive(true); // Show the "You Lose" text
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        SceneManager.LoadScene(mainMenuSceneName); // Load the main menu scene
    }
}