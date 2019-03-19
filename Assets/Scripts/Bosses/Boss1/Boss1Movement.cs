using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1Movement : MonoBehaviour
{
    // Movement
    private Rigidbody2D _rb2d;
    private float _moveX;
    private const float MOVESPEED = 6F;
    private bool _movementChanged = false;
    private System.Diagnostics.Stopwatch _movement;

    // Hatches
    private System.Diagnostics.Stopwatch _opening;
    private bool _opened;
    private SpriteRenderer _sprite;
    public Sprite Boss1_Normal;
    public Sprite Boss1_Opening1;
    public Sprite Boss1_Opening2;
    public Sprite Boss1_Opened;

    // Enemy-Spawning
    public GameObject Enemy;
    float _enemySpawning;

    // Shoot
    public GameObject Missile;
    private const float MISSILESPEED = 500F;
    private bool _isAttacking = false;
    private bool _Attacked = true;
    private System.Diagnostics.Stopwatch _shoot;
    private AudioSource _shootAudio;
    public AudioClip ShootSound;

    // Life
    Sprite[] sprites;
    private int _life = 60;
    private const int LIFEMAX = 60;
    private LifebarBoss _lifebar;

    // Scene
    public int SceneIndex;

    void Start()
    {
        // Movement
        _rb2d = GetComponent<Rigidbody2D>();
        _shootAudio = GetComponent<AudioSource>();
        _movementChanged = false;
        _movement = new System.Diagnostics.Stopwatch();
        _movement.Start();
        _shoot = new System.Diagnostics.Stopwatch();
        _shoot.Start();
        sprites = Resources.LoadAll<Sprite>("Bosses/SpritemapBoss1LifeBar");

        // Sprite
        _sprite = GetComponent<SpriteRenderer>();

        // Hatches
        _opening = new System.Diagnostics.Stopwatch();
        _opening.Start();

        // Lifebar
        _lifebar = LifebarBoss.Instance;
        _lifebar.SetLIFEMAX = LIFEMAX;
    }

    private void FixedUpdate()
    {
        // Movement
        if (_movementChanged == false)
        {
            float rngMovement;
            rngMovement = Random.Range(1, 4);
            if (rngMovement == 1)
            {
                _moveX = 0.15F;
                _rb2d.velocity = new Vector2(_moveX * MOVESPEED, 0);
            }
            else if (rngMovement == 2)
                _rb2d.velocity = new Vector2(0, 0);
            else if (rngMovement == 3)
            {
                _moveX = -0.15F;
                _rb2d.velocity = new Vector2(_moveX * MOVESPEED, 0);
            }

            _movementChanged = true;
        }

        if (_movement.ElapsedMilliseconds >= 3000)
        {
            _movement.Stop();
            _movement.Reset();
            _movement.Start();
            _movementChanged = false;
        }

        if (_rb2d.position.x >= 5)
        {
            _moveX = -0.15F;
            _rb2d.velocity = new Vector2(_moveX * MOVESPEED, 0);
            MovementOutofBounds();
        }

        if (_rb2d.position.x <= -5)
        {
            _moveX = 0.15F;
            _rb2d.velocity = new Vector2(_moveX * MOVESPEED, 0);
            MovementOutofBounds();
        }

        // Enemy-Spawning
        _enemySpawning = Random.Range(1, 250);
        if (_enemySpawning == 1 && _opened == false)
            _opening.Start();

        // Hatches
        if (_opening.ElapsedMilliseconds >= 800 && _opened == false)
            _sprite.sprite = Boss1_Opening1;
        if (_opening.ElapsedMilliseconds >= 1600 && _opened == false)
            _sprite.sprite = Boss1_Opening2;
        if (_opening.ElapsedMilliseconds >= 2400 && _opened == false)
        {
            _sprite.sprite = Boss1_Opened;
            SpawnEnemy();
            _opened = true;
        }

        if (_opening.ElapsedMilliseconds >= 3500 && _opened == true)
            _sprite.sprite = Boss1_Opening2;
        if (_opening.ElapsedMilliseconds >= 4300 && _opened == true)
            _sprite.sprite = Boss1_Opening1;
        if (_opening.ElapsedMilliseconds >= 5100 && _opened == true)
        {
            _sprite.sprite = Boss1_Normal;
            _opened = false;
            _opening.Stop();
            _opening.Reset();
        }

        // Shoot
        if (UnityEngine.Random.Range(1, 100) == 1)
        {
            if (_Attacked == true)
            {
                _Attacked = false;
                _isAttacking = true;
            }
        }

        if (_shoot.ElapsedMilliseconds >= 3000)
        {
            _Attacked = true;
            _shoot.Stop();
            _shoot.Reset();
            _shoot.Start();
        }

        if (_isAttacking)
        {
            GameObject[] missiles = GameObject.FindGameObjectsWithTag("MissileEnemy");

            if (missiles.Length <= 2)
            {
                float rngShoot;
                rngShoot = Random.Range(1, 101);
                if (rngShoot <= 33)
                {
                    Instantiate(Missile, new Vector2(gameObject.transform.position.x - 1.95F, gameObject.transform.position.y - 2.5F),
                        Quaternion.identity);
                }
                else if (rngShoot <= 66 && rngShoot > 33)
                {
                    Instantiate(Missile, new Vector2(gameObject.transform.position.x - 3.075F, gameObject.transform.position.y - 2.5F),
                        Quaternion.identity);
                }
                else if (rngShoot > 66)
                {
                    Instantiate(Missile, new Vector2(gameObject.transform.position.x - 1.95F, gameObject.transform.position.y - 2.5F),
                        Quaternion.identity);
                    Instantiate(Missile, new Vector2(gameObject.transform.position.x - 3.075F, gameObject.transform.position.y - 2.5F),
                        Quaternion.identity);
                }
            }

            _shootAudio.PlayOneShot(ShootSound, 0.3F);
            _isAttacking = false;
        }
    }

    void MovementOutofBounds()
    {
        _movement.Stop();
        _movement.Reset();
        _movement.Start();
    }

    void SpawnEnemy()
    {
        Instantiate(Enemy, new Vector2(gameObject.transform.position.x + 2.4F,
            gameObject.transform.position.y - 2.5F), Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Shoot")
        {
            _life -= 1;
            if (_life == 0)
                Destroy();
            else
                _lifebar.Life = _life;           
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneIndex + 1, LoadSceneMode.Single);
    }
}