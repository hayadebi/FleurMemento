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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ButtonAnimSet(int setn = 1)
    {
        buttonanim.SetInteger("Anumber", setn);
        if (wallrocks != null && wallrocks.Length > 0)
        {
            for (int i = 0; i < wallrocks.Length;)
            {
                wallrocks[i].SetInteger("Anumber", setn);
                i++;
            }
        }
        if(drums!=null &&drums.Length > 0)
        {
            for (int i = 0; i < drums.Length;)
            {
                drums[i].bombtrg = true;
                i++;
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "player" || col.tag == "hand"||(groundtrg && col.tag=="ground") )
        {
            staytrg = true;
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(se);
            if (uptrg && (connect_button == null || (connect_button != null && connect_button.staytrg )))
            {
                uptrg = false;
                ButtonAnimSet(1);
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "player" || col.tag == "hand"|| (groundtrg && col.tag == "ground"))
        {
            staytrg = false;
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(se);
            uptrg = true;
            ButtonAnimSet(0);
        }
    }
}
