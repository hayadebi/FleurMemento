using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Flowchart))]
public class npcsay : MonoBehaviour
{
    public localLnpc llnpc = null;
    public sayTrigger saySc = null;
    public Transform P = null;
    public float py = 0;
    public objAngle objangle = null;
    public Animator eanim = null;
    public bool audiostop = false;
    public GameObject bossoffobj = null;
    public int eventnumber = 0;
    public int defaulteventadd = 1;
    public int[] addevent;
    public int[] addEnumber;
    public GameObject wall;
    public AudioClip bgm;
    public AudioClip eventsound;
    public int GetCoin;
    public int Trigger = -1;
    public int allID;
    public int inputitemnumber;
    public float returnTime = 3;
    public GameObject[] UI;
    public bool nulleventdestroy = false;
    public int nullversion = 1;//1はe!=i、2はe<i、3はe>i
    public int[] shopID;
    public int[] stoneID;
    //-----------
    public int EventNumber = -1;
    public string DestroyOBJtext = "";
    public bool nextevent = true;
    public bool sayreturn;
    public int inputNumber;
    public int npctype;
    public int missionID;
    public int subID = -1;
    public int missionnumber;
    public string PlayerTag = "Player";
    bool saytrg = false;
    public string message = "test";
    bool isTalking = false;
    Flowchart flowChart;
    public string storyText = "";
    public string storyText2 = "";
    public int inputslskill = -1;
    public BoxCollider boxc;
    public SphereCollider spcol;
    public int getItem = -1;
    public int getPlayer = -1;
    public int selectSlime = -1;
    public int _AddMelancholy = 0;
    public bool UIsummon = false;
    public int _inputLocal = 0;
    public int[] tips_trg;
    public int subEv_index = 0;
    public int subEv_numover = 0;

    public int input_hand = -1;
    public handevent get_hand = null;
    public handevent start_hand = null;
    public bool goal_trg = false;
    public GameObject goal_player = null;
    public GameObject goal_tubomi = null;
    public bool debugtrg = false;
    private bool scenechange = false;
    public AudioSource endaudio;
    public AudioClip endbgm;
    public GameObject endui;
    // Start is called before the first frame update
    void Start()
    {
        if (Trigger != -1)
        {
            if (GManager.instance.Triggers[Trigger] == 1)
            {
                Destroy(gameObject);
            }
        }
        flowChart = this.GetComponent<Flowchart>();
        if (nulleventdestroy && inputNumber != GManager.instance.EventNumber[eventnumber] && nullversion == 1)
        {
            Destroy(gameObject);
        }
        else if (nulleventdestroy && inputNumber > GManager.instance.EventNumber[eventnumber] && nullversion == 2)
        {
            Destroy(gameObject);
        }
        else if (nulleventdestroy && inputNumber < GManager.instance.EventNumber[eventnumber] && nullversion == 3)
        {
            Destroy(gameObject);
        }
        llnpc = this.GetComponent<localLnpc>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!saytrg && npctype == 2 && GManager.instance.walktrg)
        {
            saytrg = true;
            StartCoroutine(Talk());
        }
        else if (!saytrg && npctype == 3 && inputNumber == GManager.instance.EventNumber[eventnumber] && GManager.instance.walktrg)
        {
            saytrg = true;
            StartCoroutine(Talk());
        }
    }
    private void OnTriggerStay(Collider col)
    {
        //if (GManager.instance.handtrg == input_hand && Input.GetMouseButtonDown(0) && input_hand != -1 && col.tag == "hand" && col.gameObject.GetComponent<handevent>() && col.gameObject.GetComponent<handevent>().hand_id == input_hand && !saytrg && GManager.instance.walktrg)
        //{
        //    get_hand = col.gameObject.GetComponent<handevent>();
        //    get_hand.event_area = true;
        //    if (saySc == null)
        //    {
        //        saytrg = true;
        //        StartCoroutine(Talk());
        //    }
        //    else if (saySc != null && !saySc.saystop)
        //    {
        //        saytrg = true;
        //        StartCoroutine(Talk());
        //    }
        //}
        if (col.tag == PlayerTag && !saytrg && npctype == 12 && inputNumber == GManager.instance.EventNumber[eventnumber] && GManager.instance.walktrg && GManager.instance.handtrg == -1)
        {
            if (!goal_trg || (goal_trg &&(goal_player.transform.position - goal_tubomi.transform.position).magnitude<= 2f))
            {
                if (saySc == null)
                {
                    saytrg = true;
                    StartCoroutine(Talk());
                }
                else if (saySc != null && !saySc.saystop)
                {
                    saytrg = true;
                    StartCoroutine(Talk());
                }
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if ( input_hand != -1 && GManager.instance.handtrg == input_hand && col.tag == "hand" && col.gameObject.GetComponent<handevent>() && col.gameObject.GetComponent<handevent>().hand_id == input_hand && !saytrg && GManager.instance.walktrg)
        {
            get_hand = col.gameObject.GetComponent<handevent>();
            get_hand.event_area = true;
            if (saySc == null)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
            else if (saySc != null && !saySc.saystop)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
        }
        if (col.tag == PlayerTag && !saytrg && npctype == 0 && GManager.instance.walktrg)
        {
            if (saySc == null)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
            else if (saySc != null && !saySc.saystop)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
        }
        else if (col.tag == PlayerTag && !saytrg && npctype == 1 && inputNumber == GManager.instance.EventNumber[eventnumber] && GManager.instance.walktrg)
        {
            if (saySc == null)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
            else if (saySc != null && !saySc.saystop)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
        }
        else if (col.tag == PlayerTag && !saytrg && npctype == 4 && GManager.instance.walktrg)
        {
            if (saySc == null)
            {
                if (GManager.instance.ItemID[allID].itemnumber > (inputitemnumber - 1))
                {
                    sayreturn = false;
                    GManager.instance.ItemID[allID].itemnumber -= inputitemnumber;
                    GManager.instance.Triggers[Trigger] = 1;
                    message = "itemget";
                    saytrg = true;
                    StartCoroutine(Talk());
                }
                else if (GManager.instance.ItemID[allID].itemnumber < inputitemnumber)
                {
                    message = "itemnot";
                    saytrg = true;
                    StartCoroutine(Talk());
                }
            }
            else if (saySc != null && !saySc.saystop)
            {
                if (GManager.instance.ItemID[allID].itemnumber > (inputitemnumber - 1))
                {
                    sayreturn = false;
                    GManager.instance.ItemID[allID].itemnumber -= inputitemnumber;
                    GManager.instance.Triggers[Trigger] = 1;
                    message = "itemget";
                    saytrg = true;
                    StartCoroutine(Talk());
                }
                else if (GManager.instance.ItemID[allID].itemnumber < inputitemnumber)
                {
                    message = "itemnot";
                    saytrg = true;
                    StartCoroutine(Talk());
                }
            }
        }
        if (col.tag == PlayerTag && !saytrg && npctype == 9 && inputslskill != -1 && GManager.instance.walktrg && GManager.instance.Pstatus[GManager.instance.playerselect].slskillID != inputslskill)
        {
            if (saySc == null)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
            else if (saySc != null && !saySc.saystop)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
        }
        else if (col.tag == PlayerTag && !saytrg && npctype == 10 && inputNumber == GManager.instance.EventNumber[eventnumber] && GManager.instance.walktrg)
        {
            if (subEv_numover <= GManager.instance.EventNumber[subEv_index])
            {
                message = "trueEv" + inputNumber.ToString();
            }
            else
            {
                message = "badEv" + inputNumber.ToString();
            }

            if (saySc == null)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
            else if (saySc != null && !saySc.saystop)
            {
                saytrg = true;
                StartCoroutine(Talk());
            }
        }
        else if (col.tag == PlayerTag && !saytrg && npctype == 12 && inputNumber == GManager.instance.EventNumber[eventnumber] && GManager.instance.walktrg && GManager.instance.handtrg == -1)
        {
            if (!goal_trg || (goal_trg && (goal_player.transform.position - goal_tubomi.transform.position).magnitude <= 1.5f))
            {
                if (saySc == null)
                {
                    saytrg = true;
                    StartCoroutine(Talk());
                }
                else if (saySc != null && !saySc.saystop)
                {
                    saytrg = true;
                    StartCoroutine(Talk());
                }
            }
        }
    }

    public IEnumerator Talk()
    {
        GManager.instance.walktrg = false;
        GManager.instance.twoplayermode = "行動";
        GameObject[] get_players = GameObject.FindGameObjectsWithTag("player");
        foreach (GameObject playerobj in get_players)
        {
            if (playerobj.GetComponent<player>())
            {
                player temp_p = playerobj.GetComponent<player>();
                temp_p.rb.velocity = Vector3.zero;
                temp_p.rb.isKinematic = true;
                temp_p.anim.SetInteger(temp_p.numbername, temp_p.stand_anim);
                temp_p.audioSource.Stop();
            }
        }
        GManager.instance.saytrg = true;
        if (saySc != null)
        {
            saySc.saystop = true;
        }
        if (P != null)
        {
            P.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            P.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationZ;
        }
        if (start_hand != null && start_hand.hand_id == GManager.instance.handtrg )
        {
            start_hand.p_anim.SetInteger("Anumber", 0);
            start_hand.p_anim.SetBool("hand", false);
            GManager.instance.handtrg = -1;
            start_hand = null;
        }
        if (selectSlime != -1 && GManager.instance.playerselect != selectSlime)
        {
            GManager.instance.playerselect = selectSlime;
        }
        
        //GManager.instance.EventNumber[11] += _AddMelancholy;
        if(eanim != null)
        {
            eanim.enabled = false;
        }
        if (objangle != null)
        {
            objangle.enabled = false;
        }
        if(P != null)
        {
            var ppos = P.position;
            ppos.y = py+0.1f;
            P.position = ppos;
        }
        if (EventNumber == 1 )
        {
            saveN();
            GManager.instance.Pstatus[GManager.instance.playerselect].hp = GManager.instance.Pstatus[GManager.instance.playerselect].maxHP;
        }
        else if(EventNumber == 3)
        {
            GameObject P = GameObject.Find("Player");
            wall.SetActive(true);
            GManager.instance.Pstatus[GManager.instance.playerselect].hp = GManager.instance.Pstatus[GManager.instance.playerselect].maxHP;
            GManager.instance.Pstatus[GManager.instance.playerselect].mp = GManager.instance.Pstatus[GManager.instance.playerselect].maxMP;
        }
        if(bgm != null )
        {
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
        }
        if (isTalking)
        {
            yield break;
        }
        isTalking = true;
        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        isTalking = false;
        GManager.instance.saytrg = false;
        GManager.instance.walktrg = true;
        if (P != null)
        {
            P.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            P.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        if (get_hand != null)
        {
            get_hand.p_anim.SetInteger("Anumber", 0);
            get_hand.p_anim.SetBool("hand", false);
            GManager.instance.handtrg = -1;
            Destroy(get_hand.gameObject.gameObject);
            get_hand = null;
        }
        if (tips_trg != null)
        {
            for (int i = 0; i < tips_trg.Length;)
            {
                if (GManager.instance.Triggers[GManager.instance._Tips[tips_trg[i]].tips_trgID] < 1)
                {
                    GManager.instance.Triggers[GManager.instance._Tips[tips_trg[i]].tips_trgID] = 1;
                    GManager.instance.Triggers[106] += 1;
                }
                i += 1;
            }
        }
        if (getItem != -1)
        {
            GManager.instance.ItemID[getItem].itemnumber += 1;
            GManager.instance.ItemID[getItem].gettrg = 1;
        }
        if (getPlayer != -1)
        {
            if (GManager.instance.isEnglish == 0)
            {
                GManager.instance.txtget = GManager.instance.Pstatus[getPlayer].pname + "を仲間にしました";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                GManager.instance.txtget = GManager.instance.Pstatus[getPlayer].pname + " is now a member of our group.";
            }
            GManager.instance.setrg = 1;
            GManager.instance.Pstatus[getPlayer].getpl = 1;
        }
        if(storyText != "" && UI != null && UI.Length != 0)
        {
            if (GManager.instance.isEnglish == 0)
            {
                GManager.instance.storyUI = storyText;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                GManager.instance.storyUI = storyText2;
            }
            Instantiate(UI[0], transform.position, transform.rotation);
        }
        if (eanim != null)
        {
            eanim.enabled = true;
        }
        if (objangle != null)
        {
            objangle.enabled = true;
        }
        if (nextevent == true)
        {
            GManager.instance.EventNumber[eventnumber] += defaulteventadd;
            if(addevent.Length != 0)
            {
                for (int i = 0; i < addevent.Length;)
                {
                    if(addEnumber.Length != 0)
                    {
                        GManager.instance.EventNumber[addevent[i]] = addEnumber[i];
                    }
                    else
                    {
                        GManager.instance.EventNumber[addevent[i]] += defaulteventadd;
                    }
                    i++;
                }
            }
        }
        if (sayreturn == true)
        {
            Invoke("SayTrg", returnTime);
        }
        if (EventNumber == 2 || UIsummon)
        {
            GManager.instance.walktrg = false;
            GManager.instance.setmenu = 1;
            Instantiate(UI[0], transform.position, transform.rotation);
        }
        if (UI != null && _inputLocal != 0)
        {
            GManager.instance.walktrg = false;
            GManager.instance.setmenu = 1;
            GManager.instance.EventNumber[16] = _inputLocal;
            Instantiate(UI[0], transform.position, transform.rotation);
        }
        if (EventNumber == 6)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Application.OpenURL("https://twitter.com/hayadebi");
            Application.Quit();
        }
        else if (EventNumber == 7)
        {
            if(GManager.instance.isEnglish == 0)
            {
                GManager.instance.txtget = GManager.instance.ItemID[allID].itemname + "を手に入れた！";
            }
            else if(GManager.instance.isEnglish == 1)
            {
                GManager.instance.txtget = "I've got the " + GManager.instance.ItemID[allID].itemname2 + "！";
            }
            GManager.instance.ItemID[allID].itemnumber += 1;
            GManager.instance.ItemID[allID].gettrg = 1;
        }
        else if (EventNumber == 8)
        {
            if (GManager.instance.isEnglish == 0)
            {
                GManager.instance.txtget = GManager.instance.achievementsID[allID].name + "の実績を解除した！";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                GManager.instance.txtget = "I released the " + GManager.instance.achievementsID[allID].name2 + " achievement！";
            }
            GManager.instance.setrg = 1;
            GManager.instance.achievementsID[allID].gettrg = 1;
        }
        if (EventNumber == 10)
        {
            if (GManager.instance.isEnglish == 0)
            {
                GManager.instance.txtget = GManager.instance.Pstatus[allID].pname + "が仲間になりました";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                GManager.instance.txtget = GManager.instance.Pstatus[allID].pname2 + " has joined our group.";
            }
            GManager.instance.setrg = 12;
            GManager.instance.Pstatus[allID].getpl = 1;
        }
        if (UI != null && EventNumber == 12)
        {
            GManager.instance.walktrg = false;
            GManager.instance.setmenu = 1;
            Instantiate(UI[0], transform.position, transform.rotation);
        }

        if (bgm != null&& audiostop == false && llnpc != null && !llnpc.bgmplay)
        {
            GameObject BGM = GameObject.Find("BGM");
            AudioSource bgmA = BGM.GetComponent<AudioSource>();
            bgmA.Stop();
            bgmA.clip = bgm;
            bgmA.Play();
        }
        if (saySc != null)
        {
            saySc.saystop = false;
        }
        if (DestroyOBJtext != "" && EventNumber != 3)
        {
            GameObject obj = GameObject.Find(DestroyOBJtext);
            Destroy(obj.gameObject);
        }
        if(goal_trg && !debugtrg )
        {
            GManager.instance.stageNumber += 1;
            saveN();
            Invoke("SceneChange", 0.1f);
        }
        else if(goal_trg && debugtrg)
        {
            resetN();
            GManager.instance.walktrg = false;
            endaudio.Stop();
            endaudio.clip = endbgm;
            endaudio.Play();
            GManager.instance.endtitle = true;
            GManager.instance.fleurendtrg = 1;
            PlayerPrefs.SetInt("fleur_endtrg", 1);
            PlayerPrefs.Save();
            Instantiate(endui, transform.position, transform.rotation);
        }
    }
    void SceneChange()
    {
        if (!scenechange)
        {
            scenechange = true;
            SceneManager.LoadScene("stage" + GManager.instance.stageNumber.ToString());
        }
    }
    void SayTrg()
    {
        saytrg = false;
    }
    public void resetN()
    {
        GManager.instance.walktrg = true;
        GManager.instance.ESCtrg = false;
        GManager.instance.over = false;
        GManager.instance.setmenu = 0;
        GManager.instance.txtget = "";
        GManager.instance.endtitle = false;
        GManager.instance.pushtrg = false;
        for (int i = 0; i < GManager.instance.EventNumber.Length;)
        {
            GManager.instance.EventNumber[i] = 0;
            i++;
        }
        for (int i = 0; i < GManager.instance.freenums.Length;)
        {
            GManager.instance.freenums[i] = 0;
            i++;
        }
        GManager.instance.posX = 0;
        GManager.instance.posY = 0;
        GManager.instance.posZ = 0;
        GManager.instance.stageNumber = 0;
        for (int i = 0; i < GManager.instance.ItemID.Length;)
        {
            GManager.instance.ItemID[i].itemnumber = 0;
            GManager.instance.ItemID[i].gettrg = 0;
            GManager.instance.ItemID[i]._quickset = -1;
            GManager.instance.ItemID[i]._equalsset = -1;
            GManager.instance.ItemID[i].pl_equalsselect = -1;
            i++;
        }
        for (int i = 0; i < GManager.instance.Quick_itemSet.Length;)
        {
            GManager.instance.Quick_itemSet[i] = -1;
            i++;
        }
        for (int i = 0; i < GManager.instance.P_equalsID.Length;)
        {
            GManager.instance.P_equalsID[i].hand_equals = -1;
            GManager.instance.P_equalsID[i].accessory_equals = -1;
            i++;
        }
        for (int i = 0; i < GManager.instance.Pstatus.Length;)
        {
            GManager.instance.Pstatus[i].selectskill = -1;
            if (i == 0)
            {
                GManager.instance.Pstatus[i].getpl = 1;
            }
            else
            {
                GManager.instance.Pstatus[i].getpl = 0;
            }
            i++;
        }
        GManager.instance.playerselect = 0;
        for (int i = 0; i < GManager.instance.Triggers.Length;)
        {
            GManager.instance.Triggers[i] = 0;
            i++;
        }
        for (int i = 0; i < GManager.instance.achievementsID.Length;)
        {
            GManager.instance.achievementsID[i].gettrg = 0;
            i++;
        }
        GManager.instance.handtrg = -1;
    }
    public void saveN()
    {
        for (int i = 0; i < GManager.instance.EventNumber.Length;)
        {
            PlayerPrefs.SetInt("EvN" + i, GManager.instance.EventNumber[i]);
            i++;
        }
        for (int i = 0; i < GManager.instance.freenums.Length;)
        {
            PlayerPrefs.SetFloat("freenums" + i, GManager.instance.freenums[i]);
            i++;
        }
        PlayerPrefs.SetFloat("posX", GManager.instance.posX);
        PlayerPrefs.SetFloat("posY", GManager.instance.posY);
        PlayerPrefs.SetFloat("posZ", GManager.instance.posZ);
        PlayerPrefs.SetInt("stageN", GManager.instance.stageNumber);
        for (int i = 0; i < GManager.instance.ItemID.Length;)
        {
            PlayerPrefs.SetInt("itemnumber" + i, GManager.instance.ItemID[i].itemnumber);
            PlayerPrefs.SetInt("itemget" + i, GManager.instance.ItemID[i].gettrg);
            PlayerPrefs.SetInt("item_quickset" + i, GManager.instance.ItemID[i]._quickset);
            PlayerPrefs.SetInt("item_equalsset" + i, GManager.instance.ItemID[i]._equalsset);
            PlayerPrefs.SetInt("pl_equalsselect" + i, GManager.instance.ItemID[i].pl_equalsselect);
            i++;
        }

        for (int i = 0; i < GManager.instance.Quick_itemSet.Length;)
        {
            PlayerPrefs.SetInt("quick_itemset" + i, GManager.instance.Quick_itemSet[i]);
            i++;
        }
        for (int i = 0; i < GManager.instance.P_equalsID.Length;)
        {
            PlayerPrefs.SetInt("hand_equals" + i, GManager.instance.P_equalsID[i].hand_equals);
            PlayerPrefs.SetInt("accessory_equals" + i, GManager.instance.P_equalsID[i].accessory_equals);
            i++;
        }
        //---------------
        for (int i = 0; i < GManager.instance.Pstatus.Length;)
        {
            PlayerPrefs.SetInt("pmaxhp" + i, GManager.instance.Pstatus[i].maxHP);
            PlayerPrefs.SetInt("php" + i, GManager.instance.Pstatus[i].hp);
            PlayerPrefs.SetInt("pmaxmp" + i, GManager.instance.Pstatus[i].maxMP);
            PlayerPrefs.SetInt("pmp" + i, GManager.instance.Pstatus[i].mp);
            PlayerPrefs.SetInt("pdf" + i, GManager.instance.Pstatus[i].defense);
            PlayerPrefs.SetInt("pat" + i, GManager.instance.Pstatus[i].attack);
            PlayerPrefs.SetInt("plv" + i, GManager.instance.Pstatus[i].Lv);
            PlayerPrefs.SetInt("pmaxexp" + i, GManager.instance.Pstatus[i].maxExp);
            PlayerPrefs.SetInt("pinputexp" + i, GManager.instance.Pstatus[i].inputExp);
            PlayerPrefs.SetInt("pselectskill" + i, GManager.instance.Pstatus[i].selectskill);
            for (int j = 0; j < GManager.instance.Pstatus[i].inputskill.Length;)
            {
                PlayerPrefs.SetInt("pinputskill" + i + "" + j, GManager.instance.Pstatus[i].inputskill[j]);
                j++;
            }
            PlayerPrefs.SetInt("getpl" + i, GManager.instance.Pstatus[i].getpl);
            i++;
        }
        PlayerPrefs.SetInt("plselect", GManager.instance.playerselect);
        for (int i = 0; i < GManager.instance.Triggers.Length;)
        {
            PlayerPrefs.SetInt("gmtrg" + i, GManager.instance.Triggers[i]);
            i++;
        }
        for (int i = 0; i < GManager.instance.achievementsID.Length;)
        {
            PlayerPrefs.SetInt("achiget" + i, GManager.instance.achievementsID[i].gettrg);
            i++;
        }
        PlayerPrefs.SetFloat("audioMax", GManager.instance.audioMax);
        PlayerPrefs.SetFloat("seMax", GManager.instance.seMax);
        PlayerPrefs.SetInt("Mode", GManager.instance.mode);
        PlayerPrefs.SetInt("isEn", GManager.instance.isEnglish);
        PlayerPrefs.SetInt("Reduction", GManager.instance.reduction);
        PlayerPrefs.SetInt("longDash", GManager.instance.autolongdash);
        PlayerPrefs.SetFloat("rotpivot", GManager.instance.rotpivot);
        PlayerPrefs.SetFloat("siya", GManager.instance.siya);
        PlayerPrefs.Save();
    }
}
