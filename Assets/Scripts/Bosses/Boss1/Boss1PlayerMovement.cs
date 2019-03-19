using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1PlayerMovement : MonoBehaviour
{
    // Physics
    private Rigidbody2D _rb2d;

    // Movement
    private float _moveX;
    private const int MOVESPEED = 6;

    // Shoot
    public GameObject LaserPrefab;
    private const float LASERSPEED = 500F;
    private bool _isAttacking = false;
    private AudioSource _shootAudio;
    public AudioClip ShootSound;

    // Life

    private int _life = 3;
    private Lifebar _lifebar;
    private System.Diagnostics.Stopwatch _invincible;
    private bool _invincibleFrame = false;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _shootAudio = GetComponent<AudioSource>();
        _invincible = new System.Diagnostics.Stopwatch();
        _lifebar = Lifebar.Instance;
    }

    void Update()
    {
        //Control
        _moveX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Fire1") && _isAttacking == false)
        {
            _isAttacking = true;
        }
    }

    void FixedUpdate()
    {
        // Movement
        _rb2d.velocity = new Vector2(_moveX * MOVESPEED, 0);

        // Out of Bounds Prevent
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
            _rb2d.position = new Vector2(8, _rb2d.position.y);
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

            _lifebar.Life = _life;

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
