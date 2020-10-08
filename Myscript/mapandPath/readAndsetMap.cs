using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readAndsetMap : MonoBehaviour
{   
    public Transform playerPos;
    Ray mousePoint;
    bool isSetStart;
    bool isSetEnd;
    bool isSetStart_attack;
    bool isSetAttack_end;
    int playerLayer;
    int floorLayer;
    int enemylayer;
    RougeMapCreate RougeMapCreateSCript;
    public battleManager battleManagerScript;
    public GameObject[] enemylist;
    
    findTheWayPlayer findTheWayPlayerscript;
    /// 

    //TextMesh pathnumber;
    // Start is called before the first frame update
    void Start()
    {
        isSetStart = false;
        isSetEnd = false;
        isSetStart_attack = false;
        isSetAttack_end = false;
        playerLayer = LayerMask.GetMask("Player");
        floorLayer = LayerMask.GetMask("floorPath");
        enemylayer = LayerMask.GetMask("Enemy");
        RougeMapCreateSCript= GetComponent<RougeMapCreate>();
        findTheWayPlayerscript = GetComponent<findTheWayPlayer>();

      
      
    }
    public void Click_player(Battle_State current_State)
    {
        if (current_State==Battle_State.player_path_choice&&isSetStart==false)
        {
            Debug.Log("进入Click函数");
            mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mousePoint, out mouseHit, 200f, playerLayer))
            {   
                Debug.Log("进入射线层");
                Debug.Log(playerLayer);
                //Vector3 create_Pos = mouseHit.rigidbody.transform.position;
                battleManagerScript.click_Player_startCho_Path = true;
                for (int i = 0; i <RougeMapCreateSCript.wallmap.GetLength(0); i++)
                {
                    for (int j = 0; j < RougeMapCreateSCript.wallmap.GetLength(1); j++)
                    {
                       
                        if (RougeMapCreateSCript.passableMap[i, j]==MapType.player_Pos)
                        {
                            Debug.Log("启动BFS");
                            RougeMapCreateSCript.pathMap[i, j].GetComponent<Renderer>().material.color = Color.red;
                            //RougeMapCreateSCript.passableMap[i, j] = MapType.player_Pos;
                            findTheWayPlayerscript.StartBFS_ACtivePath();
                            isSetStart = true;
                           
                        }
                    }
                }
            }

        }
      
    }

    public bool Canwalk_click_playerAround(int z,int x)
    {
       if( RougeMapCreateSCript.passableMap[z, x] == MapType.player_up)
        {
            return true;
        }
       else if(RougeMapCreateSCript.passableMap[z, x] == MapType.player_left)
        {
            return true;
        }
       else if (RougeMapCreateSCript.passableMap[z, x] == MapType.player_down)
        {
            return true;
        }
       else if (RougeMapCreateSCript.passableMap[z, x] == MapType.player_right)
        {
            return true;
        }

        return false;
    }


    public void get_endPathPoint(Battle_State current_state)
    {
        Debug.Log("进入getendpoint函数");
        if (isSetEnd == false && isSetStart == true &&current_state==Battle_State.player_path_choice)
            mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mousePoint, out mouseHit, 200f,floorLayer,QueryTriggerInteraction.Collide))
            {
               Debug.Log("点到地板了");
               Vector3 Move_to_pos = new Vector3(mouseHit.transform.position.x,0.2f, mouseHit.transform.position.z);

                for (int z = 0; z < RougeMapCreateSCript.wallmap.GetLength(0); z++)
                {
                    for (int x = 0; x < RougeMapCreateSCript.wallmap.GetLength(1); x++)
                    {
                        //Debug.Log(i + " " + j);
                        if ((Canwalk_click_playerAround(z,x)==true||RougeMapCreateSCript.passableMap[z, x] ==MapType.canWalk)&& Mathf.Abs(RougeMapCreateSCript.wallmap[z, x].transform.position.x-Move_to_pos.x) < 0.5f && Mathf.Abs(RougeMapCreateSCript.wallmap[z, x].gameObject.transform.position.z -Move_to_pos.z) < 0.5f)
                        {

                            //RougeMapCreateSCript.wallmap[i, j].SetActive(true);
                            //RougeMapCreateSCript.wallmap[i, j].GetComponent<Renderer>().material.color = Color.black;
                            RougeMapCreateSCript.pathMap[z, x].GetComponent<Renderer>().material.color = Color.blue;
                            
                            RougeMapCreateSCript.passableMap[z, x] =MapType.player_moveto;
                            battleManagerScript.hadCLickPlayer_cho_pathend = true;
                            findTheWayPlayerscript.StartBFS_Find_movePath();
                            //Debug.Log(RougeMapCreateSCript.passableMap[i, j]);
                            isSetEnd = true;
                            setStartboolReflesh();
                        }
                    }
                }
            }

     
    }

    public void Click_player_attack(Battle_State current_State)
    {
        if (current_State == Battle_State.player_attack&&isSetStart_attack==false)
        {
            Debug.Log("进入Click_attack函数");
            Ray mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mousePoint, out mouseHit, 200f, playerLayer))
            {
                Debug.Log("进入射线层");

                //Vector3 create_Pos = mouseHit.rigidbody.transform.position;
                // battleManagerScript.click_Player_startCho_Path = true;
                battleManagerScript.Isclick_player_start_attack = true;
                for (int i = 0; i < RougeMapCreateSCript.wallmap.GetLength(0); i++)
                {
                    for (int j = 0; j < RougeMapCreateSCript.wallmap.GetLength(1); j++)
                    {

                        if (RougeMapCreateSCript.passableMap[i, j] == MapType.player_Pos)
                        {
                            Debug.Log("启动攻击范围");
                            findTheWayPlayerscript.player_attack_AreaActive();
                            //RougeMapCreateSCript.pathMap[i, j].GetComponent<Renderer>().material.color = Color.red;
                            //RougeMapCreateSCript.passableMap[i, j] = MapType.player_Pos;
                              isSetStart_attack = true;
                           // battleManagerScript.Isclick_player = true;
                        }
                    }
                }
            }

        }

    }

    public int getwhichone_attack(Battle_State current_state)
    {
        Debug.Log("getwhichone_attack");
        if (isSetAttack_end == false && isSetStart_attack == true && current_state == Battle_State.player_attack)
        {
            mousePoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mousePoint, out mouseHit, 200f, enemylayer, QueryTriggerInteraction.Collide))
            {
                Debug.Log("点到地板了");
                GameObject attackobject = mouseHit.collider.gameObject;
                Vector3 hitThatPos = new Vector3(mouseHit.transform.position.x, 0.3f, mouseHit.transform.position.z);
                if (attackobject.GetComponent<EnemyMovement>().ThisEnemy_hasplayerAround() == true)
                {
                    int enemynumber = attackobject.GetComponent<EnemyAI>().enemyNumber;
                    battleManagerScript.hadClick_player_cho_attack = true;
                    playerPos.LookAt(attackobject.transform.position);
                    RougeMapCreateSCript.Refresh_PathColor();
                    RougeMapCreateSCript.Set_pathFloor_false();
                    isSetAttack_end = true;
                    setStartAttackboolReflesh();
                    return enemynumber;
                }

            }
        }

        return -1;
    }
    public void setStartAttackboolReflesh()
    {
        isSetAttack_end = false;
        isSetStart_attack = false;
    }
    public void setStartboolReflesh()
    {
        isSetStart = false;
        isSetEnd = false;
    }

    }
    // Update is called once per frame
       // Start is called before the first frame update
   
