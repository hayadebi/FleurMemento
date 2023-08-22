
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.IO;
using System;
using UnityEngine.UI;

public class EndSpell : MonoBehaviour
{
    public string check_trg = "fleur_spelltrg";
    public string[] word_list;
    public Text spelltext;
    // Start is called before the first frame update
    void Start()
    {
        //Scoreの値が7と一致するオブジェクト検索
        string tmptrg = PlayerPrefs.GetString(check_trg, "");
        if (tmptrg == "")
        {
            FetchSpell();
        }
        else spelltext.text = tmptrg;
    }
    public void FetchSpell()
    {
        var tmpday = DateTime.Now;
        var tmpminute = tmpday.Minute;
        if (tmpminute >= word_list.Length) tmpminute = UnityEngine.Random.Range(0, word_list.Length);
        var tmpsecond = tmpday.Second;
        if (tmpsecond >= word_list.Length) tmpsecond = UnityEngine.Random.Range(0, word_list.Length);
        string tmpspell = "ふるーる"+word_list[tmpday.Day]+ word_list[tmpday.Hour]+ word_list[tmpminute]+ word_list[tmpsecond];
        spelltext.text = tmpspell;
        PlayerPrefs.SetString(check_trg, tmpspell);
        PlayerPrefs.Save();
        NCMBObject obj = new NCMBObject("SpellCode");
        obj.Add("gametype", "スライムディストピア");
        obj.Add("spell", tmpspell);
        obj.SaveAsync((NCMBException e) => {
            if (e != null)
            {
               //----------------------
            }
            else
            {
                //---------------------
            }
        });
    }
}

