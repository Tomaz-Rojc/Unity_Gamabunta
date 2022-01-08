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

        GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
        foreach (GameObject fire in fires) {
            fire.GetComponent<AudioSource>().Stop();
        }

        GameObject[] fires2 = GameObject.FindGameObjectsWithTag("SmallFire");
        foreach (GameObject fire in fires2) {
            fire.transform.GetChild(2).GetComponent<AudioSource>().Stop();
        }
    }

    public void DisablePause() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

        GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
        foreach (GameObject fire in fires) {
            fire.GetComponent<AudioSource>().Play();
        }

        GameObject[] fires2 = GameObject.FindGameObjectsWithTag("SmallFire");
        foreach (GameObject fire in fires2) {
            fire.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
    }
}
