using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallbutton : MonoBehaviour
{
    public Animator[] wallrocks;
    public Animator buttonanim;
    private bool uptrg = true;
    public AudioSource audioSource;
    public AudioClip se;
    public drum[] drums;
    public bool staytrg = false;
    public wallbutton connect_button=null;
    public bool groundtrg = false;
    public GameObject onoff_laserobj=null;
    public laservlocker offlaser = null;
    private bool old_lasertrg = true;
    public int set_setrg = 11;
    private float coltime = 0f;
    private bool nocol = false;
    private GameObject tmp_leafobj;
    // Start is called before the first frame update
    void Start()
    {
        tmp_leafobj = GameObject.Find("tmp_leafobj");
    }

    // Update is called once per frame
    void Update()
    {
        if (nocol && (tmp_leafobj.transform.position -this.transform.position).magnitude >= 0.24f)
        {
            staytrg = false;
            nocol = false;
            coltime = 0;
            if (onoff_laserobj != null && offlaser != null)
            {
                old_lasertrg = !old_lasertrg;
                onoff_laserobj.SetActive(old_lasertrg);
                offlaser.laserofftrg = false;
                GManager.instance.setrg = set_setrg;
            }
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(se);
            uptrg = true;
            ButtonAnimSet(0);
        }
    }
    void ButtonAnimSet(int setn = 1)
    {
        buttonanim.SetInteger("Anumber", setn);
        if (connect_button == null || (connect_button != null && connect_button.staytrg && staytrg))
        {
            if (wallrocks != null && wallrocks.Length > 0)
            {
                for (int i = 0; i < wallrocks.Length;)
                {
                    wallrocks[i].SetInteger("Anumber", setn);
                    i++;
                }
            }
            if (drums != null && drums.Length > 0)
            {
                for (int i = 0; i < drums.Length;)
                {
                    drums[i].bombtrg = true;
                    i++;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "player" || col.tag == "hand"||(groundtrg && col.tag=="ground") )
        {
            staytrg = true;
            nocol = false;
            coltime = 0;
            if (onoff_laserobj != null && offlaser !=null)
            {
                old_lasertrg = !old_lasertrg;
                offlaser.laserofftrg = true;
                onoff_laserobj.SetActive(old_lasertrg);
                GManager.instance.setrg = set_setrg;
            }
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(se);
            if (uptrg)
            {
                uptrg = false;
                ButtonAnimSet(1);
            }
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "player" || col.tag == "hand" || (groundtrg && col.tag == "ground"))
        {
            nocol = false;
            coltime += Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if ((col.tag == "player" || col.tag == "hand" || (groundtrg && col.tag == "ground")) && coltime >= 1f)
        {
            staytrg = false;
            nocol = false;
            coltime = 0;
            if (onoff_laserobj != null && offlaser != null)
            {
                old_lasertrg = !old_lasertrg;
                onoff_laserobj.SetActive(old_lasertrg);
                offlaser.laserofftrg = false;
                GManager.instance.setrg = set_setrg;
            }
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(se);
            uptrg = true;
            ButtonAnimSet(0);
        }
        else if ((col.tag == "player" || col.tag == "hand" || (groundtrg && col.tag == "ground")) && coltime < 1f)
        {
            nocol = true;
        }
    }
}
