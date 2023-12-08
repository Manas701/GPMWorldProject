using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ComicPages
{
    public Sprite comicImage;
    public string comicText;
}

public class ComicHandler : MonoBehaviour
{
    public GameObject continueTextButton;
    public float scrollTextSpeed = 0.015f;
    public Image comicImagePlace;
    public TextMeshProUGUI comicTextPlace;

    [SerializeField] private bool isLoadingComic = false;
    int comicIndex = 0;
    int currentSceneIndex;

    public ComicPages[] comicPages;

    // Start is called before the first frame update
    void Start()
    {
        comicIndex = 0;
        //comicImagePlace.enabled = false;
        //comicTextPlace.enabled = false;
        loadComic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            continueTextButton.SetActive(false);
            if (comicIndex < comicPages.Length)
            {
                comicImagePlace.enabled = true;
                comicTextPlace.enabled = true;
                if (isLoadingComic == false)
                    loadComic();

            }
            else
            {
                comicImagePlace.enabled = false;
                comicTextPlace.enabled = false;
                Debug.Log("Finished Comics");
            }
        }
    }

    void loadComic()
    {
        isLoadingComic = true;

        comicImagePlace.sprite = comicPages[comicIndex].comicImage;
        // comicTextPlace.text = comicPages[comicIndex].comicText;
        StopCoroutine(processTextScolling(comicPages[comicIndex].comicText));
        StartCoroutine(processTextScolling(comicPages[comicIndex].comicText));

        comicIndex++;
    }

    IEnumerator processTextScolling(string dialogue)
    {
        comicTextPlace.text = "";

        for (int i = 0; i < dialogue.Length; i++)
        {
            if (comicTextPlace.text.Length < dialogue.Length)
            {
                yield return new WaitForSeconds(scrollTextSpeed);

                comicTextPlace.text += dialogue[i];
            }
        }

        isLoadingComic = false;
    }

}
