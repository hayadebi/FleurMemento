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
        if (bombtrg && !endtrg)
        {
            endtrg = true;
            Instantiate(bombeffect, transform.position, transform.rotation);
            audioSource.PlayOneShot(se);
            if (on_gravity != null && !on_gravity.useGravity)
            {
                on_gravity.useGravity = true;
                on_gravity.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
            if ((playerobj.gameObject.transform.position - transform.position).magnitude < player_mag)
            {
                playerobj.reset_trg = true;
                if (playerobj.player_id != GManager.instance.playerselect)
                    playerobj.auto_changetrg = true;
                playerobj.audioSource.Stop();
                playerobj.anim.SetInteger(playerobj.numbername, 444);
                GManager.instance.setrg = 9;
                playerobj.reset_load();
                Instantiate(GManager.instance.effectobj[4], transform.position, transform.rotation);
                GManager.instance.walktrg = false;
                Invoke(nameof(PlayerScene), 1f);
            }
            else if ((tubomiobj.gameObject.transform.position - transform.position).magnitude < player_mag)
            {
                tubomiobj.reset_trg = true;
                if (tubomiobj.player_id != GManager.instance.playerselect)
                    tubomiobj.auto_changetrg = true;
                tubomiobj.audioSource.Stop();
                tubomiobj.anim.SetInteger(tubomiobj.numbername, 444);
                GManager.instance.setrg = 6;
                tubomiobj.reset_load();
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
