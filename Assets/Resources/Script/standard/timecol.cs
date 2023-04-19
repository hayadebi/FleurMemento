using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timecol : MonoBehaviour
{
    public Collider target;
    public float timelimit = 1f;
    public bool change_trg = true;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(TargetFalse), timelimit);
    }
    void TargetFalse()
    {
        target.enabled = change_trg;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
