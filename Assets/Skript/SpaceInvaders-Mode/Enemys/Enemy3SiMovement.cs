using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy3SiMovement : MonoBehaviour {

    // Physics
    private Rigidbody2D _rb2d;

    // Movement
    private float _moveX;
    private float _moveY;
    private const float MOVESPEED = 4F;
    private bool _movementChanged = false;
    private System.Diagnostics.Stopwatch _movement;

    // Life
    private int _life = 3;
    private System.Diagnostics.Stopwatch _invincible;
    private bool _invincibleFrame = false;

    // Shoot
    public GameObject Missile;
    private const float MISSILESPEED = 500F;
    private bool _isAttacking = false;
    private bool _Attacked = true;
    private System.Diagnostics.Stopwatch _shoot;
    private AudioSource _shootAudio;
    public AudioClip ShootSound;

    // Level-Exit
    public EnemyCount WinCondition;

    // Use this for initialization
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _shootAudio = GetComponent<AudioSource>();
        _movement = new System.Diagnostics.Stopwatch();
        _movement.Start();
        _shoot = new System.Diagnostics.Stopwatch();
        _shoot.Start();
        _invincible = new System.Diagnostics.Stopwatch();
    }

    void FixedUpdate()
    {
        // Movement
        if (_movementChanged == false)
        {
            float rngMovement = UnityEngine.Random.Range(1, 4); ;
            if (_rb2d.position.x == 7.5)
                rngMovement = UnityEngine.Random.Range(1, 3);
            else if (_rb2d.position.x == -7.5)
                rngMovement = UnityEngine.Random.Range(2, 4);

            if (rngMovement == 1)
            {
                _moveX = 0.15F;
                _rb2d.velocity = new Vector2(_moveX * MOVESPEED, 0);
            }
            else if (rngMovement == 2)
            {
                _moveY = -0.15F;
                _rb2d.velocity = new Vector2(0, _moveY * MOVESPEED);
            }
            else if (rngMovement == 3)
            {
                _moveX = -0.15F;
                _rb2d.velocity = new Vector2(_moveX * MOVESPEED, 0);
            }

            _movementChanged = true;
        }

        if (_rb2d.position.x < -7.5 || _rb2d.position.x > 7.5)
        {
            _rb2d.velocity = new Vector2(0, 0);
        }

        if (_rb2d.position.y <= -4.5)
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

        if (_movement.ElapsedMilliseconds >= 3000)
        {
            _movementChanged = false;
            _movement.Stop();
            _movement.Reset();
            _movement.Start();
        }

        // Shoot
        if (UnityEngine.Random.Range(1, 50) == 1)
        {
            if (_Attacked == true)
            {
                _Attacked = false;
                _isAttacking = true;
            }
        }

        if (_shoot.ElapsedMilliseconds >= 1500)
        {
            _Attacked = true;
            _shoot.Stop();
            _shoot.Reset();
            _shoot.Start();
        }

        if (_isAttacking)
        {
            Instantiate(Missile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.5F),
                        Quaternion.identity);
            _shootAudio.PlayOneShot(ShootSound, 0.3F);
            _isAttacking = false;
        }

        // Invincible
        if (_invincible.ElapsedMilliseconds >= 200)
        {
            _invincibleFrame = false;
            _invincible.Stop();
            _invincible.Reset();
        }


    }

    // Destroys the Enemy-Object and decreases the Enemy-Count or changed scene to game over
    void OnCollisionEnter2D(Collision2D col)
    {
        if (_invincibleFrame == false)
        {
            if (col.gameObject.tag == "Shoot")
                _life -= 1;

            if (_life == 0)
            {
                WinCondition.EnemysAlive--;
                Destroy(gameObject);
            }

            _invincibleFrame = true;
            _invincible.Start();
        }
    }
}