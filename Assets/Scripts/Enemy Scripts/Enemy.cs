using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public SpriteRenderer enemySprite;
    public Rigidbody2D rb;
    public float maxHealth;
    private float health;
    // public float force = 10.0f;
    public float flashTime;
    private float flashTimer = 0;
    // public float moveTime;
    // private float moveTimer = 0;
    public float moveSpeed = 5f;
    public float moveRange = 10f;
    public bool startFacingLeft = true;

    private bool movingRight = true;
    private float initialPosition;

    void Awake()
    {
        initialPosition = transform.position.x;
        health = maxHealth;
        if (startFacingLeft) {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        // Move();
    }
    void Update() {
        UpdateTimers();
    }

    void FixedUpdate() {
        MoveEnemy();
    }

    void UpdateTimers() {
        flashTimer += Time.deltaTime;
        if (flashTimer >= flashTime && enemySprite.color == Color.red) {
            enemySprite.color = Color.white;
        }
        // moveTimer += Time.deltaTime;
        // if (moveTimer >= moveTime) {
        //     transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //     Move();
        // }
    }

    // void Move() {
    //     Debug.Log("Move called");
    //     moveTimer = 0;
    //     Debug.Log(Vector3.forward);
    //     rb.AddRelativeForce(Vector3.forward * force);
    // }

    void DamageFlash() {
        enemySprite.color = Color.red;
        flashTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        // Debug.Log("Entered collision with " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<Bullet>() != null) {
            if (collision.gameObject.GetComponent<Bullet>().shooter == "Player")
            {
                Destroy(collision.gameObject);
                DamageFlash();
                health--;
                if (health <= 0) {
                    // Kill this enemy
                    Destroy(gameObject);
                }
            }
        }
    }

    void MoveEnemy()
    {
        float currentPosition = transform.position.x;

        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        // Check if the enemy has moved beyond the moveRange in the current direction
        if ((movingRight && currentPosition >= initialPosition + moveRange) ||
            (!movingRight && currentPosition <= initialPosition - moveRange))
        {
            // Change direction
            FlipDirection();
        }
    }

    void FlipDirection()
    {
        movingRight = !movingRight;

        // Flip the enemy's sprite or object if it has a sprite renderer
        if (enemySprite != null)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
