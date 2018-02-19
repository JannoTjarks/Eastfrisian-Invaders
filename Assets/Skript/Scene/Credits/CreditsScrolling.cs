using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScrolling : MonoBehaviour {

    // Physics
    private Rigidbody2D _rb2d;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_rb2d.position.y <= 38)
            _rb2d.position += new Vector2(0, 0.01F);
    }
}
