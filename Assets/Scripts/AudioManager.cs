using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource popUpAudioSource;
    [SerializeField]
    private AudioClip popUpSfx;

    // Start is called before the first frame update
    void Start()
    {
        //playaudio on new text
        GameManager.current.onSetDialogue += PlayPopSound;
        //playaudio one by one word
        //GameManager.current.onDialoguePlayText += PlayPopSound;
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
    
    
    }

}
