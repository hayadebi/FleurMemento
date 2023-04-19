using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stagetp : MonoBehaviour
{
    public player[] pobj;
    public float set_nowalkhight = -3.49f;
    public Vector3[] tppos;
    public GameObject[] fadein_out;
    private bool tptrg = false;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "player" && !tptrg)
        {
            tptrg = true;
            pobj[0].nowalk_hight = set_nowalkhight;
            pobj[1].nowalk_hight = set_nowalkhight;
            Instantiate(fadein_out[0], transform.position, transform.rotation);
            Invoke(nameof(FadeoutTP), 0.48f);
        }
    }
    void FadeoutTP()
    {
        Instantiate(fadein_out[1], transform.position, transform.rotation);
        pobj[0].gameObject.transform.position = tppos[0];
        pobj[1].gameObject.transform.position = tppos[1];
    }
}
