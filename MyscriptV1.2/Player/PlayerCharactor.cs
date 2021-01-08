using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class PlayerCharactor:Charactor
{
    public MapListNode playerPosnode;
   
    Transform PlayerPos;
    RougeMapCreate RougeMapCreateScirpt;
    PlayerCharactor my_player;
    public playerMovement playerMovementScript;
    public findTheWayPlayer findTheWayPlayerScript;
    public Image HPslider;
    public Image HPslider_effect;
    public Image Mpslider;
    float HPsliderAmount;
    float MPsliderAmount;
    public bool HPslider_start_change;

    //
    
    public Inventory current_bag;
    public Player_Panel current_playerpanel;
    


    //
    public Text Name_text;
    public Text Level;
    //

    public int current_exp;
    int Levelupdate_exp=100;
    //
    //
    public int Magic_Attack;
    public int criticalstrike_rate;

    public int Defence_model_defence_addvalue;

    public Text Player_Current_Attack;
    public Text Player_Current_Defence;
    public Text player_Current_PathStep;
    public Text Player_Current_Name;


    public ParticleSystem HPbuff;

    /// <summary>
    /// 存档功能API
    /// </summary>
    /// >

   
    public PlayerCharactor(string name, int HP, int MP, int base_attack, int base_defence, int dodge) : base(name, HP, MP, base_attack, base_defence, dodge)
    {

        //Chara_name = "武士喵";
        //this.HP = 200;
        //this.MP = 200;
        //this.base_attack = 30;
        //this.base_defence = 20;
        //this.dodge = 20;
    }

    void Start()
    {   
        PlayerPos = GetComponent<Transform>();
        playerMovementScript = GetComponent<playerMovement>();
        RougeMapCreateScirpt = GameObject.FindGameObjectWithTag("RougeMap").GetComponent<RougeMapCreate>();
        whichData_choicen(GameSaveManager.instance.thedataNum);
        WhenStart_PlayerText();
        Defence_model_defence_addvalue = 10;
        HPslider_start_change = false;
        

    }


    public void whichData_choicen(theChoicen_playerData theChoicen_PlayerData)
    {
        if (theChoicen_PlayerData == theChoicen_playerData.newplayerData)
        {
            Debug.Log("载入了一个新存档");
            load_NwePlayerDatafromSave();
        }
        else if (theChoicen_PlayerData == theChoicen_playerData.player1Data)
        {
            load_Player1DatafromSave();
        }
        else if (theChoicen_PlayerData == theChoicen_playerData.player2Data)
        {
            load_Player2DatafromSave();
        }
    }

    public void WhenStart_PlayerText()
    {
        Name_text.text = this.Chara_name;
        
        Level.text = this.level.ToString();
    }

    public void Start_Game_init()
    {
        HPsliderAmount = (float)HP / 100;
        HPslider.fillAmount = HPsliderAmount;
        HPslider_effect.fillAmount = HPsliderAmount;
        MPsliderAmount = (float)MP / 100;
        Mpslider.fillAmount = MPsliderAmount;

    }
    public void playerLevelup()
    {
        this.base_attack += 25;
        level = level+1;
        Level.text = level.ToString();
    }
    public void Get_hurt_Reflash_playerStatus()
    {   
        HPsliderAmount =(float) HP / 100;
        HPslider.fillAmount = HPsliderAmount;
        MPsliderAmount = (float)MP / 100;
        Mpslider.fillAmount = MPsliderAmount;
        HPslider_start_change = true;
        bool isdead = IsPlayerisDead();
        if (isdead == true)
        {
           
            playerMovementScript.deadAnim();
            battleManager.instance.Current_Game_State = Game_state.player_Dead;
            Invoke("playerDeadSetactive", 3f);
           
        }
    }

    public void load_NwePlayerDatafromSave()
    {
        this.base_attack = NewPlayerData.instance.attack_value;
        this.base_defence = NewPlayerData.instance.defence_value;
        this.dodge = NewPlayerData.instance.dodge;
        this.Magic_Attack = NewPlayerData.instance.magic_attack;
        this.level = NewPlayerData.instance.level;
        this.Chara_name = NewPlayerData.instance.player_name;
        this.HP = NewPlayerData.instance.HP;
        this.MP = NewPlayerData.instance.MP;
        this.coin = NewPlayerData.instance.gold_Coin;
       // GameSaveManager.instance.Ishad_currentBag();
        current_bag = GameSaveManager.instance.newPlayerBagData;
        Debug.Log("载入背包");
       
    }

    public  void load_Player1DatafromSave()
    {
        this.base_attack = Player1Data.instance.attack_value;
        this.base_defence = Player1Data.instance.defence_value;
        this.dodge = Player1Data.instance.dodge;
        this.Magic_Attack = Player1Data.instance.magic_attack;
        this.level = Player1Data.instance.level;
        this.Chara_name = Player1Data.instance.Player_name;
        this.HP = Player1Data.instance.HP;
        this.MP = Player1Data.instance.MP;
        this.coin = Player1Data.instance.gold_Coin;
       // GameSaveManager.instance.Ishad_Player1Bag();
        current_bag = GameSaveManager.instance.player1BagData;
    }

    public void load_Player2DatafromSave()
    {
        this.base_attack = Player2Data.instance.attack_value;
        this.base_defence = Player2Data.instance.defence_value;
        this.dodge = Player2Data.instance.dodge;
        this.Magic_Attack = Player2Data.instance.magic_attack;
        this.level = Player2Data.instance.level;
        this.Chara_name = Player2Data.instance.player_name;
        this.HP = Player2Data.instance.HP;
        this.MP = Player2Data.instance.MP;
        this.coin = Player2Data.instance.gold_Coin;
        current_bag = GameSaveManager.instance.player2BagData;
    }

    public void create_recover_NewPlayer1Data()//在savemanager里调用
    {
        Player1Data.instance.attack_value = this.base_attack;
        Player1Data.instance.defence_value = this.base_defence;
        Player1Data.instance.magic_attack = this.Magic_Attack;
        Player1Data.instance.dodge = this.dodge;
        Player1Data.instance.level = this.level;
        Player1Data.instance.Player_name = this.Chara_name;
        Player1Data.instance.HP = this.HP;
        Player1Data.instance.MP = this.MP;
        Player1Data.instance.gold_Coin = this.coin;
       // GameSaveManager.instance.SavePlayer1Bagdata(current_bag);
    }

    public void create_recover_NewPlayer2Data()
    {
        Player2Data.instance.attack_value = this.base_attack;
        Player2Data.instance.defence_value = this.base_defence;
        Player2Data.instance.magic_attack = this.Magic_Attack;
        Player2Data.instance.dodge = this.dodge;
        Player2Data.instance.level = this.level;
        Player2Data.instance.player_name = this.Chara_name;
        Player2Data.instance.HP = this.HP;
        Player2Data.instance.MP = this.MP;
        Player2Data.instance.gold_Coin = this.coin;
    }
    public void new_gamePLayerchar()
    {
        this.Chara_name = "武士喵";
        this.HP = 100;
        this.MP = 200;
        this.base_attack = 100;
        this.base_defence = 20;
        this.dodge = 20;
        this.level = 1;
        Level.text = level.ToString();
    }

    // Start is called before the first frame update
  
    public override void normal_attack(Charactor other)
    {
        Player_panelManager.instance.reflash_PlayerPanel();
        Player_panelManager.instance.reflash_PlayerPanelData();
        int damage =buff_Attackvalue+ current_attack_withbag - other.base_defence-other.buff_DefenceValue;
        if (damage > 0)
        {
            other.HP = other.HP - damage;
        }
    }

    public void player_attack(EnemyChara enemyChara)
     {
        //Debug.Log("进入攻击主函数");
        normal_attack(enemyChara);
        enemyChara.gameObject.GetComponent<EnemyChara>().Get_hurt_Reflash_Enemy_Status();
        enemyChara.gameObject.GetComponent<EnemyMovement>().EnemyGetHurtAnim();
        

    }
    
    public void HPslider_Effect()
    {

    }


    public void Update()
    {
        if (HPslider_start_change == true)
        {
            if (HPslider_effect.fillAmount > HPslider.fillAmount)
            {
                HPslider_effect.fillAmount -= 0.3f * Time.deltaTime;
            }
            else
            {
                HPslider_effect.fillAmount= HPslider.fillAmount;
                HPslider_start_change = false;
            }
        }

    }

    public bool IsPlayerisDead()
    {
        if (this.HP <= 0)
        {
            return true;
        }
        return false;
    }
    
    public void DefenceMode()
    {
        base_defence =  base_defence + Defence_model_defence_addvalue;
        Player_panelManager.instance.reflash_PlayerPanel();
        Player_panelManager.instance.reflash_PlayerPanelData();
    }

    
    public void cancel_DefenceMode()
    {
        Debug.Log("防御力恢复");
        base_defence = base_defence - Defence_model_defence_addvalue;
        Player_panelManager.instance.reflash_PlayerPanel();
        Player_panelManager.instance.reflash_PlayerPanelData();
    }

    public void playerDeadSetactive()
    {
        transform.gameObject.SetActive(false);
    }

    public int current_attack_withbag;
    public int current_defence_withbag;
    public int current_magic_attack_withbag;
    public int current_dodge;


    public void Refrash_The_Current_PlayerData()/////////////////刷新玩家buff数据
    {
        Player_Current_Name.text = this.Chara_name;
        Player_Current_Attack.text = (current_attack_withbag + buff_Attackvalue).ToString();
        Player_Current_Defence.text = (current_defence_withbag + buff_DefenceValue).ToString();
        player_Current_PathStep.text = (findTheWayPlayerScript.TheBuff_addPathStep + findTheWayPlayerScript.numberofStep).ToString();

    }

}
