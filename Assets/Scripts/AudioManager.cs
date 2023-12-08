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
    private AudioClip popUpSfx;
    [SerializeField]
    private AudioClip textSfx;
    [SerializeField] 
    private AudioClip phoneSfx;
    // Start is called before the first frame update
    void Start()
    {
        //playaudio on new text
        GameManager.current.onSetDialogue += PlayPopSound;
        //playaudio one by one word
        GameManager.current.onDialoguePlayText += TextSound;
        GameManager.current.onDialogueStart +=PhoneSound;
        GameManager.current.onDialogueEnd += PhoneSound;
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


}
