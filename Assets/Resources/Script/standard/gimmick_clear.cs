using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gimmick_clear : MonoBehaviour
{
    private bool cleartrg = false;
    private bool player = false;
    private bool tubomi = false;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        if(!cleartrg && player && tubomi)
        {
            cleartrg = true;
            GManager.instance.setrg = 13;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (!cleartrg&&!player && col.gameObject.name == nameof(player)) player = true;
        else if (!cleartrg&&!tubomi && col.gameObject.name == nameof(tubomi)) tubomi = true;
    }
}
