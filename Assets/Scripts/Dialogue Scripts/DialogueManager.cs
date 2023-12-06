using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;
    [Range(0.01f, 0.5f)]
    public float textScrollSpeed = 0.025f;
    public Image spritePlace1;
    public Image spritePlace2;
    public Sprite defaultSprite;
    private Color grayedOut = new Color(0.5f, 0.5f, 0.5f);
    private bool isSentenceFinished = false, skipSentence = false;
    // private Color whiteOut = new Color(0f, 0f, 0f, 0f);

    public CharacterSpriteManager _CharacterSpriteManager;

    // Start is called before the first frame update
    void Start()
    {
        _CharacterSpriteManager = FindObjectOfType<CharacterSpriteManager>();
        dialogueCanvas.SetActive(false);
        spritePlace1.sprite = defaultSprite;
        spritePlace2.sprite = defaultSprite;
        spritePlace2.gameObject.SetActive(false);

        GameManager.current.onDialogueStart += setupCanvas;
        GameManager.current.onDialogueEnd += disableCanvas;
        GameManager.current.onSetSprite += displaySprites;
        GameManager.current.onSetDialogue += processDialogue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isSentenceFinished == false && skipSentence == false)
            {
                skipSentence = true;
            }

            if (isSentenceFinished == true)
            {
                isSentenceFinished = false;
                GameManager.current.DialogueContinue();
            }
        }
    }


    public void setupCanvas()
    {

        dialogueCanvas.SetActive(true);
        //  GameManager.current.onTriggerInteractPrompt(false);
    }

    public void disableCanvas()
    {

        spritePlace1.sprite = defaultSprite;
        spritePlace2.sprite = defaultSprite;
        dialogueCanvas.SetActive(false);
        //GameManager.current.onTriggerInteractPrompt(true);
    }

    public void processDialogue(string charName, string dialogue)
    {
        textName.text = charName;
        // textDialogue.text = dialogue;
        StartCoroutine(processTextScolling(dialogue));
    }

    IEnumerator processTextScolling(string dialogue)
    {

        textDialogue.text = "";
        isSentenceFinished = false;

        for (int i = 0; i < dialogue.Length; i++)
        {
            if (textDialogue.text.Length < dialogue.Length)
            {
                yield return new WaitForSeconds(textScrollSpeed);
                GameManager.current.DialoguePlayText();
                textDialogue.text += dialogue[i];
            }

            if (skipSentence == true)
            {
                skipSentence = false;
                textDialogue.text = dialogue;
                isSentenceFinished = true;
            }
        }

        isSentenceFinished = true;
    }

    public void displaySprites(string charName, string spriteName)
    {
        //spritePlace.sprite = cm.getSprite(charName, spriteName);
        if (charName == "Doom Slayer") //display sprite in position 1
        {
            if (spritePlace1.gameObject.activeInHierarchy == false)
            {
                spritePlace1.gameObject.SetActive(true);
            }
            spritePlace1.sprite = _CharacterSpriteManager.getSprite(charName, spriteName);
            spritePlace1.color = Color.white;
            spritePlace2.color = grayedOut;
        }
        else if (charName.Contains("-")) //display blank sprite
        {
            spritePlace1.color = grayedOut;
            spritePlace2.color = grayedOut;
            spritePlace1.gameObject.SetActive(false);
            spritePlace2.gameObject.SetActive(false);
        }
        else //display sprite in position 2
        {
            if (spritePlace2.gameObject.activeInHierarchy == false)
            {
                spritePlace2.gameObject.SetActive(true);
            }
            spritePlace2.sprite = _CharacterSpriteManager.getSprite(charName, spriteName);
            spritePlace2.color = Color.white;
            spritePlace1.color = grayedOut;
        }

        if ((spriteName.Contains("blank")))
        {
            spritePlace2.sprite = defaultSprite;
            //spritePlace1.color = grayedOut;
            spritePlace2.color = grayedOut;
        }
    }

    public void resetCanvas()
    {
        spritePlace1.sprite = null;
        spritePlace2.sprite = null;
    }
}
