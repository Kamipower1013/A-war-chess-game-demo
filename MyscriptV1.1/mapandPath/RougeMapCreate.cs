using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    canWalk,
    wallRock,
    player_Pos,
    player_up,
    player_left,
    player_down,
    player_right,
    player_moveto,
    enemy1_Pos,
    enemy2_Pos,
    enemy3_Pos,
    enemy4_Pos,
    enemy5_Pos,
    treasure_box
}




public class RougeMapCreate : MonoBehaviour
{
    public GameObject wallCube;
    public GameObject pathfloor;
    public GameObject Player;
    public GameObject[] Enemy;
    public float maplength;

    public findTheWayPlayer findTheWayPlayerScript;
    public GameObject[,] wallmap;//用来存墙列表
    public GameObject[,] pathMap;//用来存绿地砖
    public GameObject AllWall_RougeMap;//存储空物体List
    public GameObject Allpath_RougeMap;//存储空物体List
    public GameObject[] wall_rockList;//三种墙类
    List<GameObject> WallList = new List<GameObject>();
    List<GameObject> pathList = new List<GameObject>();
    bool isReflashMap;
    public MapType[,] passableMap;

    private void Awake()
    {

    }
    void Start()
    {   //wall_rockList = new GameObject[3];
        maplength = GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x;
        wallmap = new GameObject[(int)maplength, (int)maplength];
        pathMap = new GameObject[(int)maplength, (int)maplength];
        passableMap = new MapType[(int)maplength, (int)maplength];
        isReflashMap = false;
        createfullWall_Path();
        Iniwall();
        setALL_in_parent();
        RandomWall();
        findTheWayPlayerScript = GetComponent<findTheWayPlayer>();
        //Debug.Log(wallmap.GetLength(0));
        //InitCharact_Pos(Player);//不在这个脚本生成，在findway脚本的start；
    }


    public void createfullWall_Path()
    {
        for (float z = -maplength / 2; z < maplength / 2; z += 1)
        {
            for (float x = -maplength / 2; x < maplength / 2; x += 1)
            {
                if (z == (-maplength / 2) || x == (-maplength / 2) || z == (maplength / 2) - 1 || x == (maplength / 2) - 1)
                {
                    int rocktypeEdge = Random.Range(3, 6);
                    WallList.Add(Instantiate(wall_rockList[rocktypeEdge], new Vector3((transform.position.x + x) + 0.5f, 0.2f, (transform.position.z + z) + 0.5f), new Quaternion(0, 0, 0, 0)));
                    pathList.Add(Instantiate(pathfloor, new Vector3((transform.position.x + x) + 0.5f, 0.75f, (transform.position.z + z) + 0.5f), pathfloor.gameObject.transform.rotation));
                }
                else
                {
                    int rockType = Random.Range(0, 6);
                    WallList.Add(Instantiate(wall_rockList[rockType], new Vector3((transform.position.x + x) + 0.5f, 0.2f, (transform.position.z + z) + 0.5f), new Quaternion(0, 0, 0, 0)));
                    pathList.Add(Instantiate(pathfloor, new Vector3((transform.position.x + x) + 0.5f, 0.75f, (transform.position.z + z) + 0.5f), pathfloor.gameObject.transform.rotation));
                }
            }
        }
    }
    /// <summary>
    /// 关于PLAYER！！！！！！！！！！！！！！！！
    /// </summary>
    public void Refresh_PathColor()
    {
        for (int z = 0; z < pathMap.GetLength(0); z++)
        {
            for (int x = 0; x < pathMap.GetLength(1); x++)
            {
                pathMap[z, x].GetComponent<Renderer>().material.color = Color.green;

            }
        }

    }

    public void Set_pathFloor_false()
    {
        for (int z = 0; z < pathMap.GetLength(0); z++)
        {
            for (int x = 0; x < pathMap.GetLength(1); x++)
            {
                if (pathMap[z, x].activeSelf == true)
                {
                    pathMap[z, x].SetActive(false);
                }

            }
        }
    }

    public void Refresh_Old_playerPos()//用于到达目的地的刷新
    {
        for (int z = 0; z < passableMap.GetLength(0); z++)
        {
            for (int x = 0; x < passableMap.GetLength(1); x++)
            {
                if (passableMap[z, x] == MapType.player_Pos)
                {
                    passableMap[z, x] = MapType.canWalk;
                    findTheWayPlayerScript.GraphNobeArr[z, x].path_Element = MapType.canWalk;
                    findTheWayPlayerScript.GraphNobeArr[z, x].charactor = null;
                    if (findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup != null && findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.path_Element == MapType.player_up)
                    {
                        passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.z, findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.x] = MapType.canWalk;
                        findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.path_Element = MapType.canWalk;
                    }
                    if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.path_Element == MapType.player_left)
                    {
                        passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.x] = MapType.canWalk;
                        findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.path_Element = MapType.canWalk;
                    }
                    if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.path_Element == MapType.player_down)
                    {
                        passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.x] = MapType.canWalk;
                        findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.path_Element = MapType.canWalk;
                    }
                    if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.path_Element == MapType.player_right)
                    {
                        passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.x] = MapType.canWalk;
                        findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.path_Element = MapType.canWalk;
                    }

                    //charactor已经设定了
                }
            }
        }
    }

    public void Refresh_clear_PlayerMoveto_setNewplayerPos()
    {
        for (int z = 0; z < passableMap.GetLength(0); z++)
        {
            for (int x = 0; x < passableMap.GetLength(1); x++)
            {
                if (passableMap[z, x] == MapType.player_moveto)
                {
                    passableMap[z, x] = MapType.player_Pos;
                    findTheWayPlayerScript.GraphNobeArr[z, x].path_Element = MapType.player_Pos;
                    findTheWayPlayerScript.GraphNobeArr[z, x].charactor = Player;
                    if (findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup != null && findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.path_Element == MapType.canWalk)
                    {
                        passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.z, findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.x] = MapType.player_up;
                        findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.path_Element = MapType.player_up;
                    }
                    if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.path_Element == MapType.canWalk)
                    {
                        passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.x] = MapType.player_left;
                        findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.path_Element = MapType.player_left;
                    }
                    if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.path_Element == MapType.canWalk)
                    {
                        passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.x] = MapType.player_down;
                        findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.path_Element = MapType.player_down;
                    }
                    if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.path_Element == MapType.canWalk)
                    {
                        passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.x] = MapType.player_right;
                        findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.path_Element = MapType.player_right;
                    }

                }
            }
        }

    }
    /// <summary>
    /// /
    /// </summary>
    /// 

    public void Refresh_Old_EnemyPos(MapType enemy_index_pos)
    {
        for (int z = 0; z < passableMap.GetLength(0); z++)
        {
            for (int x = 0; x < passableMap.GetLength(1); x++)
            {
                if (passableMap[z, x] == enemy_index_pos)
                {
                    passableMap[z, x] = MapType.canWalk;
                    findTheWayPlayerScript.GraphNobeArr[z, x].path_Element = MapType.canWalk;
                    findTheWayPlayerScript.GraphNobeArr[z, x].charactor = null;
                    //charactor已经设定了
                }
            }
        }

    }


    public void setALL_in_parent()
    {
        for (int i = 0; i < WallList.Count; i++)
        {
            WallList[i].transform.parent = AllWall_RougeMap.transform;
            pathList[i].transform.parent = Allpath_RougeMap.transform;

        }
    }
    public void Iniwall()
    {
        int count = 0;
        for (int z = 0; z < maplength; z++)
        {
            for (int x = 0; x < maplength; x++)
            {

                wallmap[z, x] = WallList[count];
                pathMap[z, x] = pathList[count];
                wallmap[z, x].SetActive(false);
                passableMap[z, x] = MapType.canWalk;
                pathMap[z, x].SetActive(false);
                count++;
            }
        }
    }


    public void Flood_full_count(int z, int x)
    {


    }



    public void RandomWall()
    {

        for (int z = 0; z < maplength; z++)
        {
            int x_rowcount = 0;
            for (int x = 0; x < maplength; x++)
            {

                if (x == 0 || z == 0 || x == maplength - 1 || z == maplength - 1)
                {
                    //int rockType = Random.Range(0, 3);
                    passableMap[z, x] = MapType.wallRock;
                    //wallmap[z, x] = wall_rockList[rockType];
                    wallmap[z, x].SetActive(true);
                    // Debug.Log(rockType);
                }
                else if (x_rowcount < 9)
                {
                    int isWall = Random.Range(0, 9);
                    if (isWall == 0)
                    {
                        // int rockType = Random.Range(0, 3);
                        //wallmap[z, x] = Instantiate(wall_rockList[rockType],wallmap[z,x].transform.position,wallmap[z,x].transform.rotation);
                        passableMap[z, x] = MapType.wallRock;
                        wallmap[z, x].SetActive(true);
                        x_rowcount++;
                    }
                }


            }
        }

    }

    public void InitCharact_Pos(GameObject Charact)
    {
        bool isSet = false;
        for (int z = 3; z < passableMap.GetLength(0); z++)
        {
            for (int x = 3; x < passableMap.GetLength(1); x++)
            {
                if (passableMap[z, x] == MapType.canWalk)
                {
                    if (Charact.gameObject.tag == "Player")
                    {
                        Debug.Log("in");
                        passableMap[z, x] = MapType.player_Pos;
                        findTheWayPlayerScript.GraphNobeArr[z, x].path_Element = MapType.player_Pos;
                        Charact.transform.position = findTheWayPlayerScript.GraphNobeArr[z, x].mapPoint_pos;
                        findTheWayPlayerScript.GraphNobeArr[z, x].charactor = Charact;
                        if (findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup != null && findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.path_Element == MapType.canWalk)
                        {
                            passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.z, findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.x] = MapType.player_up;
                            findTheWayPlayerScript.GraphNobeArr[z, x].Nodeup.path_Element = MapType.player_up;
                        }
                        if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.path_Element == MapType.canWalk)
                        {
                            passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.x] = MapType.player_left;
                            findTheWayPlayerScript.GraphNobeArr[z, x].NodeLeft.path_Element = MapType.player_left;
                        }
                        if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.path_Element == MapType.canWalk)
                        {
                            passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.x] = MapType.player_down;
                            findTheWayPlayerScript.GraphNobeArr[z, x].NodeDown.path_Element = MapType.player_down;
                        }
                        if (findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight != null && findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.path_Element == MapType.canWalk)
                        {
                            passableMap[findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.z, findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.x] = MapType.player_right;
                            findTheWayPlayerScript.GraphNobeArr[z, x].NodeRight.path_Element = MapType.player_right;
                        }

                        isSet = true;
                        break;
                    }

                }
            }
            if (isSet == true)
            {
                break;
            }
        }

    }


    public void InitCharact_Enemy_Pos(GameObject[] enemyList)
    {
        bool isset = false;
        for (int i = 0; i < enemyList.Length; i++)
        {
            isset = false;
            for (int z = 20; z < passableMap.GetLength(0) - 20; z++)
            {
                z = z + i * 3;
                for (int x = 20; x < passableMap.GetLength(1) - 20; x++)
                {
                    x = x + i * 3;
                    if (passableMap[z, x] == MapType.canWalk)
                    {
                        if (enemyList[i].tag == "Enemy")
                        {
                            passableMap[z, x] = MapType.enemy1_Pos + i;//
                            findTheWayPlayerScript.GraphNobeArr[z, x].path_Element = passableMap[z, x];
                            enemyList[i].transform.position = findTheWayPlayerScript.GraphNobeArr[z, x].mapPoint_pos;
                            enemyList[i].gameObject.GetComponent<EnemyMovement>().thisCharactor_pos_mapnode = findTheWayPlayerScript.GraphNobeArr[z, x];
                            findTheWayPlayerScript.GraphNobeArr[z, x].charactor = enemyList[i];
                            isset = true;
                            break;
                        }

                    }
                }
                if (isset == true)
                {
                    break;
                }
            }
        }
    }



    public void testPassableMap()
    {
        for (int z = 0; z < passableMap.GetLength(0); z++)
        {
            for (int x = 0; x < passableMap.GetLength(1); x++)
            {
                if (passableMap[z, x] == MapType.enemy1_Pos)
                {
                    Debug.Log("enemy1位置" + findTheWayPlayerScript.GraphNobeArr[z, x].mapPoint_pos);
                }
                else if (passableMap[z, x] == MapType.enemy2_Pos)
                {
                    Debug.Log("enemy2位置" + findTheWayPlayerScript.GraphNobeArr[z, x].mapPoint_pos);
                }
                else if (passableMap[z, x] == MapType.enemy3_Pos)
                {
                    Debug.Log("enemy3位置" + findTheWayPlayerScript.GraphNobeArr[z, x].mapPoint_pos);
                }
                else if (passableMap[z, x] == MapType.enemy4_Pos)
                {
                    Debug.Log("enemy4位置" + findTheWayPlayerScript.GraphNobeArr[z, x].mapPoint_pos);
                }
            }
        }
    }
    private void Update()
    {

    }
}
