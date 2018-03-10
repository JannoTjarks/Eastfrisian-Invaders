using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy1SiMovement : MonoBehaviour
{
    // Physics
    private Rigidbody2D _rb2d;

    // Movement
    private float _moveX;
    private const float MOVESPEED = 6F;
    private bool _left;

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

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _shootAudio = GetComponent<AudioSource>();
        _shoot = new System.Diagnostics.Stopwatch();
        _shoot.Start();
    }

    void FixedUpdate()
    {
        // Movement
        if (_left == true)
            _moveX = -0.15F;
        else if (_left == false)
            _moveX = 0.15F;

        _rb2d.velocity = new Vector2(_moveX * MOVESPEED, 0);
        if (_rb2d.position.x > 8 || _rb2d.position.x < -8)
        {
            if (_left == true)
            {
                _left = false;
                transform.position = new Vector2(-7.9F, gameObject.transform.position.y - 1.3F);
            }
            else if (_left == false)
            {
                _left = true;
                transform.position = new Vector2(7.9F, gameObject.transform.position.y - 1.3F);
            }
        }

        if (_rb2d.position.y <= -4.7)
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

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
            GameObject laser = (GameObject)Instantiate(LaserPrefab, new Vector2(gameObject.transform.position.x,
                gameObject.transform.position.y - 0.5F), Quaternion.identity);
            _shootAudio.PlayOneShot(ShootSound, 0.05F);
            laser.GetComponent<Rigidbody2D>().AddForce(Vector2.down * LASERSPEED);
            _isAttacking = false;
        }
    }

    // Destroys the Enemy-Object and decreases the Enemy-Count or changed scene to game over
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Shoot")
        { 
            WinCondition.EnemysAlive--;
            Destroy(gameObject);
        }
    }
}