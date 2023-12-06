using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //game manager singleton
    public static GameManager current;
    public void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else if (current != this)
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
            return;
        }

    }



    #region Events List

    public Action onLevelCompleted;
    public void LevelCompleted()
    {
        onLevelCompleted?.Invoke();

    }

    public Action onLoadNextLevel;
    public void LoadNextLevel()
    {
        onLoadNextLevel?.Invoke();
    }


    public Action<string, string> onSetSprite;
    public void SetSprite(string charName, string spriteName)
    {
        onSetSprite?.Invoke(charName, spriteName);
    }

    public Action<string, string> onSetDialogue;
    public void SetDialogue(string charName, string dialogue)
    {
        onSetDialogue?.Invoke(charName, dialogue);

    }

    public Action onDialogueStart;
    public void DialogueStart()
    {
        onDialogueStart?.Invoke();

    }

    public Action onDialogueEnd;
    public void DialogueEnd()
    {
        onDialogueEnd?.Invoke();
    }

    public Action onDialogueContinue;
    public void DialogueContinue()
    {
        onDialogueContinue?.Invoke();
    }

    public Action onDialoguePlayText;
    public void DialoguePlayText()
    {
        onDialoguePlayText?.Invoke();
    }

    public Action onGameOverTrigger;
    public void GameOverTrigger()
    {

        onGameOverTrigger?.Invoke();
    }

    public Action onPauseTrigger;
    public void PauseTrigger()
    {

        onPauseTrigger?.Invoke();
    }

    public Action onGameCompleted;
    public void GameCompleted()
    {

        onGameCompleted?.Invoke();
    }



    #endregion
}
