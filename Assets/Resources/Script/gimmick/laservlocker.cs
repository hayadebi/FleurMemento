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
    public float max_raydistance = 4.5f;
    public Color temp_color = new Color(1f, 0f, 0f, 1f);
    public bool laserofftrg = false;
    public AudioSource audios1 = null;
    public AudioSource audios2 = null;
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
                if (particle_check && laserofftrg)
                {
                    GManager.instance.setrg = 8;
                    particle_check = false;
                    particle.SetActive(false);
                }
                else if(laserofftrg)
                {
                    distance = 0;
                }
                else if (hit.collider.CompareTag(hand_tag))
                {
                    if (particle_check)
                    {
                        particle_check = false;
                        particle.SetActive(false);
                        GManager.instance.setrg = 8;
                        if (audios1 != null && audios1.isPlaying) audios1.Stop();
                        if (audios2 != null && audios2.isPlaying) audios2.Stop();
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
                        if (audios1 != null && audios1.isPlaying) audios1.Stop();
                        if (audios2 != null && audios2.isPlaying) audios2.Stop();
                    }
                    distance = 0;
                    temp_p = hit.collider.gameObject.GetComponent<player>();
                    temp_p.reset_trg = true;
                    //if (temp_p.player_id != GManager.instance.playerselect)
                    //    temp_p.auto_changetrg = true;
                    temp_p.audioSource.Stop();
                    temp_p.anim.SetInteger(temp_p.numbername, 444);
                    if(temp_p.player_id == 0 )
                        GManager.instance.setrg = 9;
                    else if (temp_p.player_id == 1)
                        GManager.instance.setrg = 6;
                    temp_p.reset_load();
                    Instantiate(GManager.instance.effectobj[4], transform.position, transform.rotation);
                    GManager.instance.walktrg = false;
                    Invoke(nameof(PlayerReset), 1f);

                }
                else if (!particle_check && !laserofftrg)
                {
                    particle_check = true;
                    particle.SetActive(true);
                    if (audios1 != null && !audios1.isPlaying) audios1.Play();
                    if (audios2 != null && !audios2.isPlaying) audios2.Play();
                }

                if (distance >= max_raydistance)
                    distance = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (start_ray)
        {
            Gizmos.color = temp_color;
            Gizmos.DrawRay(ray.origin, ray.direction * distance);  // Rayをシーン上に描画
        }
    }
    void PlayerReset()
    {
        temp_p.load_scenechange();
    }
}
//CompareTag
