using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Speedrun : MonoBehaviour
{
    private Stopwatch stopWatch = new Stopwatch();

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
        if (textUI != null)
        {
            if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().buildIndex < 9)
            {
                if (!stopWatch.IsRunning)
                {
                    stopWatch.Start();
                }

            }
            else
            {
                if (stopWatch.IsRunning)
                {
                    stopWatch.Stop();
                    stopWatch.Reset();
                }                    
            }
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}",
                ts.Minutes, ts.Seconds);            
            var text = textUI.GetComponent<Text>();
            text.text = "TIME: " + elapsedTime;            
        }        
        else 
        {
            textUI = GameObject.FindGameObjectWithTag("Speedrun");            
        }
    }
}