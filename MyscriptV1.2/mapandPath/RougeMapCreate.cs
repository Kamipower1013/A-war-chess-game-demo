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
    public GameObject treasureBox;
    public GameObject[] Enemy;
    public GameObject gateperfab;
    public float maplength;
    public int TreasureBoxNum;
    public int GateNum;

    public findTheWayPlayer findTheWayPlayerScript;
    public GameObject[,] wallmap;//用来存墙列表
    public GameObject[,] pathMap;//用来存绿地砖
    public GameObject AllWall_RougeMap;//存储空物体List
    public GameObject Allpath_RougeMap;//存储空物体List
    public GameObject AllTreasureBox;
    public GameObject AllGate;
    public GameObject[] wall_rockList;//三种墙类

    public List<GameObject> WallList = new List<GameObject>();
    public List<GameObject> pathList = new List<GameObject>();
    public List<GameObject> TreasureBoxList = new List<GameObject>();
    public List<GameObject> GateList = new List<GameObject>();

    public List<Vector3> MapPointList = new List<Vector3>();


    //public bool isReflashMap;
     
    public MapType[,] passableMap;

    public List<Material> Treasure_Box_Material_List;

   


    public void Set_all_Treasurebox_material(List<GameObject> materialslist)
    {
        Debug.Log("几个宝箱"+ TreasureBoxList.Count);
        for(int i = 0; i < TreasureBoxList.Count; i++)
        {
            materialslist[i].GetComponent<treasureBox>().thisMat = Treasure_Box_Material_List[i];
            materialslist[i].GetComponent<treasureBox>().Material_Set_All();
        }

    }


    void Start()
    {   

        //wall_rockList = new GameObject[3];
        Init_Map();
        //Debug.Log(wallmap.GetLength(0));
        //InitCharact_Pos(Player);//不在这个脚本生成，在findway脚本的start；

    }

    public void Init_Map()
    {   
        maplength = GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x;
        wallmap = new GameObject[(int)maplength, (int)maplength];
        pathMap = new GameObject[(int)maplength, (int)maplength];
        passableMap = new MapType[(int)maplength, (int)maplength];
        // isReflashMap = false;

        CreatefullWall_Path();
        Iniwall();
        SetALL_in_parent();
        RandomWall();
        //Init_TreasureBox();
        //SetTreasure_in_parent();
        findTheWayPlayerScript = GetComponent<findTheWayPlayer>();
        //Set_all_Treasurebox_material(TreasureBoxList);
    }

    public void Mapscript_About_TreasureBox()
    {
        Init_TreasureBox();
        SetTreasure_in_parent();
        Set_all_Treasurebox_material(TreasureBoxList);
    }
    
    public void CreatefullWall_Path()
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

    public void Init_GateNode_Left_Down_andRightUP(GameObject Gate1,GameObject Gate2)
    {
        
        bool IsfindfirtGate = false;
        bool isfindsecondGate = false;
        for (int z = 5; z < passableMap.GetLength(0) - 5; z++)
        {
            for (int x = 5; x < passableMap.GetLength(1) - 5; x++)
            {
                if (passableMap[z, x] == MapType.canWalk && Can_SetGate(z, x) == true)
                {
                    findTheWayPlayerScript.GraphNobeArr[z, x].IsGatePoint = true;
                    findTheWayPlayerScript.GraphNobeArr[z, x].Gate = Gate1;
                    Gate1.GetComponent<Gate>().GatePoint = findTheWayPlayerScript.GraphNobeArr[z, x];
                    IsfindfirtGate = true;
                    Gate1.transform.position = Gate1.GetComponent<Gate>().GatePoint.mapPoint_pos + new Vector3(0f, 1f, 0f);
                    Debug.Log("设定第一个门"+ Gate1.GetComponent<Gate>().GatePoint);
                    break;
                }
            }
            if (IsfindfirtGate == true)
            {
                break;
            }
        }

        if (IsfindfirtGate == true && isfindsecondGate == false)
        {
            for (int z = passableMap.GetLength(0) - 5; z >5; z--)
            {
                for (int x = passableMap.GetLength(1) - 5; x > 5; x--)
                {
                    if (passableMap[z, x] == MapType.canWalk && Can_SetGate(z, x) == true)
                    {
                        findTheWayPlayerScript.GraphNobeArr[z, x].IsGatePoint = true;
                        findTheWayPlayerScript.GraphNobeArr[z, x].Gate = Gate2;
                        Gate2.GetComponent<Gate>().GatePoint = findTheWayPlayerScript.GraphNobeArr[z, x];
                        isfindsecondGate = true;
                        Gate2.transform.position = Gate2.GetComponent<Gate>().GatePoint.mapPoint_pos+new Vector3(0f,1f,0f);
                        Debug.Log("第二个门" + Gate2.GetComponent<Gate>().GatePoint);
                        break;
                    }
                }
                if (isfindsecondGate == true)
                {
                    break;
                }
            }

        }
        if(Gate1.GetComponent<Gate>().GatePoint!=null&& Gate2.GetComponent<Gate>().GatePoint!= null)
        {
            Debug.Log("链接两个门");
            Gate1.GetComponent<Gate>().GatePoint.theContectGateNode = Gate2.GetComponent<Gate>().GatePoint;
            Gate2.GetComponent<Gate>().GatePoint.theContectGateNode = Gate1.GetComponent<Gate>().GatePoint;
        }
    }



    public void Init_Two_contect_Gate()
    {
        for (int i = 0; i < GateNum; i++)
        {
            GateList.Add(Instantiate(gateperfab));
        }
        for(int i = 0; i < GateList.Count; i = i + 2)
        {
            Init_GateNode_Left_Down_andRightUP(GateList[i], GateList[i + 1]);
        }
    }



    public void IntoGate_Refresh_Player_Postion(MapListNode contectNode_nearPOS)
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

        for (int z = 0; z < passableMap.GetLength(0); z++)
        {
            for (int x = 0; x < passableMap.GetLength(1); x++)
            {
                if (passableMap[z, x] == MapType.player_moveto)
                {
                    passableMap[z, x] = MapType.canWalk;
                    findTheWayPlayerScript.GraphNobeArr[z, x].path_Element =MapType.canWalk;
                    findTheWayPlayerScript.GraphNobeArr[z, x].charactor =null;
                    
                }
            }
        }

        contectNode_nearPOS.charactor = Player;
        contectNode_nearPOS.path_Element = MapType.player_Pos;
        passableMap[contectNode_nearPOS.z, contectNode_nearPOS.x] = MapType.player_Pos;
        if (contectNode_nearPOS.Nodeup != null && contectNode_nearPOS.Nodeup.path_Element == MapType.canWalk)
        {
            passableMap[contectNode_nearPOS.Nodeup.z, contectNode_nearPOS.Nodeup.x] = MapType.player_up;
            contectNode_nearPOS.Nodeup.path_Element = MapType.player_up;
        }
        if (contectNode_nearPOS.NodeLeft != null && contectNode_nearPOS.NodeLeft.path_Element == MapType.canWalk)
        {
            passableMap[contectNode_nearPOS.NodeLeft.z, contectNode_nearPOS.NodeLeft.x] = MapType.player_left;
            contectNode_nearPOS.NodeLeft.path_Element = MapType.player_left;
        }
        if (contectNode_nearPOS.NodeDown != null && contectNode_nearPOS.NodeDown.path_Element == MapType.canWalk)
        {
            passableMap[contectNode_nearPOS.NodeDown.z, contectNode_nearPOS.NodeDown.x] = MapType.player_down;
            contectNode_nearPOS.NodeDown.path_Element = MapType.player_down;
        }
        if (contectNode_nearPOS.NodeRight != null && contectNode_nearPOS.path_Element == MapType.canWalk)
        {
            passableMap[contectNode_nearPOS.NodeRight.z, contectNode_nearPOS.NodeRight.x] = MapType.player_right;
            contectNode_nearPOS.NodeRight.path_Element = MapType.player_right;
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


    public void SetALL_in_parent()
    {
        for (int i = 0; i < WallList.Count; i++)
        {
            WallList[i].transform.parent = AllWall_RougeMap.transform;
            pathList[i].transform.parent = Allpath_RougeMap.transform;

        }

    }
    public void SetTreasure_in_parent()
    {
        for (int i = 0; i < TreasureBoxList.Count; i++)
        {
            TreasureBoxList[i].transform.parent = AllTreasureBox.transform;

        }

    }

    public void SetGate_in_parent()
    {
        for(int i = 0; i < GateList.Count; i++)
        {
            GateList[i].transform.parent = AllGate.transform;
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

    public bool Can_SetGate(int z,int x)
    {
        int count = 0;
        if (passableMap[z - 1, x] == MapType.canWalk)
        {
            count++;
        }
        if (passableMap[z + 1, x] == MapType.canWalk)
        {
            count++;
        }
        if (passableMap[z, x + 1] == MapType.canWalk)
        {
            count++;
        }
        if (passableMap[z, x - 1] == MapType.canWalk)
        {
            count++;
        }
        if (count  == 4)
        {
            return true;
        }
        return false;

    }

    public bool Can_SetTreasure(int z, int x)
    {
        int count = 0;
        if (passableMap[z - 1, x] == MapType.wallRock)
        {
            count++;
        }
        if (passableMap[z + 1, x] == MapType.wallRock)
        {
            count++;
        }
        if (passableMap[z, x + 1] == MapType.wallRock)
        {
            count++;
        }
        if (passableMap[z, x - 1] == MapType.wallRock)
        {
            count++;
        }
        if (count >= 2 && count < 4)
        {
            return true;
        }
        return false;
    }




    public void Init_TreasureBox()
    {
        int count = 0;
        int loopcount = 0;
        bool fillout = false;
        for (int z = 3; z < passableMap.GetLength(0) - 2; z++)
        {
            loopcount++;
            z = z + (count / 2);
            if (z >= passableMap.GetLength(0)-2)
            {
                z = passableMap.GetLength(0) - 3;
            }
            //Debug.Log("数组总长度是" + passableMap.GetLength(0));
            //Debug.Log("循环次数"+loopcount);
            //Debug.Log("z是"+z);
            for (int x = 2; x < passableMap.GetLength(1) - 2; x++)
            {
                x = x + (count / 2);
                //Debug.Log("z是" + z+"x是"+x);
                if (x >= passableMap.GetLength(1)-2)
                {
                    x = passableMap.GetLength(1) - 3;
                }

                if (passableMap[z, x] == MapType.canWalk)
                {
                    if (count <TreasureBoxNum && Can_SetTreasure(z, x) == true)
                    {
                        int SetorNot = Random.Range(0, 9);
                        if (SetorNot <= 3)
                        {
                            TreasureBoxList.Add(Instantiate(treasureBox, wallmap[z, x].transform.position, new Quaternion(0f, 90f, 0f, 0f)));
                            findTheWayPlayerScript.GraphNobeArr[z, x].istreasureBox = true;
                            count++;
                        }
                    }
                    else if (count >=TreasureBoxNum)
                    {
                        fillout = true;
                        break;
                    }
                }
            }
            if (fillout == true)
            {
                break;
            }

        }
       
       

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



    public void TestPassableMap()
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

    public void Update()
    {
        

        for(int i=0; i < GateList.Count; i++)
        {
            GateList[i].GetComponent<Gate>().Turn_Cricle();
        }
    }


}
