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
    [Tooltip("How long does the bullet stay for")]
    public float bulletLifetime = 3.0f;
    private float lifetimeTimer = 0.0f;
    public Vector2 bulletDirection;
    public string shooter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = bulletDirection * bulletSpeed;
        lifetimeTimer += Time.deltaTime;
        if (lifetimeTimer >= bulletLifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
