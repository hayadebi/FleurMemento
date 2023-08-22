using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endcheck : MonoBehaviour
{
    public string check_trg = "fleur_endtrg";
    public int isenddestroy = 1;
    public Image img=null;
    public Button btn;
    public bool setactive = false;
    // Start is called before the first frame update
    void Start()
    {
        if(GManager.instance.fleurendtrg<1) GManager.instance.fleurendtrg = PlayerPrefs.GetInt(check_trg, 0);
        if (GManager.instance.fleurendtrg == isenddestroy &&btn==null&& img==null) this.gameObject.SetActive(setactive);
        else
        {
            this.gameObject.SetActive(!setactive);
        }
        if (GManager.instance.fleurendtrg == isenddestroy && img != null)
        {
            var tmpalpha = img.color;
            
            tmpalpha.a = 0.25f;
            img.color = tmpalpha;
        }
        if (GManager.instance.fleurendtrg == isenddestroy && btn != null)
        {
            btn.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
