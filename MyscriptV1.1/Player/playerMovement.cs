using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerMovement : MonoBehaviour
{
    Animator PlayerAnim;
   // public PlayerCharactor PlayerCharactorScript;
    public RougeMapCreate RougeMapCreateScript;
    public findTheWayPlayer findTheWayPlayerScript;
    public battleManager battleManagerScript;
    public ParticleSystem attackeffect1;
    public ParticleSystem attackeffect2;
    public ParticleSystem defencering1;
    public ParticleSystem defencering2;
    // public GameObject roundChoicePanel;
    public MapListNode my_pos;
    public float speed = 1f;//Transform 
    public float runSpeed = 2f;
    // Start is called before the first frame update


    void Start()
    {
        PlayerAnim = GetComponent<Animator>();
        battleManagerScript = GameObject.FindGameObjectWithTag("BattleManager").gameObject.GetComponent<battleManager>();
        findTheWayPlayerScript = GameObject.FindGameObjectWithTag("RougeMap").gameObject.GetComponent<findTheWayPlayer>();
        RougeMapCreateScript = GameObject.FindGameObjectWithTag("RougeMap").gameObject.GetComponent<RougeMapCreate>();
    }
   
    public void normalWalk()
    {

    }


    public IEnumerator MovetoNewPos()
    {         int endCount = findTheWayPlayerScript.recordFindPlayerPath.Count;
            //int count = 0;
            for (int i = 1; i <findTheWayPlayerScript.recordFindPlayerPath.Count; i++)
            {   
                PlayerAnim.SetBool("iswalking",true);
                //Vector3 newPathPos = new Vector3(findTheWayPlayerScript.recordFindPlayerPath[i].x,transform.position.y,findTheWayPlayerScript.recordFindPlayerPath[i].z);
                //Debug.Log(recordFindPlayerPath[i].mapPoint_pos);
                transform.LookAt(findTheWayPlayerScript.recordFindPlayerPath[i].mapPoint_pos);
                //transform.LookAt(newPathPos);
               // Debug.Log(transform.position);
                //Debug.Log( findTheWayPlayerScript. recordFindPlayerPath[i].mapPoint_pos);
                while (Mathf.Abs(transform.position.x - findTheWayPlayerScript. recordFindPlayerPath[i].mapPoint_pos.x) > 0.1f || Mathf.Abs(transform.position.z -findTheWayPlayerScript.recordFindPlayerPath[i].mapPoint_pos.z) > 0.1f)
                {
                    
                    //Debug.Log("xiechen");
                    transform.Translate(transform.forward * Time.fixedDeltaTime * speed*0.8f, Space.World);
                    yield return new WaitForSeconds(0);
                 }
             //Debug.Log(count);
            // player.transform.position = recordFindPlayerPath[i].mapPoint_pos;
            
          }
        PlayerAnim.SetBool("iswalking", false);
        transform.position = findTheWayPlayerScript.recordFindPlayerPath[endCount - 1].mapPoint_pos;
        findTheWayPlayerScript.recordFindPlayerPath[endCount - 1].charactor = transform.gameObject;//设定地图中的玩家位置
        my_pos = findTheWayPlayerScript.recordFindPlayerPath[endCount - 1];//该节点的chara设定
        RougeMapCreateScript.Refresh_Old_playerPos();
       // RougeMapCreateScript.passableMap[findTheWayPlayerScript.recordFindPlayerPath[endCount - 1].z, findTheWayPlayerScript.recordFindPlayerPath[endCount - 1].x] = MapType.player_Pos;
        RougeMapCreateScript.Refresh_PathColor();
        RougeMapCreateScript.Refresh_clear_PlayerMoveto_setNewplayerPos();
        RougeMapCreateScript.Set_pathFloor_false();
        //////////////////////////////////////////////////////
        findTheWayPlayerScript.recordFindPlayerPath.Clear();//此处清理记录路径的队列
        ////////////////////////////////////////////////////////
    }
    public void start_move_Coroutine()
    {
        StartCoroutine("MovetoNewPos");
    }

    public void AttackAnim()
    {
        PlayerAnim.SetTrigger("isAttack");
        attackeffect1.Play();
        attackeffect2.Play();
    }

    public void InvokeAnim()
    {
        PlayerAnim.SetTrigger("ishurt");
    }

    public void hurtAnim()
    {
        Invoke("InvokeAnim", 1f);
    }

    public void deadAnim()
    {
        PlayerAnim.SetBool("isDead",true);
    }

    public void DefenceAnim()
    {
        PlayerAnim.SetTrigger("isdefence");
        defencering1.Play();
        defencering2.Play();
    }
  
}
