using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public enum Game_state
{    
    player_Win,
    player_Dead,
    has_enemyAlive,
    
}

public enum Player_num
{
    player1,
    player2,
    player3,
}


public enum Allbattle_state
{   
    Player_state,
    Enemy1_state,
    Enemy2_state,
    Enemy3_state,
    Enemy4_state,
    player_dead,
    allenemy_dead

}

public enum Battle_State//Player战斗状态机
{
    StartRoundstay,
    player_roundChoice,
    player_path_choice,
    player_attack_choice,
    player_move,
    player_attack,
    player_defence,
    player_magic,
    player_roundEnd,
  
    
}


public enum Enemy_state
{
    enemy_search,
    enemy_patrol,//巡逻
    enemy_movetoPlayer,
    enemy_attack
}

public enum Player_operation
{   none,
    attack,
    move,
    defence,
    magic,
    roundEnd
}

public class battleManager : MonoBehaviour
{
    public static battleManager instance;
    //UI类
    public GameObject player_choicePanel;//选项版面
    public GameObject player_attack_button;
    public GameObject player_move_button;
    public GameObject player_defence_button;
    public GameObject player_MagicAttack_button;
    public GameObject cancel_operation_button;
    public GameObject playerWin;
    public GameObject playerDie;
    public GameObject MenuButton;


    //状态机
    public Battle_State current_state;
    public Player_operation current_PLayerOperate;
    public Allbattle_state current_Allbattle_state;
    public Game_state Current_Game_State;
   
    //脚本
    public reflashMap reflashMapScript;
    public readAndsetMap readAndsetMapScript;
    public findTheWayPlayer findTheWayPlayerScript;
    playerMovement playerMovementScript;
    public AlltheEnemy AlltheEnemyScript;
    public cameraControl cameraControlScript;
    public Menu MenuScript;
    //
    public GameObject Player;
    public PlayerCharactor PlayerCharactorScript;
    public GameObject[] enemyList;
    //
    public GameObject CMplayer;
    public GameObject[] CMenemylist;
    
   //player的状态机开关
    public bool isActive_roundPanel;//进入玩家操控先激活板面。
    public bool click_Player_startCho_Path;
    public bool Isclick_player_start_attack;
    public bool hadClick_player_cho_attack;
    public bool hadCLickPlayer_cho_pathend;
    public bool hadClick_Player_Defence;
    Ray mouseHit;
    //
    public int lastRound_Defence;
    //
    //
    public bool playerArrive;
    public int battleRoundCount;
    public Text battleRoundCountText;


    //
    public bool enemy1AIenter;
    public bool enemy2AIenter;
    public bool enemy3AIenter;
    public bool enemy4AIenter;
    //
    public bool enemy1AIroundFinish;
    public bool enemy2AIroundFinish;
    public bool enemy3AIroundFinish;
    public bool enemy4AIroundFinish;
    //
    public int[] whichisalive;
 
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;

        playerMovementScript = Player.GetComponent<playerMovement>();
        PlayerCharactorScript = Player.GetComponent<PlayerCharactor>();
        MenuScript = MenuButton.GetComponent<Menu>();
        enemyList = AlltheEnemyScript.enemyList;
        
        click_Player_startCho_Path = false;
        Isclick_player_start_attack = false;                                                                                
        hadCLickPlayer_cho_pathend = false;
        hadClick_player_cho_attack = false;
        hadClick_Player_Defence = false;

        lastRound_Defence = 10000;

        isActive_roundPanel = false;
        playerArrive = false;
       
        current_state = Battle_State.StartRoundstay;
        current_PLayerOperate = Player_operation.none;
        current_Allbattle_state = Allbattle_state.Player_state;
        Current_Game_State = Game_state.has_enemyAlive;
        whichisalive = new int[4];
        battleRoundCount = 1;
        battleRoundCountText.text = battleRoundCount.ToString();

        //
        enemy1AIenter = false;
        enemy2AIenter = false;
        enemy3AIenter = false;
        enemy4AIenter = false;
        //
        enemy1AIroundFinish = false;
        enemy2AIroundFinish = false;
        enemy3AIroundFinish = false;
        enemy4AIroundFinish = false;
    }
    
    public void startRound()
    {
    
    }

    public void Choice_Attack()
    {
        current_PLayerOperate = Player_operation.attack;

    }
    public void Choice_Move()
    {
        if (playerArrive == false)
        {
            current_PLayerOperate = Player_operation.move;
        }
    }
    public void Choice_Defence()
    {
        current_PLayerOperate = Player_operation.defence;
    }
    public void cancel_operation()//取消操作按钮
    {
        current_PLayerOperate = Player_operation.none;
        cancel_operation_button.SetActive(false);

    }
    public void choice_EndtheRound()
    {
        current_PLayerOperate = Player_operation.roundEnd;
    }




    /// <summary>
    /// /
    /// </summary>
    public void confirm_operation()//确认操作
    {
        if (current_PLayerOperate == Player_operation.move)
        {
            current_state = Battle_State.player_path_choice;
        }
        else if (current_PLayerOperate == Player_operation.attack)
        {
            current_state = Battle_State.player_attack;
        }
        else if (current_PLayerOperate == Player_operation.magic)
        {
            current_state = Battle_State.player_magic;
        }
        else if (current_PLayerOperate == Player_operation.roundEnd)
        {
            current_state = Battle_State.player_roundEnd;
        }
        else if (current_PLayerOperate == Player_operation.defence)
        {
            current_state = Battle_State.player_defence;
        }
    }
    // Update is called once per frame
    


    public void Player_battleTime()//在update里
    {
        if (current_state == Battle_State.StartRoundstay)//
        {    
            if (isActive_roundPanel == false)
            {
                player_choicePanel.SetActive(true);
                player_attack_button.SetActive(true);
                player_move_button.SetActive(true);
                player_defence_button.SetActive(true);
                player_MagicAttack_button.SetActive(true);
                isActive_roundPanel = true;
            }
        }
        else if (current_state == Battle_State.player_attack)
        {   


            if (player_defence_button.activeSelf == true)
            {
                player_defence_button.SetActive(false);
            }


            if (Input.GetMouseButtonDown(0) && Isclick_player_start_attack == false)
            {
                Debug.Log("进入Attack函数");
                readAndsetMapScript.Click_player_attack(current_state);
            }
            else if (Input.GetMouseButtonDown(0) && hadClick_player_cho_attack == false)
            {
                int enemyNumber=readAndsetMapScript.getwhichone_attack(current_state);

                if (enemyNumber != -1)
                {
                    playerMovementScript.AttackAnim();
                    PlayerCharactorScript.player_attack(enemyList[enemyNumber].GetComponent<EnemyChara>());
                    player_attack_button.SetActive(false);
                }
            }
        }
        else if (current_state==Battle_State.player_defence)
        {    


            if (player_attack_button.activeSelf == true)
            {
                player_attack_button.SetActive(false);
            }


            if (hadClick_Player_Defence == false)
            {
                Debug.Log("进入防御状态");
                player_defence_button.SetActive(false);
                MenuScript.Defence_PanelAnim();
                playerMovementScript.DefenceAnim();
                PlayerCharactorScript.DefenceMode();
                hadClick_Player_Defence = true;
                lastRound_Defence = battleRoundCount;
            }

        }
       
        else if (current_state == Battle_State.player_path_choice)//进入选路模块
        {
            if (Input.GetMouseButtonDown(0) && click_Player_startCho_Path == false)//选中玩家
            {
               
                 Debug.Log("进入鼠标点下");
                readAndsetMapScript.Click_player(current_state);
                //if (Isclick_player == false)
                //{
                //    click_Player_startCho_Path = false;
                //}

            }
            else if (Input.GetMouseButtonDown(0) && hadCLickPlayer_cho_pathend == false)//选中目的地
            {
                Debug.Log("进入move目标选择");
                //hadCLickPlayer_cho_pathend = true;
                readAndsetMapScript.get_endPathPoint(current_state);
                //hadCLickPlayer_cho_pathend = true;

                if (hadCLickPlayer_cho_pathend == true)
                {
                    current_state = Battle_State.player_move;
                }
                
            }
        }
        else if (current_state == Battle_State.player_move&&playerArrive==false)//开始移动
        {    
           
            playerArrive = true;
            playerMovementScript.start_move_Coroutine();
            player_move_button.SetActive(false);

        }
        else if (current_state == Battle_State.player_roundEnd)
        {
            player_choicePanel.SetActive(false);
            isActive_roundPanel = false;
            battleRoundCount++;
            battleRoundCountText.text = battleRoundCount.ToString();
            hadCLickPlayer_cho_pathend = false;//行走choice归位
            click_Player_startCho_Path = false;
            hadClick_player_cho_attack = false;
            Isclick_player_start_attack = false;
            hadClick_Player_Defence = false;
            if (battleRoundCount - lastRound_Defence == 2)
            {
                PlayerCharactorScript.cancel_DefenceMode();
                lastRound_Defence = 10000;
            }

           
            readAndsetMapScript.setStartAttackboolReflesh();
            readAndsetMapScript.setStartboolReflesh();
            playerArrive = false;
           
           //行走choice归位
            for(int i = 0; i < enemyList.Length; i++)
            {
               if(judge_enemyAlive(i) == true)
                {
                    whichisalive[i] =1;
                    
                }
                else if (judge_enemyAlive(i) == false)
                {
                    whichisalive[i] = -1;
                }
            }
            int enemy_index = first_aliveEnemy();
            active_true_allEnemyCM();
            CMplayer.SetActive(false);
            Debug.Log(enemy_index + "几号激活");/////////////////////找到第一个存活敌人
            if (enemy_index == -1)
            {
                Current_Game_State = Game_state.player_Win;
            }
            else if (enemy_index ==0)
            {
                current_Allbattle_state = Allbattle_state.Enemy1_state;
                Debug.Log("1号激活");
            }
            else if (enemy_index == 1)
            {
                current_Allbattle_state = Allbattle_state.Enemy2_state;
            }
            else if (enemy_index == 2)
            {
                current_Allbattle_state = Allbattle_state.Enemy3_state;
            }
            else if (enemy_index == 3)
            {
                current_Allbattle_state = Allbattle_state.Enemy4_state;
            }
            current_state = Battle_State.StartRoundstay;
        }
       
    }
    

    public void enemy_battleTime(int enemy_Index)//启动AI模块////////////在此脚本的update里
    {   
        if (enemy_Index==1)
        {
            enemyList[0].GetComponent<EnemyAI>().startAI = true;

        }
        else if (enemy_Index == 2)
        {   

            enemyList[1].GetComponent<EnemyAI>().startAI = true;

        }
        else if (enemy_Index == 3)
        {   

            enemyList[2].GetComponent<EnemyAI>().startAI = true;

        }
        else if (enemy_Index == 4)
        {    

            enemyList[3].GetComponent<EnemyAI>().startAI = true;
        }
    }




    public bool judge_enemyAlive(int Enemy_index)
    {
        if (enemyList[Enemy_index].activeSelf == true)
        {
            return true;
        }
        return false;
    }


    public void active_true_allEnemyCM()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i].activeSelf == true)
            {
                cameraControlScript.cameraGroup[i].gameObject.SetActive(true);
            }
           else if (enemyList[i].activeSelf == false)
            {
                cameraControlScript.cameraGroup[i].gameObject.SetActive(false);
            }
        }
    }

    public int first_aliveEnemy()//寻找数组顺序下的第一个活着的敌人
    {
        int index=-1;
        for(int i = 0; i < whichisalive.Length; i++)
        {
            if (whichisalive[i] == 1)
            {
                index = i;
                break;
            }
        }
        return index;


    }


    public int theNext_aliveEnemy(int nowEnemy_Index)//敌人行动完后进入下一个环节

    {
        int the_next_alive_index=-1;
        int next_index = nowEnemy_Index + 1;
        if (nowEnemy_Index == 3)
        {
            the_next_alive_index = -1;
            return the_next_alive_index;
        }
        else
        {
           
            for (int i = next_index; i < whichisalive.Length; i++)
            {
                if (whichisalive[i] == 1)
                {
                    the_next_alive_index = i;
                    return the_next_alive_index;
                }
            }
        }

        return next_index;
    }

    public void setActive_nextenemy(int number)
    {
       
      if (number == 0)
        {
            current_Allbattle_state = Allbattle_state.Enemy1_state;
            Debug.Log("1号激活");
        }
        else if (number == 1)
        {
            current_Allbattle_state = Allbattle_state.Enemy2_state;
            Debug.Log("2号激活");
        }
        else if (number == 2)
        {
            current_Allbattle_state = Allbattle_state.Enemy3_state;
        }
        else if (number == 3)
        {
            current_Allbattle_state = Allbattle_state.Enemy4_state;
        }
    }


    public void refresh_allEnemyAIstart_trigger()
    {   


        Debug.Log("进入刷新AI开关");
        enemy1AIenter = false;
        enemy2AIenter = false;
        enemy3AIenter = false;
        enemy4AIenter = false;
        enemy1AIroundFinish = false;
        enemy2AIroundFinish = false;
        enemy3AIroundFinish = false;
        enemy4AIroundFinish = false;



    }

    void Update()
    {
        

        if (Current_Game_State == Game_state.has_enemyAlive)
        {

            if (current_Allbattle_state == Allbattle_state.Player_state)
            {
               
                //Debug.Log("进入玩家状态");
                Player_battleTime();

            }
            else if (current_Allbattle_state == Allbattle_state.Enemy1_state)
            {
                // Debug.Log("进入Enemy1状态");
                int enemy_index = 1;
                if (whichisalive[enemy_index - 1] == -1)
                {
                    enemy1AIenter = true;
                    enemy1AIroundFinish = true;
                    cameraControlScript.setoff_deadenemy(enemy_index - 1);
                }
                if (enemy1AIenter == false)
                {
                    // Debug.Log("ENemy1进入AIstart=true");

                    enemy_battleTime(enemy_index);
                    enemy1AIenter = true;
                }
                else if (enemy1AIroundFinish == true)
                {
                    //Debug.Log("进入1号行动结束的程序，启动下一个");
                    int nextAliveEnemy = theNext_aliveEnemy(enemy_index - 1);
                    if (nextAliveEnemy != -1)
                    {
                        cameraControlScript.setoff_deadenemy(enemy_index - 1);
                        setActive_nextenemy(nextAliveEnemy);//改变allbattleState
                        Debug.Log(current_Allbattle_state);
                    }
                    else if (nextAliveEnemy == -1)
                    {
                        CMplayer.SetActive(true);
                        active_true_allEnemyCM();
                        refresh_allEnemyAIstart_trigger();
                        current_Allbattle_state = Allbattle_state.Player_state;

                    }
                }

            }
            else if (current_Allbattle_state == Allbattle_state.Enemy2_state)
            {
                //Debug.Log("进入Enemy2状态");
                int enemy_index = 2;
                if (whichisalive[enemy_index - 1] == -1)
                {
                    enemy2AIenter = true;
                    enemy2AIroundFinish = true;
                    cameraControlScript.setoff_deadenemy(enemy_index - 1);
                }
                if (enemy2AIenter == false)
                {
                   // Debug.Log("ENemy2进入AIstart=true");
                    enemy_battleTime(enemy_index);
                    enemy2AIenter = true;
                }
                else if (enemy2AIroundFinish == true)
                {
                    int nextAliveEnemy = theNext_aliveEnemy(enemy_index - 1);
                    if (nextAliveEnemy != -1)
                    {
                        cameraControlScript.setoff_deadenemy(enemy_index - 1);
                        setActive_nextenemy(nextAliveEnemy);

                    }
                    else if (nextAliveEnemy == -1)
                    {
                        CMplayer.SetActive(true);
                        active_true_allEnemyCM();
                        refresh_allEnemyAIstart_trigger();
                        current_Allbattle_state = Allbattle_state.Player_state;

                    }

                }
            }
            else if (current_Allbattle_state == Allbattle_state.Enemy3_state)
            {
                //Debug.Log("进入Enemy3状态");
                int enemy_index = 3;
                if (whichisalive[enemy_index - 1] == -1)
                {
                    enemy3AIenter = true;
                    enemy3AIroundFinish = true;
                    cameraControlScript.setoff_deadenemy(enemy_index - 1);
                }
                if (enemy3AIenter == false)
                {
                    // Debug.Log("ENemy3进入AIstart=true");
                    enemy_battleTime(enemy_index);
                    enemy3AIenter = true;
                }
                else if (enemy3AIroundFinish == true)
                {
                    int nextAliveEnemy = theNext_aliveEnemy(enemy_index - 1);
                    if (nextAliveEnemy != -1)
                    {
                        cameraControlScript.setoff_deadenemy(enemy_index - 1);
                        setActive_nextenemy(nextAliveEnemy);
                    }
                    else if (nextAliveEnemy == -1)
                    {
                        CMplayer.SetActive(true);
                        active_true_allEnemyCM();
                        refresh_allEnemyAIstart_trigger();
                        current_Allbattle_state = Allbattle_state.Player_state;

                    }

                }
            }
            else if (current_Allbattle_state == Allbattle_state.Enemy4_state)
            {
                Debug.Log("进入Enemy4状态");
                int enemy_index = 4;
                if (whichisalive[enemy_index - 1] == -1)
                {
                    enemy4AIenter = true;
                    enemy4AIroundFinish = true;
                    cameraControlScript.setoff_deadenemy(enemy_index - 1);
                }
                if (enemy4AIenter == false)
                {
                    Debug.Log("ENemy4进入AIstart=true");
                    enemy_battleTime(enemy_index);

                    enemy4AIenter = true;
                }
                else if (enemy4AIroundFinish == true)
                {
                    int nextAliveEnemy = theNext_aliveEnemy(enemy_index - 1);
                    if (nextAliveEnemy != -1)
                    {
                        cameraControlScript.setoff_deadenemy(enemy_index - 1);
                        setActive_nextenemy(nextAliveEnemy);
                    }
                    else if (nextAliveEnemy == -1)
                    {
                        CMplayer.SetActive(true);
                        active_true_allEnemyCM();////////////////////////////////////////////////////
                        refresh_allEnemyAIstart_trigger();
                        current_Allbattle_state = Allbattle_state.Player_state;

                    }
                }
            }
        }
        else if (Current_Game_State == Game_state.player_Win)
        {
            playerWin.SetActive(true);
          // Time.timeScale = 0f;
        }
        else if (Current_Game_State == Game_state.player_Dead)
        {
            playerDie.SetActive(true);

        }

    }
    
}
