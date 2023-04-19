using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour
{
    //【今後修正しながら使っていくプレイヤースクリプトの原型】

    [Header("停止気にするな")] public bool stoptrg = false;//優先度高めにプレイヤーを停止させるトリガー
    public bool cm_stop = false;
    public float overhight = 9999;//ゲームオーバーになる高さ
    private bool highttrg = false;//高さでゲームオーバーにさせるかどうか
    public float playerspeed = 2;
    [Header("重力適応用")]public  bool jumptrg = false;//ジャンプトリガー
    public float spacetime = 0;//スペースキー押してる時間確認
    public int jumpmode = 0;//ジャンプ時のモード、それに応じて上昇や下降をする
    public float maxjumptime;//最大ジャンプ力
    public float jumptime;
    public float jumpspeed = 16;//ジャンプスピード
    public float max_height = 2;
    public float oldp_y = 0;
    public float gravity = 32;//重力値
    public ColEvent isground;
    public ColEvent ishead;
    public Transform character;//プレイヤーに対応するトランスフォーム
    public Transform body;//プレイヤーのモデルに対応するトランスフォーム
    private bool movetrg = false;//移動時にアニメーションさせるかどうか
    public string numbername;//アニメーターの変数名
    //移動で加算させるxyzそれぞれの値
    public float xSpeed = 0;
    public float ySpeed = 0;
    public float zSpeed = 0;
    //プレイヤーのサウンド関係
    public AudioClip groundse;
    public AudioSource audioSource;

    public Animator anim;//プレイヤーのアニメーションセット
    public Rigidbody rb;//プレイヤーの物理挙動をセット
    //カメラ関係の取得
    public GameObject[] cm_rot;
    public Camera[] cm_main;
    public int select_cm = 0;
    //回転に使う値
    public float X_Rotation = 0;
    public float Y_Rotation = 0;
    private Vector3 mXAxiz;
    private float x_cmR;
    private float y_cmR;
    private Vector3 cmAxiz;

    public int player_id = 0;
    public int stand_anim = 3;
    public int walk_anim = 4;
    public int jump_anim = 5;
    public GameObject secondP;
    private player secondScript;
    public int jump_setrg = 1;
    public ColEvent stopcol;
    private float no_ground = 0;
    public float nowalk_hight = 0.27f;

    // Start is called before the first frame update

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        //セーブしてある位置情報を取得し、プレイヤーのスポーン地点を決定する
        //GManager.instance.posX = PlayerPrefs.GetFloat("posX", 0f);
        //GManager.instance.posY = PlayerPrefs.GetFloat("posY", 0f);
        //GManager.instance.posZ = PlayerPrefs.GetFloat("posZ", 0f);
        //Vector3 ppos = this.transform.position;
        //    ppos.x = GManager.instance.posX;
        //    ppos.y = GManager.instance.posY;
        //ppos.z = GManager.instance.posZ;
        //this.transform.position = ppos;
        mXAxiz = body.transform.localEulerAngles;
        latestPos = character.transform.position;  //前回のPositionの更新
        GManager.instance.freenums[3] = 3;
        if (secondP != null)
            secondScript = secondP.GetComponent<player>();
        GManager.instance.sun_power = 0;
    }

    //視点回転に関する
    public  float maxYAngle = 135f;
    public float minYAngle = 45f;
    public Vector3 latestPos;  //前回のPosition

    public bool reset_trg = false;
    private bool off_tubomi = false;
    public GameObject[] tubomi_model;
    public Transform efpos_90;
    private bool zerotrg = false;
    void FixedUpdate()
    {
        if (overhight != 9999 && transform.position.y < -overhight && !reset_trg)
        {
            reset_trg = true;
            GManager.instance.walktrg = false;
            anim.SetInteger("Anumber", stand_anim);
            rb.velocity = Vector3.zero;
            reset_load();
            Instantiate(GManager.instance.effectobj[4], transform.position, transform.rotation);
            
            Invoke(nameof(load_scenechange), 1f);

        }
        //メニュー画面出現(後で)
        if (GManager.instance.setmenu < 1 && GManager.instance.walktrg && Input.GetKeyDown(KeyCode.Escape) && !stoptrg)
        {
            GameObject m = GameObject.Find("pause_menu(Clone)");
            GManager.instance.ESCtrg = false;
            GManager.instance.walktrg = true;
            jumpmode = 0;
            jumptrg = false;
            ySpeed = 0;
            spacetime = 0;
            if (m == null)
            {
                GManager.instance.setmenu += 1;
                GManager.instance.walktrg = false;
                GManager.instance.setrg = 3;
                GameObject ui = (GameObject)Resources.Load("Prefab/UI/pause_menu");
                Instantiate(ui, transform.position, transform.rotation);
            }
        }
        //マウスカーソル切り替え
        //if (Input.GetMouseButtonDown(2) && Cursor.visible )
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
        //else if (Input.GetMouseButtonDown(2) && !Cursor.visible )
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
        //--------------------------------------

        if (GManager.instance.walktrg && !GManager.instance.over && !stoptrg)
        {
            if (player_id == 1 & secondP.GetComponent<player>().isground.ColTrigger && off_tubomi)
            {
                off_tubomi = false;
                for (int i = 0; i < tubomi_model.Length;)
                {
                    tubomi_model[i].SetActive(true);
                    i++;
                }
            }

            if (player_id == GManager.instance.playerselect && stopcol.onAction)
            {
                stopcol.onAction = false;
            }
            else if (rb.useGravity)
            {
                rb.useGravity = false;
            }
            //リセット
            if (!reset_trg && Input.GetKeyDown(KeyCode.R) && player_id == GManager.instance.playerselect && GManager.instance.walktrg && GManager.instance.setmenu <= 0)
            {
                reset_trg = true;
                reset_load();
                Instantiate(GManager.instance.effectobj[4], transform.position, transform.rotation);
                GManager.instance.walktrg = false;
                Invoke(nameof(load_scenechange), 1.2f);
            }
            //キャラクター変更
            if (isground.ColTrigger && player_id == GManager.instance.playerselect && ((GManager.instance.EventNumber[3] >= 3 && GManager.instance.stageNumber == 0)|| GManager.instance.stageNumber > 0) && Input.GetKeyDown(KeyCode.F) && GManager.instance.freenums[3] <= 0 && GManager.instance.handtrg != 5)
            {
                GManager.instance.freenums[3] = 1;
                if (GManager.instance.playerselect == 0)
                {
                    GManager.instance.playerselect = 1;
                }
                else
                {
                    GManager.instance.playerselect = 0;
                }
                GManager.instance.setrg = 3;
                Instantiate(GManager.instance.effectobj[3], secondP.transform.position, secondP.transform.rotation, secondP.transform);
                Instantiate(GManager.instance.effectobj[3], transform.position, transform.rotation, transform);
            }
            else if (player_id == GManager.instance.playerselect && GManager.instance.freenums[3] > 0)
            {
                GManager.instance.freenums[3] -= Time.deltaTime;
            }
            //視野設定(後で)
            if (cm_main[select_cm].fieldOfView != GManager.instance.siya)
            {
                cm_main[select_cm].fieldOfView = GManager.instance.siya;
            }
            X_Rotation = 0;
            Y_Rotation = 0;
            xSpeed = 0;
            ySpeed = -gravity;
            if (player_id == 1 && GManager.instance.handtrg == 5)
            {
                ySpeed = 0;
            }
            if (!isground.ColTrigger)
            {
                if (anim.GetInteger("Anumber") != -1 && GManager.instance.handtrg == 5 && GManager.instance.sun_power > 0)
                {
                    anim.SetInteger("Anumber", -1);
                }
                no_ground += Time.deltaTime;
                if (no_ground > 2 && GManager.instance.handtrg != 5)
                {
                    anim.SetInteger(numbername, stand_anim);
                }
                if (GManager.instance.handtrg == 5 && GManager.instance.sun_power > 0)
                {
                    ySpeed = -(gravity / 7);
                }
                else
                {
                    ySpeed = -(gravity / 2.5f);
                }
            }
            if (isground.ColTrigger)
            {
                if ((Input.GetKey(KeyCode.Space) && GManager.instance.hand_cooltime <= 0 && player_id == GManager.instance.playerselect && player_id == 0) || (Input.GetKey(KeyCode.Space) && GManager.instance.handtrg == 5 && player_id == 0 && GManager.instance.sun_power > 0))
                {
                    no_ground = 0;
                    movetrg = false;
                    if (anim.GetInteger("Anumber") != -1 && GManager.instance.handtrg == 5 && GManager.instance.sun_power > 0)
                    {
                        anim.SetInteger("Anumber", -1);
                        Instantiate(GManager.instance.effectobj[5], efpos_90.transform.position, efpos_90.transform.rotation);
                    }
                    if (!jumptrg)
                    {
                        audioSource.Stop();
                        GManager.instance.setrg = jump_setrg;
                    }
                    jumptrg = true;
                    if (GManager.instance.handtrg == -1)
                    {
                        anim.SetInteger(numbername, jump_anim);
                    }
                    else if (GManager.instance.handtrg > -1 && GManager.instance.handtrg != 5)
                    {
                        anim.SetInteger(numbername, 810);
                    }
                    ySpeed = (jumptime  * jumpspeed) + gravity;
                    oldp_y = transform.position.y; //ジャンプした位置を記録する
                    jumptime = 0.0f;
                }
                else if (jumptrg || no_ground != 0)
                {
                    jumptrg = false;
                    GManager.instance.hand_cooltime = 0.2f;
                    no_ground = 0;
                }
            }
            else if (jumptrg)
            {
                if (Input.GetKey(KeyCode.Space) && oldp_y + max_height > transform.position.y && maxjumptime > jumptime )
                {
                    ySpeed = (jumptime * jumpspeed) + gravity;
                    jumptime += Time.deltaTime;
                }
                else
                {
                    GManager.instance.hand_cooltime = 0.1f;
                    jumptrg = false;
                    jumptime = 0.0f;
                }
            }
            //----ここからは移動----
            if (player_id == GManager.instance.playerselect && (!movetrg && !jumptrg && isground.ColTrigger) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
            {
                //この部分では歩きの効果音、アニメーションを操作
                movetrg = true;
                audioSource.clip = groundse;
                if (!jumptrg)
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }
                anim.SetInteger(numbername, walk_anim);
            }
            //移動メイン部分
            if (player_id == GManager.instance.playerselect)
            {
                var inputX = Input.GetAxisRaw("Horizontal");
                var inputZ = Input.GetAxisRaw("Vertical");
                var tempVc = new Vector3(-1 * inputZ, 0, inputX);
                if (tempVc.magnitude > 1) tempVc = tempVc.normalized;
                var vec = tempVc;
                var movevec = vec * playerspeed + Vector3.up * (ySpeed);
                if (jumptrg || !isground.ColTrigger)
                {
                    movevec = vec * (playerspeed / 2f) + Vector3.up * (ySpeed);//プレイヤーの移動速度などもここで
                }
                rb.velocity = movevec;

                Vector3 targetPositon = latestPos;
                // 高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
                if (character.transform.position.y != latestPos.y)
                {
                    targetPositon = new Vector3(latestPos.x, character.transform.position.y, latestPos.z);
                }
                Vector3 diff = character.transform.position - targetPositon;
                if (diff != Vector3.zero && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                {
                    Quaternion targetRotation = Quaternion.LookRotation(diff);
                    character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRotation, Time.deltaTime * GManager.instance.kando);
                }
                latestPos = character.transform.position;  //前回のPositionの更新
                                                           //------------
            }

            if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) && !jumptrg  && player_id == GManager.instance.playerselect)
            {
                //移動してない場合、またはジャンプ中の時はアニメーションや音を止める
                if (movetrg)
                {
                    movetrg = false;
                }
                anim.SetInteger(numbername, stand_anim);
                audioSource.loop = false;
                audioSource.Stop();
            }
            else if (!isground.ColTrigger && player_id == 0 && GManager.instance.handtrg == 5 && GManager.instance.sun_power > 0)
            {
                //移動してない場合、またはジャンプ中の時はアニメーションや音を止める
                if (movetrg)
                {
                    movetrg = false;
                }
                audioSource.loop = false;
                audioSource.Stop();
            }
          }
        if (!GManager.instance.walktrg && !rb.isKinematic)
        {
            ySpeed = 0;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
        else if (GManager.instance.walktrg && rb.isKinematic)
        {
            ySpeed = 0;
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
        }
        if (!GManager.instance.walktrg && nowalk_hight != this.transform.position.y)
        {
            Vector3 temphight = this.transform.position;
            temphight.y = nowalk_hight;
            this.transform.position = temphight;
        }
        //else if (!GManager.instance.walktrg && GManager.instance.playerselect == player_id && rb.velocity != Vector3.zero)//動けない場合は音を止め、物理的な動きも止める
        //{
        //    audioSource.Stop();
        //    anim.SetInteger(numbername, stand_anim);
        //    rb.velocity = Vector3.zero;
        //}
        if (player_id != GManager.instance.playerselect && !GManager.instance.over)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && GManager.instance.twoplayermode == "行動")
            {
                GManager.instance.twoplayermode = "待機";
                GManager.instance.setrg = 2;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && GManager.instance.twoplayermode == "待機")
            {
                GManager.instance.twoplayermode = "行動";
                GManager.instance.setrg = 2;
            }
            if (GManager.instance.handtrg != 5 && secondScript.isground.ColTrigger && -secondScript.overhight < secondP.transform.position.y && GManager.instance.twoplayermode =="行動")
            {
                if (!stopcol.onAction)
                {
                    stopcol.onAction = true;
                }
                else if (!rb.useGravity)
                {
                    rb.useGravity = true;
                }
                Vector3 tDir = new Vector3(secondP.transform.position.x, this.transform.position.y, secondP.transform.position.z) - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, tDir, 4 * Time.deltaTime, 0f);
                if (GManager.instance.Triggers[4] < 1)
                {
                    transform.rotation = Quaternion.LookRotation(newDir);
                    Vector3 _forward = this.transform.forward * (playerspeed / 1.5f) + Vector3.up * -(gravity / 2);
                    if (!stopcol.ColTrigger && GManager.instance.walktrg)
                    {
                        rb.velocity = _forward;
                    }
                }
                Vector3 targetPositon = latestPos;
                //高さがずれていると体ごと上下を向いてしまうので便宜的に高さを統一
                if (character.transform.position.y != latestPos.y)
                {
                    targetPositon = new Vector3(latestPos.x, character.transform.position.y, latestPos.z);
                }
                Vector3 diff = character.transform.position - targetPositon;
                if (diff != Vector3.zero && GManager.instance.walktrg && GManager.instance.Triggers[4] < 1 && isground.ColTrigger)
                {
                    if (!movetrg)
                    {
                        movetrg = true;
                        audioSource.clip = groundse;
                        if (!jumptrg)
                        {
                            audioSource.loop = true;
                            audioSource.Play();
                        }
                    }
                    anim.SetInteger(numbername, walk_anim);
                }
                else
                {
                    if (movetrg)
                    {
                        movetrg = false;
                        audioSource.loop = false;
                        audioSource.Stop();
                    }
                    if (GManager.instance.walktrg)
                        anim.SetInteger(numbername, stand_anim);
                }
                latestPos = character.transform.position;  //前回のPositionの更新
            }
            else if (!GManager.instance.walktrg || !secondScript.isground || -secondScript.overhight >= secondP.transform.position.y || GManager.instance.twoplayermode =="待機")//動けない場合は音を止め、物理的な動きも止める
            {
                audioSource.Stop();
                anim.SetInteger(numbername, stand_anim);
                if (rb.velocity != Vector3.zero && !zerotrg)
                {
                    zerotrg = true;
                    rb.velocity = Vector3.zero;
                }
                else
                {
                    rb.velocity = Vector3.up * -gravity;
                }
            }
        }
        else if(GManager.instance.over && anim.GetInteger(numbername) != stand_anim)
        {
            rb.velocity = Vector3.zero;
            anim.SetInteger(numbername, stand_anim);
        }
        if (zerotrg && (GManager.instance.playerselect == player_id || (GManager.instance.playerselect != player_id && GManager.instance.twoplayermode != "待機")))
            zerotrg = false;
        if(player_id == 1 && anim.GetBool("sun"))
        {
            anim.SetBool("sun", false);
        }
        if(player_id == 1 & GManager.instance.handtrg == 5 && !secondP.GetComponent<player>().isground.ColTrigger && !off_tubomi && GManager.instance.sun_power > 0)
        {
            off_tubomi = true;
            for (int i = 0; i < tubomi_model.Length;)
            {
                tubomi_model[i].SetActive(false);
                i++;
            }
        }
        
    }
    public void load_scenechange()
    {
        GManager.instance.walktrg = true;
        SceneManager.LoadScene("stage" + GManager.instance.stageNumber);
    }
    public void reset_load()
    {
        GManager.instance.walktrg = true;
        GManager.instance.ESCtrg = false;
        GManager.instance.over = false;
        GManager.instance.setmenu = 0;
        GManager.instance.txtget = "";
        GManager.instance.endtitle = false;
        GManager.instance.pushtrg = false;
        for (int i = 0; i < GManager.instance.colTrg.Length;)
        {
            GManager.instance.colTrg[i] = false;
            i++;
        }
        for (int i = 0; i < GManager.instance.EventNumber.Length;)
        {
            GManager.instance.EventNumber[i] = PlayerPrefs.GetInt("EvN" + i, 0);
            i++;
        }
        if (GManager.instance.stageNumber == 0)
        {
            GManager.instance.EventNumber[3] = 2;
        }
        else if (GManager.instance.stageNumber == 1)
        {
            GManager.instance.EventNumber[3] = 5;
        }
        else if (GManager.instance.stageNumber == 2)
        {
            GManager.instance.EventNumber[3] = 7;
        }
        else if (GManager.instance.stageNumber == 3)
        {
            GManager.instance.EventNumber[3] = 9;
        }
        for (int i = 0; i < GManager.instance.EventNumber.Length;)
        {
            PlayerPrefs.SetInt("EvN" + i, GManager.instance.EventNumber[i]);
            i++;
        }
        PlayerPrefs.Save();
        for (int i = 0; i < GManager.instance.freenums.Length;)
        {
            GManager.instance.freenums[i] = PlayerPrefs.GetFloat("freenums" + i, 0);
            i++;
        }
        GManager.instance.posX = PlayerPrefs.GetFloat("posX", 0);
        GManager.instance.posY = PlayerPrefs.GetFloat("posY", 0);
        GManager.instance.posZ = PlayerPrefs.GetFloat("posZ", 0);
        GManager.instance.stageNumber = PlayerPrefs.GetInt("stageN", 0);
        //---------------
        for (int i = 0; i < GManager.instance.ItemID.Length;)
        {
            GManager.instance.ItemID[i].itemnumber = PlayerPrefs.GetInt("itemnumber" + i, 0);
            GManager.instance.ItemID[i].gettrg = PlayerPrefs.GetInt("itemget" + i, 0);
            GManager.instance.ItemID[i]._quickset = PlayerPrefs.GetInt("item_quickset" + i, -1);
            GManager.instance.ItemID[i]._equalsset = PlayerPrefs.GetInt("item_equalsset" + i, -1);
            GManager.instance.ItemID[i].pl_equalsselect = PlayerPrefs.GetInt("pl_equalsselect" + i, -1);
            i++;
        }
        //---------------
        for (int i = 0; i < GManager.instance.Pstatus.Length;)
        {
            GManager.instance.Pstatus[i].selectskill = PlayerPrefs.GetInt("pselectskill" + i, -1);
            for (int j = 0; j < GManager.instance.Pstatus[i].inputskill.Length;)
            {
                GManager.instance.Pstatus[i].inputskill[j] = PlayerPrefs.GetInt("pinputskill" + i + "" + j, GManager.instance.Pstatus[i].inputskill[j]);
                j++;
            }
            if (i == 0)
            {
                GManager.instance.Pstatus[i].getpl = PlayerPrefs.GetInt("getpl" + i, 1);
            }
            else
            {
                GManager.instance.Pstatus[i].getpl = PlayerPrefs.GetInt("getpl" + i, 0);
            }
            i++;
        }
        GManager.instance.playerselect = PlayerPrefs.GetInt("plselect", 0);
        for (int i = 0; i < GManager.instance.Triggers.Length;)
        {
            GManager.instance.Triggers[i] = PlayerPrefs.GetInt("gmtrg" + i, 0);
            i++;
        }
        for (int i = 0; i < GManager.instance.achievementsID.Length;)
        {
            GManager.instance.achievementsID[i].gettrg = PlayerPrefs.GetInt("achiget" + i, 0);
            i++;
        }
        GManager.instance.handtrg = -1;
    }
    
}
