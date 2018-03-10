using UnityEngine;

public class LaserSiMovement : MonoBehaviour
{
    // Physics
    private Rigidbody2D _rb2d;

    // Position Camera
    private GameObject _camera;
    private Rigidbody2D _rb2dCamera;

    // Use this for initialization
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _rb2dCamera = _camera.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Out of Bounds Prevent
        if (_rb2d.position.y - _rb2dCamera.position.y >= 4.5 ||
            _rb2d.position.y - _rb2dCamera.position.y <= -4.5)
        {
            Destroy(gameObject);
        }
    }      

    // Destroys the Lasers-Objects
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider)
        {
            Destroy(gameObject);
        }
    }
}