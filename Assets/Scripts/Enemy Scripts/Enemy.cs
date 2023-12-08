using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public SpriteRenderer enemySprite;
    public float maxHealth;
    private float health;
    public float flashTime;
    private float flashTimer = 0;
    // public float moveSpeed = 5f;
    // public float moveRange = 10f;

    // private bool movingRight = true;
    // private float initialPosition;

    void Awake()
    {
        //initialPosition = transform.position.x;
        health = maxHealth;
    }

    // void Update()
    // {
    //     MoveEnemy();
    // }

    void FixedUpdate() {
        UpdateTimers();
    }

    void UpdateTimers() {
        flashTimer += Time.deltaTime;
        if (flashTimer >= flashTime && enemySprite.color == Color.red) {
            enemySprite.color = Color.white;
        }
    }

    void DamageFlash() {
        enemySprite.color = Color.red;
        flashTimer = 0;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Entered collision with " + collision.gameObject.name);
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

    // void MoveEnemy()
    // {
    //     float currentPosition = transform.position.x;

    //     if (movingRight)
    //     {
    //         transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    //     }
    //     else
    //     {
    //         transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    //     }

    //     // Check if the enemy has moved beyond the moveRange in the current direction
    //     if ((movingRight && currentPosition >= initialPosition + moveRange) ||
    //         (!movingRight && currentPosition <= initialPosition - moveRange))
    //     {
    //         // Change direction
    //         FlipDirection();
    //     }
    // }

    // void FlipDirection()
    // {
    //     movingRight = !movingRight;

    //     // Flip the enemy's sprite or object if it has a sprite renderer
    //     if (enemySprite != null)
    //     {
    //         Vector3 scale = transform.localScale;
    //         scale.x *= -1;
    //         transform.localScale = scale;
    //     }
    // }
}
