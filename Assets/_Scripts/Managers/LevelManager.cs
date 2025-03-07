using System.Collections;
using System.Collections.Generic;
using RollerBall;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : CustomMonobehaviour
{
    private const float TIME_BEFORE_START = 3.0f;

    private static LevelManager instance;
    public static LevelManager Instance => instance;

    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject endMenu;
    [SerializeField]
    private Text endTimerText;
    [SerializeField]
    private Transform respawnSpoint;
    [SerializeField]
    private Text timerText;
    private GameObject player;

    private float startTime;
    private float levelDuration;
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
        this.endMenu.SetActive(false);
        this.startTime = Time.time;

        this.player.transform.position = this.respawnSpoint.position;
    }

    protected override void Update()
    {
        if (Time.time - this.startTime < TIME_BEFORE_START)
            return;

        if (player.transform.position.y < -10.0f)
            GameOver();

        UpdateTimer();
    }

    protected override void LoadComponents()
    {
        LoadGameObjects();
    }

    #region Private
    private void LoadGameObjects()
    {
        this.player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }

    private void UpdateTimer()
    {
        this.levelDuration = Time.time - (startTime + TIME_BEFORE_START);
        string minutes = ((int)this.levelDuration / 60).ToString("00");
        string seconds = (this.levelDuration % 60).ToString("00.00");
        this.timerText.text = minutes + ":" + seconds;
    }
    #endregion

    #region public


    public void TogglePaseMenu()
    {
        this.pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = this.pauseMenu.activeSelf ? 0 : 1;
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneTags.MAIN_MENU);
    }

    public void EventResume()
    {
        Time.timeScale = 1;
        this.pauseMenu.SetActive(false);
    }

    public void EventRestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EventVictory()
    {
        if (GameManager.Instance == null) return;

        foreach (Transform transform in this.endMenu.transform.parent)
        {
            transform.gameObject.SetActive(false);
        }

        this.endMenu.SetActive(true);
        Rigidbody rigidbody = this.player.GetComponent<Rigidbody>();
        // rigidbody.velocity = Vector3.zero;
        // rigidbody.angularVelocity = Vector3.zero;
        // rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePosition;

        this.endTimerText.text = this.timerText.text;

        if (this.levelDuration < goldTime)
        {
            GameManager.Instance.currency += 50;
            this.endTimerText.color = Color.yellow;
        }
        else if (this.levelDuration < silverTime)
        {
            GameManager.Instance.currency += 25;
            this.endTimerText.color = Color.gray;
        }
        else
        {
            GameManager.Instance.currency += 10;
            this.endTimerText.color = new Color(0.8f, 0.5f, 0.2f, 1.0f);
        }

        GameManager.Instance.SavePlayerPrefs();

        string saveString = "";
        LevelData level = new LevelData(SceneManager.GetActiveScene().name);

        saveString += (level.BestTime > this.levelDuration || level.BestTime == 0.0f)
        ? this.levelDuration.ToString()
        : level.BestTime.ToString();

        saveString += '&';
        saveString += silverTime.ToString();
        saveString += '&';
        saveString += goldTime.ToString();
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);
    }

    public void GameOver()
    {
        // player.transform.position = this.respawnSpoint.position;
        // Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        // rigidbody.velocity = Vector3.zero;
        // rigidbody.angularVelocity = Vector3.zero;
        EventRestartLevel();
    }
    #endregion
}
