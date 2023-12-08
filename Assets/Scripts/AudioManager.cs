using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource popUpAudioSource;
    [SerializeField]
    private AudioSource textAudioSource;
    [SerializeField]
    private AudioSource phoneAudioSource;
    [SerializeField]
    private AudioSource playerMoveAudioSource;
    [SerializeField]
    private AudioSource playerJumpAudioSource;
    [SerializeField]
    private AudioSource playerShootAudioSource;
    [SerializeField]
    private AudioSource playerHurtAudioSource;
    [SerializeField]
    private AudioClip popUpSfx;
    [SerializeField]
    private AudioClip textSfx;
    [SerializeField] 
    private AudioClip phoneSfx;
    [SerializeField]
    private AudioClip[] playerMoveSfx;
    [SerializeField]
    private AudioClip playerJumpSfx;
    [SerializeField]
    private AudioClip[] playerShootSfx;
    [SerializeField]
    private AudioClip[] playerHurtSfx;
    // Start is called before the first frame update
    void Start()
    {
        //playaudio on new text
        GameManager.current.onSetDialogue += PlayPopSound;
        //playaudio one by one word
        GameManager.current.onDialoguePlayText += TextSound;
        GameManager.current.onDialogueStart +=PhoneSound;
        GameManager.current.onDialogueEnd += PhoneSound;
        GameManager.current.onPlayerMove += MoveSound;
        GameManager.current.onPlayerJump += JumpSound;
        GameManager.current.onPlayerShoot += ShootSound;
        GameManager.current.onPlayerHurt += HurtSound;
        GameManager.current.onPlayerStopMove += StopMoveSound;
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void PlayPopSound(string sound ,string soind)

    {
        popUpAudioSource.clip = popUpSfx;
        popUpAudioSource.Play();
    }
    void TextSound()

    {
        if (!textAudioSource.isPlaying)
        {
            textAudioSource.clip = textSfx;
            textAudioSource.Play();
        }
       
    }
    void PhoneSound()
    {
        phoneAudioSource.clip=phoneSfx;
        phoneAudioSource.Play();

    }
    void MoveSound()
    {
        if (playerMoveAudioSource.isPlaying)
        {
            return;
        }
        playerMoveAudioSource.clip = playerMoveSfx[Random.Range(0, playerMoveSfx.Length)];
        playerMoveAudioSource.Play();

    }
    void JumpSound()
    {

        playerJumpAudioSource.clip = playerJumpSfx;
        playerJumpAudioSource.Play();
    }
    void ShootSound()
    {
        playerShootAudioSource.clip = playerShootSfx[Random.Range(0, playerShootSfx.Length)];
        playerShootAudioSource.Play();

    }
    void HurtSound()
    {
        playerHurtAudioSource.clip = playerHurtSfx[Random.Range(0, playerHurtSfx.Length)];
        playerHurtAudioSource.Play();

    }

    void StopMoveSound()
    {
        playerMoveAudioSource.Stop();



    }
}
