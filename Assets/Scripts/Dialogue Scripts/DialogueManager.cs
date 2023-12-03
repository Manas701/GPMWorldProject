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
    public Image spritePlace1;
    public Image spritePlace2;
    public Sprite defaultSprite;
    private Color grayedOut = new Color(0.5f, 0.5f, 0.5f);
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
        textDialogue.text = dialogue;
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
