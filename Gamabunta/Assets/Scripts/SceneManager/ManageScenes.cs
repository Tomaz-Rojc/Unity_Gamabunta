using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void GoInGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToInfo()
    {
        SceneManager.LoadScene("Info");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }
}
