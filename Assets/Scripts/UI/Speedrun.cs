using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Speedrun : MonoBehaviour
{
    private static Stopwatch stopWatch = new Stopwatch();
    private bool isStarted = false;

    GameObject textUI;

    private static Speedrun _instance = null;
    public static Speedrun Instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {

    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 9)
        {
            Destroy(this.gameObject);
        }

        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().buildIndex < 9)
        {
            if (!isStarted)
            {
                stopWatch.Start();
                isStarted = true;
            }
        }
        else
        {
            if (isStarted)
            {
                stopWatch.Stop();
                stopWatch.Reset();
                isStarted = false;
            }
        }

        if (textUI != null)
        {
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}",
                ts.Minutes, ts.Seconds);
            var text = textUI.GetComponent<Text>();
            text.text = "TIME: " + elapsedTime;
            Stats.Time = elapsedTime;
        }
        else
        {
            textUI = GameObject.FindGameObjectWithTag("SpeedrunUI");
        }
    }

    public static void RestartTimer()
    {
        stopWatch.Start();
    }

    public static void StopTimer()
    {
        stopWatch.Stop();
    }
}