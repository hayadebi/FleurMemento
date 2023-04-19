using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent_leaf : MonoBehaviour
{
    public child_leaf[] _child;
    public Transform ef_pos;
    public AudioSource audioS;
    public AudioClip[] se;
    public Animator parent_anim;
    public Renderer[] rens;
    private float absolute_time = 0;
    // Start is called before the first frame update
    void Start()
    {
        RenOff();
    }
    void RenOff()
    {
        for(int i = 0; i < rens.Length;)
        {
            rens[i].enabled = false;
            i++;
        }
    }
    void RenOn()
    {
        for (int i = 0; i < rens.Length;)
        {
            rens[i].enabled = true;
            i++;
        }
    }
    void time_leaf()
    {
        GManager.instance.Triggers[4] = 4;
        Instantiate(GManager.instance.effectobj[5], ef_pos.position, ef_pos.rotation);
        if (_child[0].enter_obj != null && _child[0].enter_obj.GetComponent<Rigidbody>() && _child[0].enter_obj.GetComponent<Rigidbody>().isKinematic)
        {
            _child[0].enter_obj.GetComponent<Rigidbody>().isKinematic = false;
            _child[0].enter_obj.transform.parent = null;
        }
        Invoke(nameof(reset_leaf), 1f);
    }

    void reset_leaf()
    {
        absolute_time = 0f;
        parent_anim.SetFloat("movespeed", 1);
            GManager.instance.Triggers[4] = 0;
            RenOff();
    }

    public void leaf_reverse()
    {
        audioS.PlayOneShot(se[1]);
        parent_anim.SetInteger("Anumber", 0);
            GManager.instance.Triggers[4] = 3;
        Invoke(nameof(time_leaf), 0.35f);
    }
    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.playerselect == 1 && Input.GetMouseButtonDown(1) && GManager.instance.walktrg && !GManager.instance.over && GManager.instance.setmenu <= 0)
        {
            if (GManager.instance.Triggers[4] == 0)
            {
                absolute_time = 1.3f;
                RenOn();
                audioS.PlayOneShot(se[0]);
                Instantiate(GManager.instance.effectobj[5], ef_pos.position, ef_pos.rotation);
                parent_anim.SetInteger("Anumber", 1);
                GManager.instance.Triggers[4] = 1;
            }
            else if (GManager.instance.Triggers[4] == 1 || GManager.instance.Triggers[4] == 2)
            {
                parent_anim.SetFloat("movespeed", 0.9f);
                leaf_reverse();
            }
        }
        else if (GManager.instance.playerselect == 1 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || (absolute_time <= 0f && GManager.instance.Triggers[4]==1)) && (GManager.instance.Triggers[4] == 1 || GManager.instance.Triggers[4] == 2) && GManager.instance.walktrg && !GManager.instance.over && GManager.instance.setmenu <= 0 )
        {
            if (_child[0].enter_obj != null && _child[0].enter_obj.GetComponent<Rigidbody>() && _child[0].enter_obj.GetComponent<Rigidbody>().isKinematic)
            {
                _child[0].enter_obj.GetComponent<Rigidbody>().isKinematic = false;
                _child[0].enter_obj.transform.parent = null;
            }
            parent_anim.SetFloat("movespeed", 0.9f);
            leaf_reverse();
        }
        if (absolute_time > 0f)
            absolute_time -= Time.deltaTime;
    }
}
