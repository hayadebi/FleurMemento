using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonLog : MonoBehaviour
{
    public GameObject summon_targetobj;
    public float max_cooltime = 10f;
    private float check_summontime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.walktrg && !GManager.instance.over)
        {
            check_summontime += Time.deltaTime;
            if(check_summontime >= max_cooltime)
            {
                check_summontime = 0;
                GameObject temp_obj = Instantiate(summon_targetobj, transform.position, transform.rotation);
                temp_obj.SetActive(true);
            }
        }
    }
}
