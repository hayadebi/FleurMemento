﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jplocal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Application.systemLanguage == SystemLanguage.Japanese)
        {
            GManager.instance.isEnglish = 0;
        }
        if(!GManager.instance.resettrg)
        {
            GManager.instance.resettrg = true;
            PlayerPrefs.DeleteAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
