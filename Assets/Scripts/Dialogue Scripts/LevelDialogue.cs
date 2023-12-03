using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDialogue : MonoBehaviour
{

    public TextAsset testInkFile;
    public TextAsset endInkFile;
    private StoryProcessor sp;

    private bool triggerDialogueBool = false;


    // Start is called before the first frame update
    void Start()
    {
        sp = FindObjectOfType<StoryProcessor>();
        GameManager.current.onDialogueEnd += endLevel;
    }

    private void Update()
    {
        //can change KeyCode to fit whatever your needs, don't set it as Space though since it will trigger and skip the dialogue
        if (Input.GetKeyDown(KeyCode.T) && triggerDialogueBool == false)
        {
            Debug.Log("Space space");
            triggerDialogueBool = true;

            //set up Beginning Dialogue
            sp.inkFile = testInkFile;
            GameManager.current.DialogueStart();
        }
    }

    void endLevel()
    {
        GameManager.current.LoadNextLevel();
    }
}
