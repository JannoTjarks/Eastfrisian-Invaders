using UnityEngine;

public class Boss1RocketMovement : MonoBehaviour
{
    // Physics
    private Rigidbody2D _rb2d;

    // Movement
    private const float MISSILESPEED = 8F;

    // Position Player
    private GameObject _player;
    private Rigidbody2D _rb2dPlayer;
    private Vector2 _oldPositionPlayer;
    private bool _lastMovement = false;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb2dPlayer = _player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(_rb2d.position, _rb2dPlayer.position) >= 3 && _lastMovement == false)
            _rb2d.position = Vector2.MoveTowards(_rb2d.position, _rb2dPlayer.position,
                0.1F);
        else if (Vector3.Distance(_rb2d.position, _rb2dPlayer.position) < 3 && _lastMovement == false)
        {
            _lastMovement = true;
            _oldPositionPlayer = _rb2dPlayer.position;
        }
        else if (_lastMovement == true)
            _rb2d.position = Vector2.MoveTowards(_rb2d.position, _oldPositionPlayer, 0.1F);

        if (_rb2d.position.y <= -4.5)
            Destroy(gameObject);
    }

    // Destroys the Lasers-Objects
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.tag != "MissileEnemy")
            Destroy(gameObject);
    }
}