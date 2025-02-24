using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : CustomMonobehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;

    public GameObject pauseMenu;
    [SerializeField]
    private Transform respawnSpoint;
    private GameObject player;

    private float startTime;
    [SerializeField]
    private float silverTime;
    [SerializeField]
    private float goldTime;

    protected override void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        this.pauseMenu.SetActive(false);
        this.startTime = Time.time;

        this.player.transform.position = this.respawnSpoint.position;
    }

    protected override void Update()
    {
        if (player.transform.position.y < -10.0f)
            GameOver();
    }

    protected override void LoadComponents()
    {
        LoadGameObjects();
    }

    private void LoadGameObjects()
    {
        this.player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }

    public void TogglePaseMenu()
    {
        this.pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(SceneTags.MAIN_MENU);
    }

    public void EventResume()
    {
        this.pauseMenu.SetActive(false);
    }

    public void EventVictory()
    {
        if (GameManager.Instance == null) return;

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

    public void GameOver()
    {
        player.transform.position = this.respawnSpoint.position;
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
