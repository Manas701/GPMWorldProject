using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum phases { phase1, phase2, phase3 }

public class CyclopsPhases : MonoBehaviour
{

    public phases currentPhase;
    public float currentHealth;
    public Animator bossAnimator;
    public float actionCooldownTime;
    private bool canAction;
    private string[] phaseOneTriggerNames = { "PlayRightHandSlam", "PlayLeftHandSlam", "PlayDiagnolSlam", "PlayDiagnolSlam", "PlayDiagnolSlam" };
    private string[] phaseTwoTriggerNames = { "PlayRightHandSlam", "PlayLeftHandSlam", "PlayDiagnolSlam", "PlayHeadShootLaser" };
    private string[] phaseThreeTriggerNames = { "PlayHeadShootLaser", "PlayHorizontalLaser", "PlayVerticalLaser" };


    // Start is called before the first frame update
    void Start()
    {
        currentPhase = phases.phase1;
        canAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAction == true)
        {
            StartCoroutine(initiateActionCooldown());
            updatePhase();
            actOnPhases(currentPhase);
        }
    }

    void updatePhase()
    {
        if (currentHealth < 75 && currentHealth > 50)
        {
            currentPhase = phases.phase2;
        }
        else if (currentHealth < 50 && currentHealth > 0)
        {
            currentPhase = phases.phase3;
        }
    }

    void actOnPhases(phases phase)
    {
        int randomIndex;
        string randomTrigger;

        if (phase == phases.phase1)
        {
            randomIndex = Random.Range(0, phaseOneTriggerNames.Length);
            randomTrigger = phaseOneTriggerNames[randomIndex];
            bossAnimator.SetTrigger(randomTrigger);
        }
        else if (phase == phases.phase2)
        {
            randomIndex = Random.Range(0, phaseTwoTriggerNames.Length);
            randomTrigger = phaseTwoTriggerNames[randomIndex];
            bossAnimator.SetTrigger(randomTrigger);
        }
        else if (phase == phases.phase3)
        {
            randomIndex = Random.Range(0, phaseThreeTriggerNames.Length);
            randomTrigger = phaseThreeTriggerNames[randomIndex];
            bossAnimator.SetTrigger(randomTrigger);
        }
    }



    IEnumerator initiateActionCooldown()
    {
        canAction = false;
        yield return new WaitForSeconds(actionCooldownTime);
        canAction = true;
    }
}


