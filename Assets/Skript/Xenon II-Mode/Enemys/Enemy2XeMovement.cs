using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2XeMovement : MonoBehaviour
{
    // Physics
    private Rigidbody2D _rb2d;

    // Movement
    private float _moveX;
    private float _moveY;
    private const float MOVESPEED = 6F;
    private bool _movementChanged = false;
    private System.Diagnostics.Stopwatch _movement;

    // Shoot
    public GameObject LaserPrefab;
    private const float LASERSPEED = 500F;
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
    }

    void FixedUpdate()
    {
        // Movement
        if (_rb2d.position.y > -5.5 && _rb2d.position.x < 7.5
            && _rb2d.position.x > -7.5)
            _rb2d.isKinematic = true;
        else
            _rb2d.isKinematic = false;

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

        if (_shoot.ElapsedMilliseconds >= 1000)
        {
            _Attacked = true;
            _shoot.Stop();
            _shoot.Reset();
            _shoot.Start();
        }

        if (_isAttacking)
        {
            GameObject laser1 = (GameObject)Instantiate(LaserPrefab, new Vector2(gameObject.transform.position.x + 0.3F,
                gameObject.transform.position.y - 0.5F), Quaternion.identity);
            GameObject laser2 = (GameObject)Instantiate(LaserPrefab, new Vector2(gameObject.transform.position.x - 0.3F,
                gameObject.transform.position.y - 0.5F), Quaternion.identity);
            _shootAudio.PlayOneShot(ShootSound, 0.05F);
            laser1.GetComponent<Rigidbody2D>().AddForce(Vector2.down * LASERSPEED);
            laser2.GetComponent<Rigidbody2D>().AddForce(Vector2.down * LASERSPEED);
            _isAttacking = false;
        }
    }

    // Destroys the Enemy-Object and decreases the Enemy-Count or changed scene to game over
    void OnCollisionEnter2D(Collision2D col)
    {
        //Collision
        _rb2d.isKinematic = false;
        if (col.gameObject.tag == "Shoot")
        {
            WinCondition.EnemysAlive--;
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "End")
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}