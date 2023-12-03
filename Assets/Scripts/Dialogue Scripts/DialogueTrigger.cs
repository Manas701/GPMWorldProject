using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset DialogueText;
    private StoryProcessor sp;

    private void Start()
    {
        sp = GameObject.FindObjectOfType<StoryProcessor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sp.inkFile = DialogueText;
            GameManager.current.DialogueStart();
            Destroy(this.gameObject);
        }
    }
}
