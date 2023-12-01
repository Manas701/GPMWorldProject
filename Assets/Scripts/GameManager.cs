using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //create Game Manager Singleton
    public static GameManager current;
    public void Awake()
    {
        if (current == null)
        {
            current = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    #region Events List

    //for reference on how to make events/listeners

    //public Action<JunkObject> onJunkSpawned;
    //public void JunkSpawned(JunkObject JO)
    //{
    //    if (onJunkSpawned != null)
    //        onJunkSpawned(JO);
    //}

    //public Action onJunkPicked;
    //public void JunkPicked()
    //{
    //    if (onJunkPicked != null)
    //        onJunkPicked();
    //}

    #endregion
}
