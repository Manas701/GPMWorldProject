using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsSound : MonoBehaviour
{
    AudioSource audioSource;
    public AudioSource idleSource;
    public AudioClip[] soundClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayDiagnolSlam()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.clip = soundClip[0];
        audioSource.Play();
    }

    public void Verticallaserside()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.clip = soundClip[1];
        audioSource.Play();
    }
    public void Playheadshootlaser()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.clip = soundClip[2];
        audioSource.Play();
    }
    public void PlayHorizontalLaser()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.clip = soundClip[3];
        audioSource.Play();
    }
    public void PlayLefthandslam()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.clip = soundClip[4];
        audioSource.Play();
    }
    public void PlayRighthandslam()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.clip = soundClip[5];
        audioSource.Play();
    }
    public void Idle()
    {
       
        idleSource.clip = soundClip[Random.Range(6,soundClip.Length)];
        idleSource.Play();
    }
}
