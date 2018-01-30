using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Pause
    private bool _paused = false;
    public GameObject PauseScreenPrefab;
    private GameObject _pauseScreen;

    // Position Camera
    private GameObject _camera;
    private Rigidbody2D _rb2dCamera;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _rb2dCamera = _camera.GetComponent<Rigidbody2D>();
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit"))
        {   
            if (!_paused)
            {
                _pauseScreen = (GameObject)Instantiate(PauseScreenPrefab, new Vector2(_rb2dCamera.transform.position.x,
                _rb2dCamera.transform.position.y), Quaternion.identity);
                Time.timeScale = 0;
                _paused = true;
            }
            else if (_paused)
            {
                Destroy(_pauseScreen);
                Time.timeScale = 1;
                _paused = false;
            }
        }
    }
}