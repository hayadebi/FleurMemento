using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColEvent : MonoBehaviour
{
    public bool ColTrigger = false;
    public bool onAction = true;
    public string tagName = "Player";
    public bool managerTrg = false;
    public int managerIndex = 0;
    public bool stoptrg = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == tagName && onAction)
        {
            ColTrigger = true;
            if(managerTrg )
            {
                GManager.instance.colTrg[managerIndex] = true;
            }
        }
        else if (tagName == "" && onAction && col.tag != "player" && col.tag != "OnMask" && col.tag != "enemy" && col.tag != "noactive" && col.tag != "wall" && col.tag != "water" && col.tag != "npc" && col.tag != "event" && col.tag != "stop" && col.tag != "stop_player" && col.tag != "stop_tubomi")
        {
            ColTrigger = true;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = true;
            }
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == tagName && onAction && !ColTrigger)
        {
            ColTrigger = true;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = true;
            }
        }
        else if (tagName == "" && onAction && !ColTrigger && col.tag != "player" && col.tag != "OnMask" && col.tag != "enemy" && col.tag != "wall" && col.tag != "noactive" && col.tag != "water" && col.tag != "npc" && col.tag != "event" && col.tag != "stop" && col.tag != "stop_player" && col.tag != "stop_tubomi")
        {
            ColTrigger = true;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = true;
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Water" && tagName == "")
        {
            GManager.instance.setrg = 10;
        }
        if (col.tag == tagName)
        {
            ColTrigger = false;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = false;
            }
        }
        else if (tagName == "" && onAction && col.tag != "player" && col.tag != "OnMask" && col.tag != "enemy" && col.tag != "wall" && col.tag != "noactive" && col.tag != "water" && col.tag != "npc"  && col.tag != "event" && col.tag != "stop" && col.tag != "stop_player" && col.tag != "stop_tubomi")
        {
            ColTrigger = false;
            if (managerTrg)
            {
                GManager.instance.colTrg[managerIndex] = false;
            }
        }
    }
}
