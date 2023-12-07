using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody2D rb;
    public Collider2D coll;

    [Tooltip("How fast the bullet travels")]
    public float bulletSpeed = 10.0f;
    [Tooltip("How much damage the bullet does")]
    public int bulletDamage = 1;
    public Vector2 bulletDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = bulletDirection * bulletSpeed;
    }
}
