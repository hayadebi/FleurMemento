using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drum : MonoBehaviour
{
    public bool bombtrg = false;
    public GameObject bombeffect;
    public AudioSource audioSource;
    public AudioClip se;
    public GameObject[] destroyobj;
    public Renderer this_enableren;//0は自分、1はリリー、2はツボミン
    public Renderer[] player_ren;
    public Renderer[] tubomi_ren;
    public float player_mag = 1f;
    private bool endtrg = false;
    public player playerobj;
    public player tubomiobj;
    public Rigidbody on_gravity=null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bombtrg && !endtrg)
        {
            endtrg = true;
            Instantiate(bombeffect, transform.position, transform.rotation);
            audioSource.PlayOneShot(se);
            if(on_gravity != null && !on_gravity.useGravity )
            {
                on_gravity.useGravity = true;
                on_gravity.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
            if((playerobj.gameObject.transform.position-transform.position ).magnitude <player_mag || (tubomiobj.gameObject.transform.position - transform.position).magnitude < player_mag)
            {
                playerobj.reset_trg = true;
                if ((playerobj.gameObject.transform.position - transform.position).magnitude < player_mag)
                {
                    for(int i = 0; i < player_ren.Length;)
                    {
                        player_ren[i].enabled = false;
                        i++;
                    }
                }
                else if ((tubomiobj.gameObject.transform.position - transform.position).magnitude < player_mag)
                {
                    for (int i = 0; i < tubomi_ren.Length;)
                    {
                        tubomi_ren[i].enabled = false;
                        i++;
                    }
                }
                playerobj.reset_load();
                Instantiate(GManager.instance.effectobj[4], transform.position, transform.rotation);
                GManager.instance.walktrg = false;
                Invoke(nameof(PlayerScene), 1f);
            }
            for (int i = 0; i < destroyobj.Length;)
            {
                this_enableren.enabled = false;
                if ((destroyobj[i].transform.position - transform.position).magnitude < player_mag)
                    destroyobj[i].SetActive(false);
                i++;
            }
        }
    }
    void PlayerScene()
    {
        playerobj.load_scenechange();
        Destroy(gameObject, 0.1f);
    }
}
