using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChage : MonoBehaviour
{
    public VLight vl;
    public player tubomi_pl;
    public float sunup_slowspeed = 15;
    public float maxlight_power = 1;
    public GameObject effect_obj;
    public bool use_trg = true;
    public AudioSource audioS;
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
        if(GManager.instance.playerselect == 1 && GManager.instance.walktrg && !GManager.instance.over && GManager.instance.setmenu <= 0)
        {
            tubomi_pl.anim.SetBool("sun", true);
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (GManager.instance.playerselect == 1 && GManager.instance.walktrg && !GManager.instance.over && GManager.instance.setmenu <= 0)
        {
            if (!tubomi_pl.anim.GetBool("sun"))
            {
                tubomi_pl.anim.SetBool("sun", true);
            }
            else if(GManager.instance.sun_power < maxlight_power && vl.lightMultiplier > 0)
            {
                GManager.instance.sun_power += (Time.deltaTime / sunup_slowspeed);
                vl.lightMultiplier -= (Time.deltaTime / sunup_slowspeed);
            }
            else if (use_trg && vl.lightMultiplier <= 0)
            {
                use_trg = false;
                effect_obj.SetActive(false);
                tubomi_pl.anim.SetBool("sun", false);
                audioS.enabled = false;
                Destroy(vl.gameObject, 0.2f);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (GManager.instance.playerselect == 1 && GManager.instance.walktrg && !GManager.instance.over && GManager.instance.setmenu <= 0)
        {
            tubomi_pl.anim.SetBool("sun", false);
        }
    }
}
