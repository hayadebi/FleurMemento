using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public bool resettrg = false;
    public static GManager instance = null;
    public float sun_power = 0;
    public bool walktrg = false; //動ける状態か
    public bool saytrg = false;
    public int handtrg = -1;//new！
    public bool ESCtrg = false; //Escを押してるかどうか、または強制的にEscさせるための
    public bool over = false; //ゲームオーバーかどうか
    public int setmenu = 0; //UIの表示状態、0はUIが無い時を示す
    public int itemhand = 0; //手に持ってる状態
    public string txtget; //様々なとこから一時的に格納する文章
    public bool endtitle = false; //いずれ使う
    public int[] EventNumber; //各イベント状態、0はそのイベントが進行していないことを示す
    public float[] freenums; //各々のスクリプトが使う、一時的な数値
    public bool pushtrg = false; //一時的な変数

    //プレイヤーの現在位置を格納、セーブする用。再開時に使用
    public float posX = 0; 
    public float posY = 0;
    public float posZ = 0;
    public int setrg = -1;
    public int stageNumber = 1; //現在のステージID
    //設定
    public float audioMax = 0.16f; //音量設定に使用
    public float seMax = 0.16f;//効果音設定に使用
    public int mode = 1; //難易度設定に使用
    public int isEnglish = 0; //言語設定に使用
    public float kando = 1; //感度設定に使用
    public int reduction = 0; //画面効果設定に使用
    public int autolongdash = 1; //自動ダッシュ設定に使用
    public float rotpivot = 5; //回転速度設定に使用
    public float siya = 60;//new！

    [System.Serializable]
    public struct item
    {
        //各アイテム情報
        public string itemname;
        [Multiline]
        public string itemscript;
        public Sprite itemimage;
        public int eventnumber;
        public int itemnumber;
        public GameObject itemobj;
        public string itemname2;
        [Multiline]
        public string itemscript2;
        public int gettrg;
        public int _equalsset;
        public int pl_equalsselect;
        public int _quickset;
    }
    public item[] ItemID;

    [System.Serializable]
    public struct _Equals
    {
        //各装備情報
        public int hand_equals;
        public int accessory_equals;
    }
    public _Equals[] P_equalsID;

    public int[] Quick_itemSet;//アイテムスロットの状態
    public int _quickSelect = -1;//現在選択してるアイテムスロット
    //------------------------------
    public int itemselect; //現在選択しているアイテム
    [System.Serializable]
    public struct player
    {
        //各プレイヤーの情報
        public Sprite pimage;
        public string pname;
        public string pname2;
        [Multiline]
        public string pscript;
        [Multiline]
        public string pscript2;
        public int maxHP;
        public int hp;
        public int maxMP;
        public int mp;
        public float speed;
        public int defense;
        public int attack;
        public int Lv;
        public int maxExp;
        public int inputExp;
        public int[] inputskill;//秘伝用にセーブ
        public int selectskill;
        public GameObject pobj;
        public float loadtime;
        public float maxload;
        public int changemode;
        public int getpl;
        public int slskillID;
    }
    public player[] Pstatus;

    //new
    public int playerselect; //現在操作しているプレイヤー(スライム)
    [System.Serializable]
    public struct skill
    {
        //各スキル情報
        [Header("スキル名")]
        public string skillname;
        public string skillname2;
        [Header("スキル説明")]
        [Multiline]
        public string skillscript;
        [Multiline]
        public string skillscript2;
        [Header("スキル使用後何秒発動できないか")]
        public int skillmaxbar;
        [Header("スキルアイコン")]
        public Sprite skillicon;
        [Header("発動効果か持続効果なのか")]
        public bool notrg;
        [Header("スキル効果によって発生するオブジェクト")]
        public GameObject inputskillobj;
    }
    public skill[] SkillID;
    public int skillselect; //現在選択しているスキル
    
    public GameObject[] effectobj; //汎用的なエフェクトを格納
    public int animmode = -1; //一時的な、アニメーションを再生するための変数
    public int[] Triggers; //各トリガーの状態。イベントとは違い、この宝箱は一度取ってあるのか、この敵は討伐した奴かどうかなどを格納
    public string SceneText; //一時的なステージ名を指定する用
    public GameObject spawnUI; //表示させるUIを、会話イベントスクリプト等から一時的に格納
    public AudioClip ase; //一時的な効果音を格納する用
    public string sayobjname; //会話イベントで一時的に使用
    [System.Serializable]
    public struct achievements
    {
        //各実績情報
        [Multiline]
        public string name;
        [Multiline]
        public string name2;
        [Multiline]
        public string script;
        [Multiline]
        public string script2;
        public int gettrg;
        public Sprite image;
    }
    public achievements[] achievementsID;

    public Vector3 mouseP; //現在のマウス位置

    [System.Serializable]
    public struct StageID
    {
        public string jp_name;
        public string en_name;
        [Multiline] public string[] jp_charascript;
        [Multiline] public string[] en_charascript;
        public int riri_hp;
    }
    public StageID[] StageName;//new！

    public string storyUI = ""; //章の始終で使用する、一時的な短い文章

    public bool[] colTrg;//一時的な、コライダー取得用
    public AudioClip[] managerSE; //汎用的な効果音を格納

    public float[] instantP;//(一時的な、会話中に位置情報を保存する用)

    [System.Serializable]
    public struct Tips_ID
    {
        public Sprite tips_image;
        public string[] tips_name;
        [Multiline]
        public string[] tips_script;
        public int tips_trgID;
    }
    public Tips_ID[] _Tips;

    [System.Serializable]
    public struct hand_ID
    {
        public string[] hand_name;
    }
    public hand_ID[] _hand;

    public string twoplayermode = "行動";
    public float hand_cooltime = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if(hand_cooltime >= 0)
        {
            hand_cooltime -= Time.deltaTime;
        }
    }

}