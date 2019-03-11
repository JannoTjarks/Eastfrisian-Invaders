using UnityEngine;

public class EndingPlayer : MonoBehaviour
{
    // Physics
    private Rigidbody2D _rb2d;

    // Movement
    private const int MOVESPEEDX = 6;
    private const int MOVESPEEDY = 5;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Movement
        if (_rb2d.position.y <= 32)
            _rb2d.position += new Vector2(0, 0.01F);
        else
            _rb2d.velocity = new Vector2(0 * MOVESPEEDX, 1 * MOVESPEEDY);

        if (_rb2d.position.y >= 45)
            Destroy(gameObject);
    }
}