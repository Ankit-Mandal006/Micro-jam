using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;
    // Call this from the Play button
    public void PlayGame()
    {
        // Load next scene in Build Settings (index 1)
        SceneManager.LoadScene(1);
        Time.timeScale = 1f; // just in case
    }

    // Call this from the Exit/Quit button
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void Credits()
    {
        credits.SetActive(true);
    }
    public void Back()
    {
        credits.SetActive(false);
    }
}
