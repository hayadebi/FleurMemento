using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handevent : MonoBehaviour
{
    public bool hand = true;
    public GameObject player;
    public Animator p_anim;
    public Animator this_anim=null;
    public int stop_animnum;
    public GameObject handobj;
    public Collider bcol;
    public Rigidbody rb;
    public int hand_id = 0;
    public bool event_area = false;
    public float no_time = 0;
    private GameObject leaf;
    private parent_leaf pleaf;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        p_anim = player.GetComponent<Animator>();
        handobj = GameObject.Find("handobj");
        Invoke(nameof(GetLeaf), 0.1f);
    }
    void GetLeaf()
    {
        leaf = GameObject.Find("leaf");
        pleaf = leaf.GetComponent<parent_leaf>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hand && no_time <= 0 && !event_area && GManager.instance.walktrg && GManager.instance.playerselect == 0 && player && handobj && bcol && p_anim && rb  && Input.GetMouseButtonDown(0) && GManager.instance.handtrg == hand_id)
        {
            GManager.instance.setrg = 2;
            GManager.instance.hand_cooltime = 2f;
            bcol.isTrigger = false;
            rb.useGravity = true;
            if (hand_id != 5)
            {
                ;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
            }
            this.gameObject.transform.parent = null;
            rb.isKinematic = false;
            p_anim.SetInteger("Anumber", 0);
            p_anim.SetBool("hand", false);
            GManager.instance.handtrg = -1;
            no_time = 0;
        }
        if (no_time > 0)
        {
            no_time -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "player" && hand_id != 5)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition| RigidbodyConstraints.FreezeRotation;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (hand && no_time <= 0 && GManager.instance.hand_cooltime <= 0 && !event_area && GManager.instance.walktrg && GManager.instance.playerselect == 0 && player && handobj && bcol && p_anim && rb && Input.GetMouseButtonDown(0) && col.gameObject.tag == "player" && GManager.instance.handtrg == -1 && col.gameObject.name == "player")
        {
            no_time = 2;
            if (this_anim != null)
                this_anim.SetInteger("Anumber", stop_animnum);
            GManager.instance.setrg = 2;
            GManager.instance.handtrg = hand_id;
            if (pleaf != null && pleaf._child[0].enter_obj == this.gameObject)
                pleaf._child[0].enter_obj = null;
            rb.isKinematic = true;
            if (hand_id != 5)
            {
                rb.useGravity = false;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            bcol.isTrigger = true;
            p_anim.SetBool("hand", true);
            this.gameObject.transform.position = handobj.transform.position;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y + 90, transform.eulerAngles.z);
            this.gameObject.transform.parent = handobj.transform;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "player" && hand_id != 5)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
