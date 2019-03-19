using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject selectedObject;

    // Pause
    private bool _paused = false;
    
    private void Start()
    {

    }
    
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit"))
        {   
            if (!_paused)
            {
                selectedObject.SetActive(true);                
                Time.timeScale = 0;
                _paused = true;
            }
            else if (_paused)
            {
                selectedObject.SetActive(false);
                Time.timeScale = 1;
                _paused = false;
            }
        }
    }
}