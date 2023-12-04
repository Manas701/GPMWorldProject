using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum phases { phase1, phase2, phase3}

public class CyclopsPhases : MonoBehaviour
{

    public phases currentPhase;
    public int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentPhase = phases.phase1;
    }

    // Update is called once per frame
    void Update()
    {
        updatePhase();
        actOnPhases(currentPhase);
    }

    void updatePhase()
    {
       if(currentHealth < 75 && currentHealth >50)
        {
            currentPhase = phases.phase2;
        }
       else if( currentHealth < 50 && currentHealth > 0)
        {
            currentPhase = phases.phase3;
        }
    }

    void actOnPhases(phases phase)
    {
        if (phase == phases.phase1)
        {
            //insert code for behaviour
        }
        else if(phase == phases.phase2)
        {
            //insert code for behaviour
        }
        else if (phase == phases.phase3)
        {
            //insert code for behaviour
        }
    }
}
