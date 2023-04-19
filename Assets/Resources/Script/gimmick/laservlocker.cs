using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class laservlocker : MonoBehaviour
{
    public string hand_tag = "hand";
    public string wall_tag = "wall";
    public string player_tag = "player";

    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;   // Rayを飛ばす方向
    private float distance = 0;    // Rayを飛ばす距離
    public float distance_speed = 8f;
    private player temp_p;
    private bool start_ray = false;
    public GameObject particle;
    private bool particle_check = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.walktrg)
        {
            distance += Time.deltaTime * distance_speed;
            Vector3 temp = transform.forward * distance_speed;
            direction = temp.normalized;
            ray = new Ray(transform.position, direction);  // Rayを飛ばす
            if (!start_ray)
                start_ray = true;
            if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
            {
                
                if (hit.collider.CompareTag(hand_tag))
                {
                    if (particle_check)
                    {
                        particle_check = false;
                        particle.SetActive(false);
                        GManager.instance.setrg = 8;
                    }
                    distance = 0;
                }
                else if (hit.collider.CompareTag(player_tag) && hit.collider.gameObject.GetComponent<player>())
                {
                    if (particle_check)
                    {
                        particle_check = false;
                        particle.SetActive(false);
                        GManager.instance.setrg = 8;
                    }
                    distance = 0;
                    temp_p = hit.collider.gameObject.GetComponent<player>();
                    temp_p.reset_trg = true;
                    temp_p.reset_load();
                    Instantiate(GManager.instance.effectobj[4], transform.position, transform.rotation);
                    GManager.instance.walktrg = false;
                    Invoke(nameof(PlayerReset), 1f);

                }
                else if (!particle_check)
                {
                    particle_check = true;
                    particle.SetActive(true);
                }

                if (distance >= 4.5f)
                    distance = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (start_ray)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 1f);
            Gizmos.DrawRay(ray.origin, ray.direction * distance);  // Rayをシーン上に描画
        }
    }
    void PlayerReset()
    {
        temp_p.load_scenechange();
    }
}
//CompareTag
