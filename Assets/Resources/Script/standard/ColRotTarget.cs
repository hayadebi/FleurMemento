using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColRotTarget : MonoBehaviour
{
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
            if (tmpangle.targetobj.Length - 1 > tmpangle.indextarget)
                tmpangle.indextarget += 1;
            else
                Destroy(col.gameObject);
        }
    }
}
