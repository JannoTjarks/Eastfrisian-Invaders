using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebarMovement : MonoBehaviour
{
    // Physics
    private Rigidbody2D _rb2d;

    // Lifebar Position
    private float distance;

    // Position Camera
    private GameObject _camera;
    private Rigidbody2D _rb2dCamera;

    // Use this for initialization
    void Start ()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _rb2dCamera = _camera.GetComponent<Rigidbody2D>();
        distance = _rb2d.position.y -_rb2dCamera.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        _rb2d.position = new Vector2(_rb2d.position.x, _rb2dCamera.position.y + distance);
    }
}
