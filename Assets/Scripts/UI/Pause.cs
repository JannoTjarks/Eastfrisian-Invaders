using UnityEngine;

public class Pause : MonoBehaviour
{
    GameObject speedrunTimer;

    private bool _paused = false;
    
    private void Start()
    {
        speedrunTimer = GameObject.FindGameObjectWithTag("SpeedrunTimer");
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit"))
        {
            if (!_paused)
            {
                Time.timeScale = 0;
                if (speedrunTimer.activeSelf)
                {
                    Speedrun.StopTimer();
                    Debug.Log("Stop");
                }
                _paused = true;                
            }
            else if (_paused)
            {
                Time.timeScale = 1;
                if (speedrunTimer.activeSelf)
                {
                    Speedrun.RestartTimer();
                    Debug.Log("Restart");
                }
                _paused = false;                
            }
        }
    }
}