using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum enemyAI_state
{   enemy_roundStart_Stay,
    start_patrol,
    //同级
    no_posand_wait,
    chase_player_Move,
    enemy_start_attack,
    enemy_attack,
    //同级
    enemy_roundend,
    enemy_dead
}
//
public class EnemyAI : MonoBehaviour
{
    public int enemyNumber;
    public enemyAI_state thisEnemycurrentAI;
    public MapType thisEnemyPos;
    public  bool startAI;
    public int searchArea;
    public RougeMapCreate RougeMapCreateSCript;
    public findTheWayPlayer findTheWayPlayerScript;
    public EnemyMovement EnemyMovementScript;
    public EnemyChara EnemyCharaScript;
    public battleManager battleManagerScript;
    public bool thisStartMove;
    public bool thisIsArrived;
    public bool thisStartAttack;
    public bool thisHadAttack;
    public bool thisStartPatrol;
    public bool thisstartWait;
    public GameObject player;

    public GameObject wander;
    public GameObject findplayer;

    // Start is called before the first frame update
    void Start()
    {
        searchArea = 35;
        thisEnemycurrentAI = enemyAI_state.enemy_roundStart_Stay;
        EnemyMovementScript = GetComponent<EnemyMovement>();
        EnemyCharaScript = GetComponent<EnemyChara>();
        battleManagerScript = GameObject.FindGameObjectWithTag("BattleManager").gameObject.GetComponent<battleManager>();
        findTheWayPlayerScript = GameObject.FindGameObjectWithTag("RougeMap").gameObject.GetComponent<findTheWayPlayer>();
        RougeMapCreateSCript = GameObject.FindGameObjectWithTag("RougeMap").gameObject.GetComponent<RougeMapCreate>();
        player = GameObject.FindGameObjectWithTag("Player");
        startAI = false;
        thisStartMove = false;
        thisIsArrived = false;
        thisStartAttack = false;
        thisHadAttack = false;
        thisStartPatrol = false;
        thisstartWait = false;
    }



    public int get_thisEnemy_PosAndstartsearch(int enemy_index)//alltheEnemy里赋值了enemy——index,此函数被下面的update函数调用了
    {
        int pathCount = 0;
        if (thisEnemycurrentAI == enemyAI_state.enemy_roundStart_Stay)
        {
            if (enemyNumber == 0)
            {
                thisEnemyPos = MapType.enemy1_Pos;
            }
            else if (enemyNumber == 1)
            {
                thisEnemyPos = MapType.enemy2_Pos;
            }
            else if (enemyNumber == 2)
            {
                thisEnemyPos = MapType.enemy3_Pos;
            }
            else if (enemyNumber == 3)
            {
                thisEnemyPos = MapType.enemy4_Pos;
            }
            else if (enemyNumber == 4)
            {
                thisEnemyPos = MapType.enemy5_Pos;
            }
            //Vector3 create_Pos = mouseHit.rigidbody.transform.position;

            //Debug.Log("启动BFS");

            //RougeMapCreateSCript.passableMap[i, j] = MapType.player_Pos;
            pathCount = findTheWayPlayerScript.enemy_Start_BFS_searchPlayer(thisEnemyPos);


        }
        return pathCount;

    }

    public void HPlessRunning()
    {
        if (EnemyCharaScript.HP < 50)
        {

        }

    }

    public void Start_wander()
    {

    }

    public void ThisenemyFinishRound(int thisenemyNumber)
    {
        if (thisenemyNumber == 0)
        {
            Debug.Log("roundFinsh111=true");
            battleManagerScript.enemy1AIroundFinish = true;
        }
        else if (thisenemyNumber == 1)
        {
            battleManagerScript.enemy2AIroundFinish = true;
        }
        else if (thisenemyNumber == 2)
        {
            battleManagerScript.enemy3AIroundFinish = true;
        }
        else if (thisenemyNumber == 3)
        {
            battleManagerScript.enemy4AIroundFinish = true;
        }
        else if (thisenemyNumber == 4)
        {
            battleManagerScript.enemy5AIroundFinish = true;
        }
    }
    //public bool Thisisnear_player_pos()
    //{
    //    if (enemyNumber == 0)
    //    {
    //        thisEnemyPos = MapType.enemy1_Pos;
    //    }
    //    else if (enemyNumber == 1)
    //    {
    //        thisEnemyPos = MapType.enemy2_Pos;
    //    }
    //    else if (enemyNumber == 2)
    //    {
    //        thisEnemyPos = MapType.enemy3_Pos;
    //    }
    //    else if (enemyNumber == 3)
    //    {
    //        thisEnemyPos = MapType.enemy4_Pos;
    //    }

    //}
    // Update is called once per frame


    public IEnumerator waitforSecond()
    {
        yield return new WaitForSeconds(3f);

    }

    void Update()
    {   
        if (startAI == true)
        {
            //Debug.Log(thisEnemycurrentAI + "AI状态");
            if (thisEnemycurrentAI == enemyAI_state.enemy_roundStart_Stay)
            {
                findplayer.SetActive(false);
                wander.SetActive(false);
                int pathcount = get_thisEnemy_PosAndstartsearch(enemyNumber);
                Debug.Log("搜索范围传出来了吗"+pathcount);
              
                
                if (pathcount == 1)
                {
                    findplayer.SetActive(true);
                    wander.SetActive(false);
                    thisEnemycurrentAI = enemyAI_state.enemy_start_attack;
                }
                else if (pathcount <= searchArea&&pathcount>1)//没到此距离
                {
                    findplayer.SetActive(true);
                    wander.SetActive(false);
                    Debug.Log("小于搜索范围，进入寻路");
                    int havePostowalk = findTheWayPlayerScript.player_aroundCanwalk();
                    Debug.Log("玩家周围几号有空位" + havePostowalk);
                    if (havePostowalk == -1)
                    {
                        thisEnemycurrentAI = enemyAI_state.no_posand_wait;
                    }
                    else if (havePostowalk != -1)
                    {
                        if (havePostowalk == 1)
                        {
                            Debug.Log("进入up位");
                            findTheWayPlayerScript.Enemy_startFindandRecordWay(thisEnemyPos, MapType.player_up);

                            thisEnemycurrentAI = enemyAI_state.chase_player_Move;
                            Debug.Log(thisEnemycurrentAI + "AI状态");
                        }
                        else if (havePostowalk == 2)
                        {
                            Debug.Log("进入左位");
                            findTheWayPlayerScript.Enemy_startFindandRecordWay(thisEnemyPos, MapType.player_left);
                            thisEnemycurrentAI = enemyAI_state.chase_player_Move;
                        }
                        else if (havePostowalk == 3)
                        {
                            Debug.Log("进入下位");
                            findTheWayPlayerScript.Enemy_startFindandRecordWay(thisEnemyPos, MapType.player_down);
                            thisEnemycurrentAI = enemyAI_state.chase_player_Move;

                        }
                        else if (havePostowalk == 4)
                        {
                            Debug.Log("进入右位");
                            findTheWayPlayerScript.Enemy_startFindandRecordWay(thisEnemyPos, MapType.player_right);
                            thisEnemycurrentAI = enemyAI_state.chase_player_Move;
                        }
                    }
                }
                else if (pathcount > searchArea)
                {
                   Debug.Log("此时pathcount" + pathcount + "此时的searcharea" + searchArea);
                    thisEnemycurrentAI = enemyAI_state.start_patrol;
                }
            }
            else if(thisEnemycurrentAI == enemyAI_state.no_posand_wait&&thisstartWait==false)
            {
                thisstartWait = true;
                EnemyMovementScript.start_Enemy_no_pos_wait_coroutine();
               
            }
            else if (thisEnemycurrentAI == enemyAI_state.start_patrol&&thisStartPatrol==false)
            {   thisStartPatrol = true;
                findplayer.SetActive(false);
                wander.SetActive(true);
                findTheWayPlayerScript.wanderPathFind(thisEnemyPos);

                thisEnemycurrentAI = enemyAI_state.chase_player_Move;
            }
            else if (thisEnemycurrentAI == enemyAI_state.chase_player_Move && thisStartMove == false)
            {
                thisStartMove = true;
                EnemyMovementScript.start_Enemy_move_Coroutine();
               
               
            }
            else if (thisEnemycurrentAI == enemyAI_state.enemy_start_attack&&thisStartAttack==false)
            {
               // Debug.Log("进入eneny startAttack状态");
                thisStartAttack = true;
                EnemyMovementScript.startEnemyattack_coroutine();
             
                EnemyCharaScript.Enemy_attack(player.GetComponent<PlayerCharactor>());
                
            }
            else if (thisEnemycurrentAI == enemyAI_state.enemy_attack&&thisHadAttack==false)
            {
                thisHadAttack = true;
                thisEnemycurrentAI = enemyAI_state.enemy_roundend;

            }
            else if (thisEnemycurrentAI == enemyAI_state.enemy_roundend)
            {   startAI = false;
                thisStartMove = false;
                thisStartAttack = false;
                thisHadAttack = false;
                thisStartPatrol = false;
                thisstartWait = false;
                Debug.Log(thisEnemycurrentAI);
                thisEnemycurrentAI = enemyAI_state.enemy_roundStart_Stay;
                ThisenemyFinishRound(enemyNumber);
            }
        }
    }
}
