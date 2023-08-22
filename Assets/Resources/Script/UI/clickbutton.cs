using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickbutton : MonoBehaviour
{
    public string settingtype = "";
    public float addsettingfloat;
    public int addsettingint;
    public bool menutrg = false;
    public bool addstage = false;
    public bool stagetrg = false;
    public bool resettrg = false;
    public float maxUI = 0;
    public string nextscene;
    public GameObject settingUI;
    public GameObject fadeinUI;
    public AudioClip clickse;
    AudioSource audioSource;
    public bool subTrg = false;
    private int oldrandom = 0;
    public Animator anim_;
    public string a_name;
    public int a_setnumber;
    public GameObject target_obj;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetOnView()
    {
        GManager.instance.setrg = 2;
        target_obj.SetActive(true);
    }
    public void SetNotView()
    {
        GManager.instance.setrg = 2;
        target_obj.SetActive(false);
    }
    public void animClick()
    {
        if(anim_)
        {
            GManager.instance.setrg = 2;
            anim_.SetInteger(a_name, a_setnumber);
        }
    }
    public void animBool()
    {
        if (anim_)
        {
            anim_.SetBool(a_name, !anim_.GetBool(a_name));
        }
    }
    public void endgo_twitter()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.OpenURL("https://unitygamehayadebi.jimdofree.com/2023/03/26/%E3%82%B9%E3%83%A9%E3%82%A4%E3%83%A0%E3%83%87%E3%82%A3%E3%82%B9%E3%83%88%E3%83%94%E3%82%A2-%E9%96%8B%E7%99%BA-%E4%BD%9C%E5%93%81%E7%B4%B9%E4%BB%8B/");
        Application.Quit();
    }
    public void settingClick()
    {
        if (GManager.instance.setmenu < maxUI && GManager.instance.walktrg == true)
        {
            GManager.instance.setmenu += 1;
            GManager.instance.walktrg = false;
            audioSource.PlayOneShot(clickse);
            if (menutrg == false)
            {
                Instantiate(settingUI, transform.position, transform.rotation);
            }
            else if (menutrg == true)
            {
                Instantiate(GManager.instance.spawnUI, transform.position, transform.rotation);
            }
        }
        else if (GManager.instance.setmenu < maxUI && GManager.instance.setmenu > 0)
        {
            GManager.instance.setmenu += 1;
            GManager.instance.walktrg = false;
            audioSource.PlayOneShot(clickse);
            if (menutrg == false)
            {
                Instantiate(settingUI, transform.position, transform.rotation);
            }
            else if (menutrg == true)
            {
                Instantiate(GManager.instance.spawnUI, transform.position, transform.rotation);
            }
        }
        else if(GManager.instance.setmenu < maxUI && GManager.instance.walktrg == false)
        {
            GManager.instance.setmenu += 1;
            GManager.instance.walktrg = false;
            audioSource.PlayOneShot(clickse);
            if (menutrg == false)
            {
                Instantiate(settingUI, transform.position, transform.rotation);
            }
            else if (menutrg == true)
            {
                Instantiate(GManager.instance.spawnUI, transform.position, transform.rotation);
            }
        }
    }

    public void startClick()
    {
        audioSource.PlayOneShot(clickse);
        Instantiate(fadeinUI, transform.position, transform.rotation);
        Resources.UnloadUnusedAssets();
        Invoke("GameStart", 3.0f);
    }

    public void quitClick()
    {
        Application.Quit();
    }
    public void DestroyClick()
    {
        GManager.instance.walktrg = true;
        Destroy(gameObject);
    }
    
    void GameStart()
    {
        if (menutrg == false)
        {
            if (resettrg == true)
            {
                resetN();
            }
            else if (resettrg == false)
            {
                loadN();
            }
        }
        else if (menutrg == true)
        {
            SceneManager.LoadScene(GManager.instance.SceneText);
        }
        if (stagetrg == false)
        {
            SceneManager.LoadScene(nextscene);
        }
        else if (stagetrg == true)
        {
            SceneManager.LoadScene(nextscene + GManager.instance.stageNumber);
        }
    }
    public void Slider()
    {
        if(settingtype == "audio")
        {
            GManager.instance.audioMax += addsettingfloat;
            if(GManager.instance.audioMax > 1)
            {
                GManager.instance.audioMax = 1;
            }
            else if (GManager.instance.audioMax < 0)
            {
                GManager.instance.audioMax = 0;
            }
        }
        if (settingtype == "se")
        {
            GManager.instance.seMax += addsettingfloat;
            if (GManager.instance.seMax > 1)
            {
                GManager.instance.seMax = 1;
            }
            else if (GManager.instance.seMax < 0)
            {
                GManager.instance.seMax = 0;
            }
        }
        else if (settingtype == "mode")
        {
            GManager.instance.mode += addsettingint;
            if (GManager.instance.mode> 2)
            {
                GManager.instance.mode = 2;
            }
            else if (GManager.instance.mode < 0)
            {
                GManager.instance.mode = 0;
            }
        }
        else if (settingtype == "kando")
        {
            GManager.instance.kando += addsettingint;
            if (GManager.instance.kando > 15)
            {
                GManager.instance.kando = 15;
            }
            else if (GManager.instance.kando < 1)
            {
                GManager.instance.kando = 1;
            }
        }
        else if(settingtype == "reduction")
        {
            if(GManager.instance.reduction == 0)
            {
                GManager.instance.reduction = 1;
            }
            else if(GManager.instance.reduction == 1)
            {
                GManager.instance.reduction = 0;
            }
        }
        else if (settingtype == "オートダッシュ")
        {
            if (GManager.instance.autolongdash == 0)
            {
                GManager.instance.autolongdash = 1;
            }
            else if (GManager.instance.autolongdash  == 1)
            {
                GManager.instance.autolongdash  = 0;
            }
        }
        else if (settingtype == "回転速度")
        {
            GManager.instance.rotpivot += addsettingfloat;
            if (GManager.instance.rotpivot > 4)
            {
                GManager.instance.rotpivot = 4;
            }
            else if (GManager.instance.rotpivot < 1)
            {
                GManager.instance.rotpivot = 1;
            }
        }
    }

    public void JapaneseL()
    {
        GManager.instance.isEnglish = 0;
    }
    public void EnglishL()
    {
        GManager.instance.isEnglish = 1;
    }
    public void resetN()
    {
        //PlayerPrefs.DeleteAll();
        GManager.instance.walktrg = true;
        GManager.instance.ESCtrg = false;
        GManager.instance.over = false;
        GManager.instance.setmenu = 0;
        GManager.instance.txtget = "";
        GManager.instance.endtitle = false;
        GManager.instance.pushtrg = false;
        for(int i = 0;i < GManager.instance.EventNumber.Length;)
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
        for(int i = 0;i < GManager.instance.Triggers.Length;)
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
    public void loadN()
    {
        //後でやる
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
            GManager.instance.EventNumber[i] = PlayerPrefs.GetInt("EvN"+i, 0);
            i++;
        }
        for (int i = 0; i < GManager.instance.freenums.Length;)
        {
            GManager.instance.freenums[i] = PlayerPrefs.GetFloat("freenums"+i,0);
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
        for (int i = 0; i < GManager.instance.Quick_itemSet.Length;)
        {
            GManager.instance.Quick_itemSet[i] = PlayerPrefs.GetInt("quick_itemset" + i, -1);
            i++;
        }
        for (int i = 0; i < GManager.instance.P_equalsID.Length;)
        {
            GManager.instance.P_equalsID[i].hand_equals = PlayerPrefs.GetInt("hand_equals" + i, -1);
            GManager.instance.P_equalsID[i].accessory_equals = PlayerPrefs.GetInt("accessory_equals" + i, -1);
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
                GManager.instance.Pstatus[i].getpl = PlayerPrefs.GetInt("getpl"+i,1);
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
            GManager.instance.Triggers[i] = PlayerPrefs.GetInt("gmtrg"+i, 0); 
            i++;
        }
        for (int i = 0; i < GManager.instance.achievementsID.Length;)
        {
            GManager.instance.achievementsID[i].gettrg = PlayerPrefs.GetInt("achiget" + i, 0);
            i++;
        }
        if(GManager.instance.stageNumber != 0)
        {
            GManager.instance.audioMax = PlayerPrefs.GetFloat("audioMax", 0.16f);
            GManager.instance.seMax = PlayerPrefs.GetFloat("seMax", 0.16f);
            GManager.instance.mode = PlayerPrefs.GetInt("Mode", 1);
            GManager.instance.isEnglish = PlayerPrefs.GetInt("isEn", 0);
            GManager.instance.reduction = PlayerPrefs.GetInt("Reduction", 0);
            GManager.instance.autolongdash = PlayerPrefs.GetInt("longDash", 1);
            GManager.instance.rotpivot = PlayerPrefs.GetFloat("rotpivot", 1.6f);
            GManager.instance.rotpivot = PlayerPrefs.GetFloat("rotpivot", 1.6f);
            GManager.instance.siya = PlayerPrefs.GetFloat("siya", 60);
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
