using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject cyclopsHealthManager;
    private float flashTimer = 0f;
    public float flashTime = 0.25f;
    public SpriteRenderer bossSprite;

    // Start is called before the first frame update
    void Awake()
    {
        cyclopsHealthManager = GameObject.Find("Cyclops Health Manager");
    }

    // Update is called once per frame
    void Update()
    {
        flashTime += Time.deltaTime;
        if (flashTime >= flashTimer && bossSprite.color != Color.white) {
            bossSprite.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        // Debug.Log("Entered collision with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Bullet") {
            if (collision.gameObject.GetComponent<Bullet>().shooter == "Player")
            {
                Destroy(collision.gameObject);
                DamageFlash();
                cyclopsHealthManager.GetComponent<CyclopsHealth>().TakeDamage(1);
            }
        }
    }

    void DamageFlash() {
        bossSprite.color = Color.red;
        flashTimer = 0;
    }
}
