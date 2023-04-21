using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColRotTarget : MonoBehaviour
{
    public int set_index = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "ground" && col.GetComponent<objAngle>())
        {
            objAngle tmpangle = col.GetComponent<objAngle>();
            if (set_index != -1)
                tmpangle.indextarget = set_index;
            else
                Destroy(col.gameObject);
        }
    }
}
