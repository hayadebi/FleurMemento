using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
	// ジャンプする力（上向きの力）を定義
	public float jumpForce = 40.0f;
	[Header("0は開花エフェクト、1はジャンプ時のエフェクト")]
	public GameObject[] effect_obj;
	[Header("0=開花時SE、1=ジャンプ時SE、2=閉花時SE")]
	public int[] setrg_id;
	public Animator anim;
	public float no_time = 0;
	private GameObject pl;
	public bool flower = false;
	private void Update()
	{
		if (!anim.GetBool("flower") && GManager.instance.sun_power >= 1 && GManager.instance.playerselect == 1 && GManager.instance.walktrg && !GManager.instance.over && GManager.instance.setmenu <= 0)
		{
			flower = true;
			anim.SetBool("flower", true);
			GManager.instance.setrg = setrg_id[0];
			Instantiate(effect_obj[0], transform.position, transform.rotation);
		}
		else if (flower && !anim.GetBool("flower") && GManager.instance.sun_power > 0 && GManager.instance.walktrg && !GManager.instance.over && GManager.instance.setmenu <= 0)
		{
			anim.SetBool("flower", true);
		}
		if (no_time > 0)
		{
			pl.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Acceleration);
			no_time -= Time.deltaTime;
		}
	}
	private void OnTriggerEnter(Collider col)
	{
		// 当たった相手のタグがPlayerだった場合
		if (GManager.instance.handtrg != 5 && no_time <= 0 && anim.GetBool("flower") &&GManager.instance.sun_power > 0 && GManager.instance.playerselect == 0 && GManager.instance.walktrg && !GManager.instance.over && GManager.instance.setmenu <= 0 && col.CompareTag("player") && col.gameObject.name == "player")
		{
			pl = col.gameObject;
			GManager.instance.setrg = setrg_id[1];
			Instantiate(effect_obj[1], transform.position, transform.rotation);
			// 当たった相手のRigidbodyコンポーネントを取得して、上向きの力を加える
			col.GetComponent<Rigidbody>().AddForce(0, jumpForce*2, 0 ,ForceMode.Acceleration);
			no_time = 0.2f;
			GManager.instance.sun_power -= 0.4f;
			if(GManager.instance.sun_power <= 0)
			{
				flower = false;
				GManager.instance.sun_power = 0;
				GManager.instance.setrg = setrg_id[2];
				Instantiate(effect_obj[0], transform.position, transform.rotation);
				anim.SetBool("flower", false);
			}
		}
	}
}
