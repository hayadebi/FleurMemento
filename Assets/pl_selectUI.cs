using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_selectUI : MonoBehaviour
{
    public GameObject[] active_ui;
    public int old_pl = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(old_pl != GManager.instance.playerselect )
        {
            old_pl = GManager.instance.playerselect;
            for (int i = 0;i<active_ui.Length;)
            {
                if(i==GManager.instance.playerselect )
                {
                    active_ui[i].SetActive(true);
                }
                else if (i != GManager.instance.playerselect)
                {
                    active_ui[i].SetActive(false);
                }
                i++;
            }
        }
    }
}
