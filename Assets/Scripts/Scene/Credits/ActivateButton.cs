using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public GameObject selectedObject;

    private GameObject _camera;
    private Rigidbody2D _rb2dCamera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _rb2dCamera = _camera.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb2dCamera.position.y >= 38)
        {
            selectedObject.SetActive(true);
        }
    }
}
