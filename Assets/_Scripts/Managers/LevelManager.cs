using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;

    public GameObject pauseMenu;

    private float startTime;
    [SerializeField]
    private float silverTime;
    [SerializeField]
    private float goldTime;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        pauseMenu.SetActive(false);
        startTime = Time.time;
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

    public void EventVictory()
    {
        float duration = Time.time - startTime;

        if (duration < goldTime)
            GameManager.Instance.currency += 50;
        else if (duration < silverTime)
            GameManager.Instance.currency += 25;
        else
            GameManager.Instance.currency += 10;

        GameManager.Instance.SavePlayerPrefs();

        string saveString = "";
        LevelData level = new LevelData(SceneManager.GetActiveScene().name);

        saveString += (level.BestTime > duration || level.BestTime == 0.0f) ? duration.ToString() : level.BestTime.ToString();
        saveString += '&';
        saveString += silverTime.ToString();
        saveString += '&';
        saveString += goldTime.ToString();
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);
        SceneManager.LoadScene(SceneTags.MAIN_MENU);
    }
}
