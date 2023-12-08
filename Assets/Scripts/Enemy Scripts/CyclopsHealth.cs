using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CyclopsHealth : MonoBehaviour
{
    public CyclopsPhases cp;
    public Image healthBar;
    float healthAmount;


    // Start is called before the first frame update
    void Start()
    {
        healthAmount = cp.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //this is for testing
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(10f);
        }
        
    }

    void TakeDamage(float damageAmount)
    {
        healthAmount -= damageAmount;
        cp.currentHealth = healthAmount;
        healthBar.fillAmount = healthAmount / 100f;
    }
}
