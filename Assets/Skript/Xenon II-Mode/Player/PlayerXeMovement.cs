using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerXeMovement : MonoBehaviour
{
    // Physics
    private Rigidbody2D _rb2d;

    // Movement
    private float _moveX;
    private float _moveY;
    private const int MOVESPEEDX = 6;
    private const int MOVESPEEDY = 4;

    // Shoot
    public GameObject LaserPrefab;
    private const float LASERSPEED = 500F;
    private bool _isAttacking = false;
    private AudioSource _shootAudio;
    public AudioClip ShootSound;

    // Life
    public GameObject LifeBarPrefab;
    private GameObject _lifeBar;
    public Sprite LifeBar2;
    public Sprite LifeBar1;
    private int _life = 3;
    private System.Diagnostics.Stopwatch _invincible;
    private bool _invincibleFrame = false;

    // Position Camera
    private GameObject _camera;
    private Rigidbody2D _rb2dCamera;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _shootAudio = GetComponent<AudioSource>();
        _lifeBar = (GameObject)Instantiate(LifeBarPrefab, new Vector2(gameObject.transform.position.x - 7,
                gameObject.transform.position.y - 1.7F), Quaternion.identity);
        _invincible = new System.Diagnostics.Stopwatch();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _rb2dCamera = _camera.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Control
        _moveX = Input.GetAxis("Horizontal");
        _moveY = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1") && _isAttacking == false)
        {
            _isAttacking = true;
        }
    }

    void FixedUpdate()
    {
        // Movement
        _rb2d.velocity = new Vector2(_moveX * MOVESPEEDX, _moveY * MOVESPEEDY);

        // Out of Bounds Prevent
        if (_rb2d.position.y - _rb2dCamera.position.y == 4.5)
        {
            _rb2d.position += new Vector2(0, 0.005F);
            if (_moveY == 1)
                _moveY = 0;
        }
        else if (_rb2d.position.y - _rb2dCamera.position.y == -4.5)
        {
            _rb2d.position += new Vector2(0, 0.011F);
            if (_moveY == -1)
                _moveY = 0;
        }

        if (_rb2d.position.y - _rb2dCamera.position.y > 4.5)
        {
            _rb2d.position = new Vector2(_rb2d.position.x, _rb2dCamera.position.y + 4.5F);
        }
        else if (_rb2d.position.y - _rb2dCamera.position.y < -4.5)
        {
            _rb2d.position = new Vector2(_rb2d.position.x, _rb2dCamera.position.y - 4.5F);
        }

        if (_rb2d.position.x == 8)
        {
            _rb2d.position += new Vector2(-0.011F, 0);
            if (_moveX == 1)
                _moveX = 0;
        }
        else if (_rb2d.position.x == -8)
        {
            _rb2d.position += new Vector2(0.011F, 0);
            if (_moveX == -1)
                _moveX = 0;
        }

        if (_rb2d.position.x > 8)
        {
            _rb2d.position = new Vector2(8,_rb2d.position.y);
        }
        else if (_rb2d.position.x < -8)
        {
            _rb2d.position = new Vector2(-8, _rb2d.position.y);
        }

        // Invincible
        if (_invincible.ElapsedMilliseconds >= 200)
        {
            _invincibleFrame = false;
            _invincible.Stop();
            _invincible.Reset();
        }

        // Shoot
        if (_isAttacking)
        {
            GameObject laser = (GameObject)Instantiate(LaserPrefab, new Vector2(gameObject.transform.position.x,
                gameObject.transform.position.y + 0.7F),
                Quaternion.identity);
            _shootAudio.PlayOneShot(ShootSound, 0.125F);
            laser.GetComponent<Rigidbody2D>().AddForce(Vector2.up * LASERSPEED);
            _isAttacking = false;
        }

        // Lifebar
        if (_life == 2)
            _lifeBar.GetComponent<SpriteRenderer>().sprite = LifeBar2;
        if (_life == 1)
            _lifeBar.GetComponent<SpriteRenderer>().sprite = LifeBar1;
    }

    //  Calls the destroying of the Player-Object or changes the life
    void OnCollisionEnter2D(Collision2D col)
    {
        if (_invincibleFrame == false)
        {
            if (col.gameObject.tag == "ShootEnemy")
                _life -= 1;
            else if (col.gameObject.tag == "MissileEnemy")
                _life -= 1;
            else if (col.gameObject.tag == "Enemy")
                _life -= 1;

            if (_life == 0)
                Destroy();

            _invincibleFrame = true;
            _invincible.Start();

        }
    }

    // Destroys the Player-Object
    void Destroy()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}