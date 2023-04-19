using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child_leaf : MonoBehaviour
{
    public BoxCollider col_this;
    public parent_leaf top_parent;
    public GameObject enter_obj = null;
    public SpriteRenderer _sprite;
    public Transform set_leafobj;
    public bool off_script = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.Triggers[4] == 0 && !off_script && _sprite.enabled)
        {
            col_this.isTrigger = true;
            _sprite.enabled = false;
            top_parent.parent_anim.SetFloat("movespeed", 0.9f);
        }
        else if (GManager.instance.Triggers[4] != 0 && !off_script && !_sprite.enabled)
        {
            col_this.isTrigger = false;
            _sprite.enabled = true;
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag != "player" && (col.gameObject.tag == "wall" || col.gameObject.tag == "obj" || col.gameObject.tag == "hand")) && !off_script && GManager.instance.Triggers[4] == 1)
        {
            GManager.instance.Triggers[4] = 2;
            if (col.gameObject.tag == "hand")
            {
                enter_obj = col.gameObject;
                enter_obj.transform.parent = set_leafobj;
                if (enter_obj.GetComponent<Rigidbody>())
                    enter_obj.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                top_parent.parent_anim.SetFloat("movespeed", 0);
            }
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if ((col.gameObject.tag != "player" && (col.gameObject.tag == "wall" || col.gameObject.tag == "obj" || col.gameObject.tag == "hand")) && !off_script && GManager.instance.Triggers[4] == 1)
        {
            GManager.instance.Triggers[4] = 2;
            if (col.gameObject.tag == "hand")
            {
                enter_obj = col.gameObject;
                enter_obj.transform.parent = set_leafobj;
                if (enter_obj.GetComponent<Rigidbody>())
                    enter_obj.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                top_parent.parent_anim.SetFloat("movespeed", 0);
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag != "player" && (col.gameObject.tag == "wall" || col.gameObject.tag == "obj" || col.gameObject.tag == "hand")) && !off_script && GManager.instance.Triggers[4] == 1)
        {
            GManager.instance.Triggers[4] = 2;
            if (col.gameObject.tag == "hand")
            {
                enter_obj = col.gameObject;
                enter_obj.transform.parent = set_leafobj;
                if (enter_obj.GetComponent<Rigidbody>())
                    enter_obj.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                top_parent.parent_anim.SetFloat("movespeed", 0);
            }
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if ((col.gameObject.tag != "player" && (col.gameObject.tag == "wall" || col.gameObject.tag == "obj" || col.gameObject.tag == "hand")) && !off_script && GManager.instance.Triggers[4] == 1)
        {
            GManager.instance.Triggers[4] = 2;
            
            if (col.gameObject.tag == "hand")
            {
                enter_obj = col.gameObject;
                enter_obj.transform.parent = set_leafobj;
                if (enter_obj.GetComponent<Rigidbody>())
                    enter_obj.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                top_parent.parent_anim.SetFloat("movespeed", 0);
            }
        }
    }
}
