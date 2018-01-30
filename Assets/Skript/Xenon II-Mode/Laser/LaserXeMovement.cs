using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserXeMovement : MonoBehaviour
{
    // Destroys the Lasers-Objects
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider)
        {
            Destroy(gameObject);
        }
    }
}
