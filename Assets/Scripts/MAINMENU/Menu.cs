using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void QuitGame()
    {
        Application.Quit(); // for built game
        Debug.Log("Game Quit!"); // testing in Editor
    }
}
