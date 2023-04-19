using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmrRot : MonoBehaviour
{
    private Vector3 vec;
    public Transform p_pos;
    public float mag_posx = 2.6f;
    public float mag_posy = 1.8f;
    public float over_playerposforward = 999;
    public float over_playerposhight = 999f;
    public bool returntrg = false;
    public bool downtrg = false;
    // Start is called before the first frame update
    void Start()
    {
        vec = this.transform.position;
        vec.x = mag_posx ;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GManager.instance.over && vec.z != p_pos.position.z )
        {
            vec.z = p_pos.position.z;
            this.transform.position = vec;
        }
        if (!GManager.instance.over && over_playerposforward != 999 && (p_pos.position.x <= over_playerposforward || (p_pos.position.x >= (over_playerposforward * -1)&&returntrg )) )
        {
            vec.x = p_pos.position.x+mag_posx +(over_playerposforward * -1);
            this.transform.position = vec;
        }
        else if (!GManager.instance.over && over_playerposforward != 999 && (p_pos.position.x > over_playerposforward || (p_pos.position.x < (over_playerposforward * -1)&&returntrg)) && this.transform.position.x != mag_posx)
        {
            vec.x = mag_posx;
            this.transform.position = vec;
        }
        if (!GManager.instance.over && over_playerposhight != 999 && (p_pos.position.y >= over_playerposhight||(downtrg && p_pos.position.y <= -(over_playerposhight/2))))
        {
            vec.y = p_pos.position.y + mag_posy/2 + over_playerposhight/2;
            this.transform.position = vec;
        }
        else if (!GManager.instance.over && over_playerposhight != 999 && (p_pos.position.y > -(over_playerposhight / 2)&&p_pos.position.y < over_playerposhight)  && this.transform.position.y != mag_posy)
        {
            vec.y = mag_posy;
            this.transform.position = vec;
        }
    }
}
