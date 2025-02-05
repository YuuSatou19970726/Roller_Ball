using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void TogglePaseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(SceneTags.MAIN_MENU);
    }

    public void EventResume()
    {
        pauseMenu.SetActive(false);
    }
}
