using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowerdMove : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.walktrg && !GManager.instance.over)
        {
            rb.velocity = transform.forward * speed;

        }
        else if (rb.velocity != Vector3.zero)
            rb.velocity = Vector3.zero;
    }
}
