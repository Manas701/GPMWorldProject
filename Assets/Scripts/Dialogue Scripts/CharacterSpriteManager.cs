using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSpriteManager : MonoBehaviour
{
    public CharacterSprite[] characterSprites;

    private void Start()
    {

    }

    public Sprite getSprite(string charName, string spriteName)
    {
        foreach(CharacterSprite c in characterSprites)
        {
            if(c.name.ToLower() == charName.ToLower() && c.sprite.name.ToLower() == spriteName.ToLower())
            {

                return c.sprite;
            }
        }
        return null;
    }

}
