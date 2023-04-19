using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItext : MonoBehaviour
{
    public string bossname;
    private GameObject boss;
    public string InputText = "";
    private Text scoreText = null;
    public Image Picon;
    private int oldInt = -1;
    private float oldFloat = 0;
    private string oldString = "";
    private Sprite oldSprite = null;
    private bool oldbool = true;
    private int oldEnglish = 0;
    public Animator textgetanim;
    private float stime = 0;
    private int newlv = 1;
    public Sprite[] set_icon;
    public GameObject active_obj;
    public bool gm_trg = false;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        if (InputText == "textget")
        {
            scoreText.text = GManager.instance.txtget;
            oldString = GManager.instance.txtget;
            Invoke("TextGetAnimEnd", 4.3f);
        }
        else if (InputText == "stage")
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "ステージ" + GManager.instance.stageNumber;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = "Stage" + GManager.instance.stageNumber;
            }
            oldInt = GManager.instance.stageNumber;
        }
        else if (InputText == "stone")
        {
            scoreText.text = GManager.instance.ItemID[10].itemnumber + "×";
            oldInt = GManager.instance.ItemID[10].itemnumber;
        }
        else if (InputText == "picon")
        {
            Picon.sprite = GManager.instance.Pstatus[GManager.instance.playerselect].pimage;
            oldSprite = GManager.instance.Pstatus[GManager.instance.playerselect].pimage;
        }
        else if (InputText == "pname")
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = GManager.instance.Pstatus[GManager.instance.playerselect].pname;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = GManager.instance.Pstatus[GManager.instance.playerselect].pname2;
            }
            oldString = GManager.instance.Pstatus[GManager.instance.playerselect].pname;
        }
        else if (InputText == "hp")
        {
            scoreText.text = GManager.instance.Pstatus[GManager.instance.playerselect].hp.ToString();
            oldInt = GManager.instance.Pstatus[GManager.instance.playerselect].hp;
        }
        else if (InputText == "mp")
        {
            scoreText.text = GManager.instance.Pstatus[GManager.instance.playerselect].mp.ToString();
            oldInt = GManager.instance.Pstatus[GManager.instance.playerselect].mp;
        }
        else if (InputText == "lv")
        {
            scoreText.text = GManager.instance.Pstatus[GManager.instance.playerselect].Lv.ToString();
            oldFloat = GManager.instance.Pstatus[GManager.instance.playerselect].Lv;
        }
        else if (InputText == "hpS")
        {
            scoreText.text = "MaxHP/" + GManager.instance.Pstatus[GManager.instance.playerselect].maxHP;
        }
        else if (InputText == "lvS")
        {
            scoreText.text = "Lv/" + GManager.instance.Pstatus[GManager.instance.playerselect].Lv;
        }
        else if (InputText == "expS")
        {
            scoreText.text = "Next Lv/" + (GManager.instance.Pstatus[GManager.instance.playerselect].maxExp - GManager.instance.Pstatus[GManager.instance.playerselect].inputExp);
        }
        else if (InputText == "atS")
        {
            if (GManager.instance.itemhand != -1)
            {
                scoreText.text = "AT/" + GManager.instance.Pstatus[GManager.instance.playerselect].attack;
            }
            else if (GManager.instance.itemhand == -1)
            {
                scoreText.text = "AT/" + GManager.instance.Pstatus[GManager.instance.playerselect].attack;
            }
        }
        else if (InputText == "dfS")
        {
            scoreText.text = "DF/" + GManager.instance.Pstatus[GManager.instance.playerselect].defense;
        }
        else if (InputText == "spS")
        {
            scoreText.text = "SP/" + GManager.instance.Pstatus[GManager.instance.playerselect].speed;
        }
        if (InputText == "itemcl")
        {
            //for (int i = 0; i < (GManager.instance.ItemID.Length);)
            //{
            //    GManager.instance.ItemID[i].itemnumber = PlayerPrefs.GetInt("itemnumber" + i, 0);
            //    GManager.instance.ItemID[i].gettrg = PlayerPrefs.GetInt("itemget" + i, 0);
            //    i += 1;
            //}
            int allitem = GManager.instance.ItemID.Length;
            int getitem = 0;
            for (int i = 0; i < GManager.instance.ItemID.Length;)
            {
                if (GManager.instance.ItemID[i].gettrg == 1)
                {
                    getitem += 1;
                }
                i++;
            }
            int percent = (100 / allitem) * getitem;
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "アイテム収集率:" + percent + "%";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = "Item collection:" + percent + "%";
            }
        }
        else if (InputText == "reduction")
        {
            if (GManager.instance.reduction == 0)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "軽量化:OFF";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Weight reduction: OFF";
                }
                oldInt = 0;
            }
            else if (GManager.instance.reduction == 1)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "軽量化:ON";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Weight reduction: ON";
                }
                oldInt = 1;
            }
        }
        else if (InputText == "オートダッシュ")
        {
            if (GManager.instance.autolongdash == 0)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "自動長押しダッシュ:OFF";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Auto long-press dash: OFF";
                }
                oldInt = 0;
            }
            else if (GManager.instance.autolongdash == 1)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "自動長押しダッシュ:ON";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Auto long-press dash: ON";
                }
                oldInt = 1;
            }
        }
        else if (InputText == "status")
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "MaxHP:" + GManager.instance.Pstatus[GManager.instance.playerselect].maxHP
                    + "\nMaxMP:" + GManager.instance.Pstatus[GManager.instance.playerselect].maxMP
                    + "\nAT:" + GManager.instance.Pstatus[GManager.instance.playerselect].attack
                    + "\nDF:" + GManager.instance.Pstatus[GManager.instance.playerselect].defense
                    + "\nLV:" + GManager.instance.Pstatus[GManager.instance.playerselect].Lv
                    + "\n次のLvUPまで:" + GManager.instance.Pstatus[GManager.instance.playerselect].maxExp
                    + "\n手に入れたEXP:" + GManager.instance.Pstatus[GManager.instance.playerselect].inputExp;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = "MaxHP:" + GManager.instance.Pstatus[GManager.instance.playerselect].maxHP
                    + "\nMaxMP:" + GManager.instance.Pstatus[GManager.instance.playerselect].maxMP
                    + "\nAT:" + GManager.instance.Pstatus[GManager.instance.playerselect].attack
                    + "\nDF:" + GManager.instance.Pstatus[GManager.instance.playerselect].defense
                    + "\nLV:" + GManager.instance.Pstatus[GManager.instance.playerselect].Lv
                    + "\nNext Lv UP.:" + GManager.instance.Pstatus[GManager.instance.playerselect].maxExp
                    + "\nEXP obtained:" + GManager.instance.Pstatus[GManager.instance.playerselect].inputExp;
            }
            oldInt = GManager.instance.playerselect;
        }
        //else if (InputText == "stagename")
        //{
        //    if (GManager.instance.isEnglish == 0)
        //    {
        //        scoreText.text = "現在地：" + GManager.instance.stageName[GManager.instance.stageNumber];
        //    }
        //    if (GManager.instance.isEnglish == 1)
        //    {
        //        scoreText.fontSize = 28;
        //        scoreText.text = "Current location：" + GManager.instance.stageName2[GManager.instance.stageNumber];
        //    }
        //    oldString = GManager.instance.stageNumber.ToString();
        //}
        else if (InputText == "modetext")
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.fontSize = 24;
                if (GManager.instance.mode == 0)
                {
                    scoreText.text = "難易度：スライム";
                }
                else if (GManager.instance.mode == 1)
                {
                    scoreText.text = "難易度：勇者";
                }
                else if (GManager.instance.mode == 2)
                {
                    scoreText.text = "難易度：魔王";
                }
            }
            if (GManager.instance.isEnglish == 1)
            {
                scoreText.fontSize = 21;
                if (GManager.instance.mode == 0)
                {
                    scoreText.text = "Difficulty: Slime";
                }
                else if (GManager.instance.mode == 1)
                {
                    scoreText.text = "Difficulty: Hero";
                }
                else if (GManager.instance.mode == 2)
                {
                    scoreText.text = "Difficulty: Demon king";
                }
            }
            oldInt = GManager.instance.mode;
        }
        else if (InputText == "modetext_2")
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.fontSize = 24;
                if (GManager.instance.mode == 0)
                {
                    scoreText.text = "難易度：空腹";
                }
                else if (GManager.instance.mode == 1)
                {
                    scoreText.text = "難易度：満腹";
                }
                else if (GManager.instance.mode == 2)
                {
                    scoreText.text = "難易度：暴食";
                }
            }
            if (GManager.instance.isEnglish == 1)
            {
                scoreText.fontSize = 21;
                if (GManager.instance.mode == 0)
                {
                    scoreText.text = "Difficulty: Hunger";
                }
                else if (GManager.instance.mode == 1)
                {
                    scoreText.text = "Difficulty: Full stomach";
                }
                else if (GManager.instance.mode == 2)
                {
                    scoreText.text = "Difficulty: Surfeit";
                }
            }
            oldInt = GManager.instance.mode;
        }
        else if (InputText == "ストーリー")
        {
            if (GManager.instance.isEnglish == 1)
            {
                scoreText.fontSize = 18;
            }
            scoreText.text = GManager.instance.storyUI;
            oldString = GManager.instance.storyUI;
        }
        else if (InputText == "tips")
        {
            if (GManager.instance.Triggers[105] != GManager.instance.Triggers[106])
            {
                Picon.enabled = true;
                oldInt = 1;
            }
            else
            {
                Picon.enabled = false;
                oldInt = 0;
            }
        }
        else if (InputText == "hand")
        {
            if (GManager.instance.handtrg == -1)
            {
                scoreText.text = "????????";
            }
            else if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = GManager.instance._hand[GManager.instance.handtrg].hand_name[0];
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = GManager.instance._hand[GManager.instance.handtrg].hand_name[1];
            }
            if (GManager.instance.playerselect <= 1)
            {
                Picon.sprite = set_icon[GManager.instance.playerselect];
            }
            oldInt = GManager.instance.handtrg;
        }
        else if (InputText == "plselect")
        {
            if (GManager.instance.playerselect <= 1)
            {
                if (GManager.instance.isEnglish == 0 && gm_trg)
                {
                    scoreText.text = GManager.instance.StageName[GManager.instance.stageNumber].jp_charascript[GManager.instance.playerselect];
                }
                else if (GManager.instance.isEnglish == 1 && gm_trg)
                {
                    scoreText.text = GManager.instance.StageName[GManager.instance.stageNumber].en_charascript[GManager.instance.playerselect];
                }
                if (Picon != null)
                {
                    Picon.sprite = set_icon[GManager.instance.playerselect];
                }
            }
            if (active_obj != null)
            {
                if (GManager.instance.playerselect == 0)
                {
                    active_obj.SetActive(true);
                }
                else if (GManager.instance.playerselect == 1)
                {
                    active_obj.SetActive(false);
                }
            }
            oldInt = GManager.instance.playerselect;
        }
        else if (InputText == "menuhp")
        {
            scoreText.text = "HP："+GManager.instance.StageName[GManager.instance.stageNumber].riri_hp.ToString()+"%" ;
            oldInt = GManager.instance.playerselect;
        }
        else if (InputText == "plname")
        {
            if (GManager.instance.isEnglish == 0)
            {
                if (GManager.instance.playerselect == 0)
                {
                    scoreText.text = "リリーちゃん";
                }
                else if (GManager.instance.playerselect == 1)
                {
                    scoreText.text = "ツボミン";
                }
            }
            else if (GManager.instance.isEnglish == 1)
            {
                if (GManager.instance.playerselect == 0)
                {
                    scoreText.text = "Lily";
                }
                else if (GManager.instance.playerselect == 1)
                {
                    scoreText.text = "Tsubomin";
                }
            }
        }
        else if (InputText == "sun")
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "光合成："+ Mathf.Floor(GManager.instance.sun_power * 100).ToString() + "%";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = "Energy：" + Mathf.Floor(GManager.instance.sun_power * 100).ToString() + "%";
            }
            oldFloat = GManager.instance.sun_power;
        }
    }
    void TextGetAnimEnd()
    {
        textgetanim.SetInteger("Anumber", 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (oldEnglish != GManager.instance.isEnglish)
        {
            oldEnglish = GManager.instance.isEnglish;
            if (InputText == "hand")
            {
                if (GManager.instance.handtrg == -1)
                {
                    scoreText.text = "????????";
                }
                else if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = GManager.instance._hand[GManager.instance.handtrg].hand_name[0];
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = GManager.instance._hand[GManager.instance.handtrg].hand_name[1];
                }
                if (GManager.instance.playerselect <= 1)
                {
                    Picon.sprite = set_icon[GManager.instance.playerselect];
                }
                oldInt = GManager.instance.handtrg;
            }
            if (InputText == "plselect")
            {
                if (GManager.instance.playerselect <= 1)
                {
                    if (GManager.instance.isEnglish == 0 && gm_trg)
                    {
                        scoreText.text = GManager.instance.StageName[GManager.instance.stageNumber].jp_charascript[GManager.instance.playerselect];
                    }
                    else if (GManager.instance.isEnglish == 1 && gm_trg)
                    {
                        scoreText.text = GManager.instance.StageName[GManager.instance.stageNumber].en_charascript[GManager.instance.playerselect];
                    }
                    if (Picon != null)
                    {
                        Picon.sprite = set_icon[GManager.instance.playerselect];
                    }
                }
                if (active_obj != null)
                {
                    if (GManager.instance.playerselect == 0)
                    {
                        active_obj.SetActive(true);
                    }
                    else if (GManager.instance.playerselect == 1)
                    {
                        active_obj.SetActive(false);
                    }
                }
                oldInt = GManager.instance.playerselect;
            }
            if (InputText == "plname")
            {
                if (GManager.instance.isEnglish == 0)
                {
                    if (GManager.instance.playerselect == 0)
                    {
                        scoreText.text = "リリーちゃん";
                    }
                    else if (GManager.instance.playerselect == 1)
                    {
                        scoreText.text = "ツボミン";
                    }
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    if (GManager.instance.playerselect == 0)
                    {
                        scoreText.text = "Lily";
                    }
                    else if (GManager.instance.playerselect == 1)
                    {
                        scoreText.text = "Tsubomin";
                    }
                }
            }
            else if (InputText == "sun")
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "光合成：" + Mathf.Floor(GManager.instance.sun_power * 100).ToString() + "%";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Energy：" + Mathf.Floor(GManager.instance.sun_power * 100).ToString() + "%";
                }
                oldFloat = GManager.instance.sun_power;
            }
        }
        if (InputText == "textget" && oldString != GManager.instance.txtget && GManager.instance.setmenu <= 0)
        {
            scoreText.text = GManager.instance.txtget;
            oldString = GManager.instance.txtget;
            textgetanim.SetInteger("Anumber", 0);
            Invoke("TextGetAnimEnd", 4.3f);
        }
        if (InputText == "picon" && oldSprite != GManager.instance.Pstatus[GManager.instance.playerselect].pimage)
        {
            Picon.sprite = GManager.instance.Pstatus[GManager.instance.playerselect].pimage;
            oldSprite = GManager.instance.Pstatus[GManager.instance.playerselect].pimage;
        }
        if (InputText == "reduction" && oldInt != GManager.instance.reduction)
        {
            if (GManager.instance.reduction == 0)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "軽量化:OFF";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Weight reduction: OFF";
                }
                oldInt = 0;
            }
            else if (GManager.instance.reduction == 1)
            {
                if (GManager.instance.isEnglish == 0)
                {
                    scoreText.text = "軽量化:ON";
                }
                else if (GManager.instance.isEnglish == 1)
                {
                    scoreText.text = "Weight reduction: ON";
                }
                oldInt = 1;
            }
        }
        if (InputText == "hand" && oldInt != GManager.instance.handtrg)
        {
            if (GManager.instance.handtrg == -1)
            {
                scoreText.text = "????????";
            }
            else if (GManager.instance.isEnglish == 0 && GManager.instance._hand.Length > GManager.instance.handtrg)
            {
                scoreText.text = GManager.instance._hand[GManager.instance.handtrg].hand_name[0];
            }
            else if (GManager.instance.isEnglish == 1 && GManager.instance._hand.Length > GManager.instance.handtrg)
            {
                scoreText.text = GManager.instance._hand[GManager.instance.handtrg].hand_name[1];
            }
            if (GManager.instance.playerselect <= 1)
            {
                Picon.sprite = set_icon[GManager.instance.playerselect];
            }
            oldInt = GManager.instance.handtrg;
        }
        if (InputText == "sun" && oldFloat != GManager.instance.sun_power)
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "光合成：" + Mathf.Floor(GManager.instance.sun_power*100).ToString() + "%";
            }
            else if (GManager.instance.isEnglish == 1)
            {
                scoreText.text = "Energy：" + Mathf.Floor(GManager.instance.sun_power * 100).ToString() + "%";
            }
            oldFloat = GManager.instance.sun_power;
        }
        if(InputText == "twoplayer" && oldString != GManager.instance.twoplayermode)
        {
            if (GManager.instance.isEnglish == 0)
            {
                scoreText.text = "連れ歩きモード(Shift)："+ GManager.instance.twoplayermode;
            }
            else if (GManager.instance.isEnglish == 1)
            {
                if(GManager.instance.twoplayermode == "行動")
                    scoreText.text = "Walking with mode(Shift)：Action";
                else if (GManager.instance.twoplayermode == "待機")
                    scoreText.text = "Walking with mode(Shift)：Stop";
            }
            oldString = GManager.instance.twoplayermode;
        }
    }
}
