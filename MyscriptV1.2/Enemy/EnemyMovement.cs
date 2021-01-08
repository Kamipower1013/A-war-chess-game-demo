using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Animator EnemyAnim;
    public GameObject player;
    public EnemyAI thisEnemyAIScript;
    public RougeMapCreate RougeMapCreateScript;
    public findTheWayPlayer findTheWayPlayerScript;
    public EnemyChara EnemyCharaScript;
    public battleManager battleManagerScript;
    public int enemyNumber;
    public MapListNode thisCharactor_pos_mapnode;
    public float speed = 1f;
    public int Enemycanwalk_step = 15;
    //Transform 
    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        EnemyAnim = GetComponent<Animator>();
        thisEnemyAIScript = GetComponent<EnemyAI>();
        EnemyCharaScript = GetComponent<EnemyChara>();
        battleManagerScript = GameObject.FindGameObjectWithTag("BattleManager").gameObject.GetComponent<battleManager>();
        findTheWayPlayerScript = GameObject.FindGameObjectWithTag("RougeMap").gameObject.GetComponent<findTheWayPlayer>();
        RougeMapCreateScript = GameObject.FindGameObjectWithTag("RougeMap").gameObject.GetComponent<RougeMapCreate>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public MapType thisEnemyMaptype()
    {
        if (enemyNumber == 0)
        {
            return MapType.enemy1_Pos;
        }
        else if (enemyNumber == 1)
        {
            return MapType.enemy2_Pos;
        }
        else if (enemyNumber == 2)
        {
            return MapType.enemy3_Pos;
        }
        else if (enemyNumber == 3)
        {
            return MapType.enemy4_Pos;
        }

        return MapType.enemy5_Pos;

    }



    public int dontArrive_SamePos(List<MapListNode> Record_EnemyFindPath, int endcount)//递归，错位置
    {
        Debug.Log("进入递归函数" + "这层递归的endcount为" + endcount);
        if ((Record_EnemyFindPath[endcount - 1].path_Element == MapType.enemy1_Pos) || (Record_EnemyFindPath[endcount - 1].path_Element == MapType.enemy2_Pos) || (Record_EnemyFindPath[endcount - 1].path_Element == MapType.enemy3_Pos) || (Record_EnemyFindPath[endcount - 1].path_Element == MapType.enemy4_Pos) || (Record_EnemyFindPath[endcount - 1].path_Element == MapType.enemy5_Pos))
        {
            Debug.Log("进入下一层递归");
            return dontArrive_SamePos(Record_EnemyFindPath, endcount - 1);
        }
        Debug.Log("最终退出时的endCount" + endcount);
        return endcount;
    }

    public IEnumerator ThisEnemyMovetoNewPos()
    {
        Debug.Log("敌人进入协程");
        int endCount = findTheWayPlayerScript.Record_EnemyFindPath.Count;
        Debug.Log("刚开始路径步数长度" + endCount);

        if (endCount >= (Enemycanwalk_step + EnemyCharaScript.buff_PathStep))
        {
            endCount = (Enemycanwalk_step + EnemyCharaScript.buff_PathStep);
        }

        endCount = dontArrive_SamePos(findTheWayPlayerScript.Record_EnemyFindPath, endCount);

        Debug.Log("此敌人最终需要走的步数" + endCount);
        MapType thisEnemyMaptypePos = thisEnemyMaptype();
        //int count = 0;
        for (int i = 1; i < endCount; i++)
        {
            EnemyAnim.SetBool("EnemyIsRunning", true);
            // Vector3 newPathPos = new Vector3(findTheWayPlayerScript.Record_EnemyFindPath[i].x, transform.position.y, findTheWayPlayerScript.Record_EnemyFindPath[i].z);
            // Debug.Log(findTheWayPlayerScript.Record_EnemyFindPath[i].mapPoint_pos);
            transform.LookAt(findTheWayPlayerScript.Record_EnemyFindPath[i].mapPoint_pos);
            //transform.LookAt(newPathPos);

            //Debug.Log( findTheWayPlayerScript. recordFindPlayerPath[i].mapPoint_pos);
            while (Vector3.Distance(transform.position, findTheWayPlayerScript.Record_EnemyFindPath[i].mapPoint_pos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, findTheWayPlayerScript.Record_EnemyFindPath[i].mapPoint_pos, speed * Time.fixedDeltaTime);
                //Debug.Log("xiechen");
                // transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
                yield return null;
            }
            //Debug.Log(count);
            // player.transform.position = recordFindPlayerPath[i].mapPoint_pos;

        }
        EnemyAnim.SetBool("EnemyIsRunning", false);
        transform.position = findTheWayPlayerScript.Record_EnemyFindPath[endCount - 1].mapPoint_pos;
        if (thisEnemyAIScript.IsFindplayer == true)
        {
            transform.LookAt(player.transform);
        }
        findTheWayPlayerScript.Record_EnemyFindPath[endCount - 1].charactor = transform.gameObject;//设定地图中的玩家位置
        thisCharactor_pos_mapnode = findTheWayPlayerScript.Record_EnemyFindPath[endCount - 1];
        //该节点的chara设定
        RougeMapCreateScript.Refresh_Old_EnemyPos(thisEnemyMaptypePos);
        RougeMapCreateScript.passableMap[findTheWayPlayerScript.Record_EnemyFindPath[endCount - 1].z, findTheWayPlayerScript.Record_EnemyFindPath[endCount - 1].x] = thisEnemyMaptypePos;

        //////////////////////////////////////////////////////
        findTheWayPlayerScript.Record_EnemyFindPath.Clear();//此处清理记录路径的队列
        ////////////////////////////////////////////////////////
        ///
        thisEnemyAIScript.thisEnemycurrentAI = enemyAI_state.enemy_roundend;
    }



    public void start_Enemy_move_Coroutine()
    {
        StartCoroutine("ThisEnemyMovetoNewPos");

    }
    public void enemy_deadAnim()
    {
        Debug.Log("进入死亡动画");
        EnemyAnim.SetBool("EnemyDead", true);
    }

    public void EnemyGetHurtAnim()
    {
        Debug.Log("进入敌人受伤状态");
        EnemyAnim.SetTrigger("enemyishurt");
    }

    public void startEnemyattack_coroutine()
    {
        Debug.Log("开始进入攻击携程");
        StartCoroutine("Enemy_attackAnim");
    }

    public IEnumerator Enemy_attackAnim()
    {
        Debug.Log("进入攻击携程");
        transform.LookAt(player.transform);
        EnemyAnim.SetTrigger("EnemyAttack");
        yield return new WaitForSeconds(3.5f);

        thisEnemyAIScript.thisEnemycurrentAI = enemyAI_state.enemy_attack;
    }

    public void start_Enemy_no_pos_wait_coroutine()
    {
        StartCoroutine("Enemy_no_pos_wait");
    }

    public IEnumerator Enemy_no_pos_wait()
    {
        Debug.Log("进入满位等待携程");
        yield return new WaitForSeconds(3.5f);
        thisEnemyAIScript.thisEnemycurrentAI = enemyAI_state.enemy_roundend;
    }

    public bool ThisEnemy_hasplayerAround()
    {
        if (thisCharactor_pos_mapnode.Nodeup != null && thisCharactor_pos_mapnode.Nodeup.path_Element == MapType.player_Pos)
        {
            return true;
        }
        else if (thisCharactor_pos_mapnode.NodeLeft != null && thisCharactor_pos_mapnode.NodeLeft.path_Element == MapType.player_Pos)
        {
            return true;
        }
        else if (thisCharactor_pos_mapnode.NodeDown != null && thisCharactor_pos_mapnode.NodeDown.path_Element == MapType.player_Pos)
        {
            return true;
        }
        else if (thisCharactor_pos_mapnode.NodeRight != null && thisCharactor_pos_mapnode.NodeRight.path_Element == MapType.player_Pos)
        {
            return true;
        }
        return false;
    }

   
}
