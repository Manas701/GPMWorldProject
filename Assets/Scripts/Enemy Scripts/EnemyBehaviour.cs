using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public SpriteRenderer enemySprite;
    public float moveSpeed = 5f;
    public float moveRange = 10f;

    private bool movingRight = true;
    private float initialPosition;

    void Start()
    {
        initialPosition = transform.position.x;
    }

    void Update()
    {
        MoveEnemy();
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
