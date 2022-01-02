using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(!pauseMenu.activeSelf) {
                EnablePause();
            } else {
                DisablePause();
            }
        }
    }

    public void EnablePause() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisablePause() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
