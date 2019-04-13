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

    // Play Global
    private static Speedrun instance = null;
    private static Speedrun Instance
    {
        get { return instance; }
    }

    void Start()
    {

    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

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