using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hight_audio : MonoBehaviour
{
    public Transform player_pos;
    public Transform tubomi_pos;
    public AudioSource audioSource;
    public bool hightdownenable = false;
    public int check_hight = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((player_pos.position.y < check_hight|| tubomi_pos.position.y < check_hight) && audioSource.enabled != hightdownenable)
        {
            audioSource.enabled = hightdownenable;
        }
        else if ((player_pos.position.y >= check_hight&& tubomi_pos.position.y >= check_hight) && audioSource.enabled != !hightdownenable)
        {
            audioSource.enabled = !hightdownenable;
        }
    }
}
