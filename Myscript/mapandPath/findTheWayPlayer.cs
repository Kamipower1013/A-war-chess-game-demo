using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class findTheWayPlayer : MonoBehaviour
{
    public DOTween dOTween_object;
    int arrayLength;
    /// <summary>
    /// 脚本引用
    /// </summary>
    RougeMapCreate RougeMapCreateScript;
    readAndsetMap readAndsetMapScript;
    public AlltheEnemy AlltheEnemyScript;
    //
    public int numberofStep = 8;
    //public int search_area = 5;
    TextMesh pathnumber;
    bool iniBFSover;
    LinkedList<int> mylist;
    [HideInInspector] public MapListNode[,] GraphNobeArr;
    bool isStart_xiechen;
    bool cannotfindplayer;
    // [HideInInspector] public List<MapListNode> queue;
    [HideInInspector] public List<MapListNode> queue_playerMoveto;//用来Bfs找最佳路径\是算法用队列
    [HideInInspector] public List<MapListNode> recordFindPlayerPath;//用来走格子
    [HideInInspector] public List<MapListNode> queue_playerCanMoveto;//用来点亮路径，可走路径\是算法用队列
    //
    //
    //
    //[HideInInspector] public List<MapListNode> queue_EnemyfindWay;
    [HideInInspector] public List<MapListNode> queue_EnemySearch;
    [HideInInspector] public List<MapListNode> queue_Enemyusedtofindway;
    [HideInInspector] public List<MapListNode> Record_EnemyFindPath;
    //[HideInInspector] public List<MapListNode>
    bool isArrive;//player走格子用的队列
    // Start is called before the first frame update
    void Start()
    {   
        iniBFSover = false;
        isStart_xiechen = false;
        RougeMapCreateScript = GetComponent<RougeMapCreate>();
        readAndsetMapScript = GetComponent<readAndsetMap>();
        arrayLength = RougeMapCreateScript.passableMap.GetLength(0);

       GraphNobeArr = new MapListNode[arrayLength, arrayLength];
        //
        queue_playerMoveto = new List<MapListNode>();
        recordFindPlayerPath = new List<MapListNode>();
        queue_playerCanMoveto = new List<MapListNode>();
        //
        queue_EnemySearch = new List<MapListNode>();
        queue_Enemyusedtofindway = new List<MapListNode>();
        Record_EnemyFindPath = new List<MapListNode>();

        initGraph_maplistNode();
        //enemyInitScript.init_enemy();
        RougeMapCreateScript.InitCharact_Pos(RougeMapCreateScript.Player);
        RougeMapCreateScript.InitCharact_Enemy_Pos(AlltheEnemyScript.enemyList);
        RougeMapCreateScript.testPassableMap();
        //RougeMapCreateScript.InitCharact_Enemy_Pos();
        contact_Arr();
        cannotfindplayer = false;
    }
    public void initGraph_maplistNode()
    {
        Debug.Log(RougeMapCreateScript.passableMap.GetLength(0));
        for (int z = 0; z < RougeMapCreateScript.passableMap.GetLength(0); z++)
        {
            for (int x = 0; x < RougeMapCreateScript.passableMap.GetLength(1); x++)
            {
                //Debug.Log("是否生成了Arr");
                GraphNobeArr[z, x] = new MapListNode(z, x,new Vector3(RougeMapCreateScript.wallmap[z, x].transform.position.x,0.31f, RougeMapCreateScript.wallmap[z, x].transform.position.z), RougeMapCreateScript.passableMap[z, x], 0);
                
            }
        }
    }
    public void contact_Arr()
    {
        for (int z = 0; z < RougeMapCreateScript.passableMap.GetLength(0); z++)
        {
            for (int x = 0; x < RougeMapCreateScript.passableMap.GetLength(1); x++)
            {
                if (z >= 1 && x >= 1 && z < (GraphNobeArr.GetLength(0) - 1) && x < (GraphNobeArr.GetLength(1) - 2))
                {
                    if (GraphNobeArr[z, x + 1].path_Element != MapType.wallRock && GraphNobeArr[z, x].path_Element != MapType.wallRock)
                    {
                        GraphNobeArr[z, x].NodeRight = GraphNobeArr[z, x + 1];
                       // Debug.Log("连了右");
                    }
                }
                if (z >= 1 && x > 1 && z < GraphNobeArr.GetLength(0) - 1 && x < GraphNobeArr.GetLength(1) - 1)
                {
                    if (GraphNobeArr[z, x - 1].path_Element != MapType.wallRock && GraphNobeArr[z, x].path_Element != MapType.wallRock)
                    {
                        GraphNobeArr[z, x].NodeLeft = GraphNobeArr[z, x - 1];
                        //Debug.Log("连了左");
                    }
                }
                if (z >= 1 && x >= 1 && (z < GraphNobeArr.GetLength(0) - 2) && x < GraphNobeArr.GetLength(1) - 1)
                {
                    if (GraphNobeArr[z + 1, x].path_Element != MapType.wallRock && GraphNobeArr[z, x].path_Element != MapType.wallRock)
                    {
                        GraphNobeArr[z, x].Nodeup = GraphNobeArr[z + 1, x];
                    }
                }
                if (z > 1 && x >= 1 && z < GraphNobeArr.GetLength(0) - 1 && x < GraphNobeArr.GetLength(1) - 1)
                {
                    if (GraphNobeArr[z - 1, x].path_Element != MapType.wallRock && GraphNobeArr[z, x].path_Element != MapType.wallRock)
                    {
                        GraphNobeArr[z, x].NodeDown = GraphNobeArr[z - 1, x];
                    }
                }
            }
        }

        iniBFSover = true;
    }//连接地图各元素

    public void Reflash_GraphNodeArr()//重置地图player、enemy位置元素
    {
        for (int z = 0; z < RougeMapCreateScript.passableMap.GetLength(0); z++)
        {
            for (int x = 0; x < RougeMapCreateScript.passableMap.GetLength(1); x++)
            {
                //Debug.Log("是否生成了Arr");
                GraphNobeArr[z, x].path_Element = RougeMapCreateScript.passableMap[z, x];
                //if(GraphNobeArr[z, x].path_Element==MapType.canWalk|| GraphNobeArr[z, x].path_Element == MapType.player_left || GraphNobeArr[z, x].path_Element == MapType.player_up|| GraphNobeArr[z, x].path_Element == MapType.player_down|| GraphNobeArr[z, x].path_Element == MapType.player_right)
                //{

                //}
            }
        }
    }

    public void Clear_Graph_Visited()//重置地图visited和FatherNode,pathcount
    {
        for (int z = 0; z < GraphNobeArr.GetLength(0); z++)
        {
            for (int x = 0; x < GraphNobeArr.GetLength(1); x++)
            {
                GraphNobeArr[z, x].isvisited = visited_State.none_visited;
                GraphNobeArr[z, x].fatherNode = null;
                GraphNobeArr[z, x].pathcount = 0;
            }
        }
    }



    public void StartBFS_ACtivePath()//启动函数、、、、、、已经嵌套进readsetMap脚本。点亮路径
    {
         Reflash_GraphNodeArr();
        for (int z = 0; z < GraphNobeArr.GetLength(0); z++)
        {
            for (int x = 0; x < GraphNobeArr.GetLength(1); x++)
            {

                if (GraphNobeArr[z, x].path_Element == MapType.player_Pos)
                {
                    Clear_Graph_Visited();
                    queue_playerCanMoveto.Clear();
                    queue_playerCanMoveto.Add(GraphNobeArr[z, x]);
                    //Debug.Log(GraphNobeArr[z, x].x + " " + GraphNobeArr[z, x].z);//开始点亮路径
                    //queue.Add(GraphNobeArr[z, x]);
                    GraphNobeArr[z, x].isvisited = visited_State.hadvisited;
                    ActivePathroundPlayer();// queueRec();
                    
                }
            }
        }

    }
    public void ActivePathroundPlayer()//用来点亮周围的Path
    {

        int count = 0;

        while (queue_playerCanMoveto[0].pathcount < numberofStep)//小于步数
        {
            count = queue_playerCanMoveto[0].pathcount;
            count++;
            //Debug.Log(count);
            RougeMapCreateScript.pathMap[queue_playerCanMoveto[0].z, queue_playerCanMoveto[0].x].SetActive(true);
            if (queue_playerCanMoveto[0].NodeDown != null && queue_playerCanMoveto[0].NodeDown.isvisited != visited_State.hadvisited)
            {
                queue_playerCanMoveto.Add(queue_playerCanMoveto[0].NodeDown);
                //Debug.Log("进队列了");
                queue_playerCanMoveto[0].NodeDown.isvisited = visited_State.hadvisited;
                queue_playerCanMoveto[0].NodeDown.fatherNode = queue_playerCanMoveto[0];
                queue_playerCanMoveto[0].NodeDown.pathcount = count;

            }
            if (queue_playerCanMoveto[0].NodeLeft != null && queue_playerCanMoveto[0].NodeLeft.isvisited != visited_State.hadvisited)
            {
                queue_playerCanMoveto.Add(queue_playerCanMoveto[0].NodeLeft);
                queue_playerCanMoveto[0].NodeLeft.isvisited = visited_State.hadvisited;
                queue_playerCanMoveto[0].NodeLeft.fatherNode = queue_playerCanMoveto[0];
                queue_playerCanMoveto[0].NodeLeft.pathcount = count;

            }
            if (queue_playerCanMoveto[0].Nodeup != null && queue_playerCanMoveto[0].Nodeup.isvisited != visited_State.hadvisited)
            {
                queue_playerCanMoveto.Add(queue_playerCanMoveto[0].Nodeup);
                queue_playerCanMoveto[0].Nodeup.isvisited = visited_State.hadvisited;
                queue_playerCanMoveto[0].Nodeup.fatherNode = queue_playerCanMoveto[0];
                queue_playerCanMoveto[0].Nodeup.pathcount = count;

            }
            if (queue_playerCanMoveto[0].NodeRight != null && queue_playerCanMoveto[0].NodeRight.isvisited != visited_State.hadvisited)
            {
                queue_playerCanMoveto.Add(queue_playerCanMoveto[0].NodeRight);
                queue_playerCanMoveto[0].NodeRight.isvisited = visited_State.hadvisited;
                queue_playerCanMoveto[0].NodeRight.fatherNode = queue_playerCanMoveto[0];
                queue_playerCanMoveto[0].NodeRight.pathcount = count;

            }
            queue_playerCanMoveto.RemoveAt(0);
        }
        Clear_Graph_Visited();
    }

    public void StartBFS_Find_movePath()//启动函数、、、、、、已经嵌套进readsetMap脚本用来MOVE
    {
       // Debug.Log("进入BFS找路函数");
        Reflash_GraphNodeArr();
        for (int z = 0; z < GraphNobeArr.GetLength(0); z++)
        {
            for (int x = 0; x < GraphNobeArr.GetLength(1); x++)
            {

                if (GraphNobeArr[z, x].path_Element == MapType.player_Pos)
                {
                    //Debug.Log("进入BFS找路函数");
                    queue_playerMoveto.Clear();
                    queue_playerMoveto.Add(GraphNobeArr[z, x]);
                    //Debug.Log(GraphNobeArr[z, x].x + " " + GraphNobeArr[z, x].z);//开始点亮路径
                    //queue.Add(GraphNobeArr[z, x]);
                    GraphNobeArr[z, x].isvisited = visited_State.hadvisited;
                    Player_findPath();// queueRec();
                    //StartCoroutine("queueRec");
                }
            }
        }

    }
    public int enemy_Start_BFS_searchPlayer(MapType thisenemyPos)
    {
        int pathcount=0;
        Reflash_GraphNodeArr();

        for (int z = 0; z < GraphNobeArr.GetLength(0); z++)
        {
            for (int x = 0; x < GraphNobeArr.GetLength(1); x++)
            {
                if (GraphNobeArr[z, x].path_Element == thisenemyPos)
                {
                    Clear_Graph_Visited();
                    queue_EnemySearch.Clear();
                    queue_EnemySearch.Add(GraphNobeArr[z, x]);
                    //Debug.Log(GraphNobeArr[z, x].x + " " + GraphNobeArr[z, x].z);//开始点亮路径
                    //queue.Add(GraphNobeArr[z, x]);
                    GraphNobeArr[z, x].isvisited = visited_State.hadvisited;
                    Debug.Log(GraphNobeArr[z, x].pathcount + "这是传入的敌人位置的Pathcount");
                    pathcount=enemy_searchArea();
                    //StartCoroutine("queueRec");
                }
            }
        }

        return pathcount;
    }



    public int player_aroundCanwalk()//用来读取周围是否可以站怪
    {
        for (int z = 0; z < GraphNobeArr.GetLength(0); z++)
        {
            for (int x = 0; x < GraphNobeArr.GetLength(1); x++)
            {
                //Debug.Log("是否生成了Arr");
                if (GraphNobeArr[z,x].path_Element == MapType.player_Pos)
                {
                    if (GraphNobeArr[z, x].Nodeup != null&&GraphNobeArr[z, x].Nodeup.path_Element == MapType.player_up )
                    {
                        return 1;
                    }
                    else if (GraphNobeArr[z, x].NodeLeft != null&&GraphNobeArr[z, x].NodeLeft.path_Element == MapType.player_left)
                    {
                        return 2;
                    }
                    else if (GraphNobeArr[z, x].NodeDown != null&&GraphNobeArr[z, x].NodeDown.path_Element == MapType.player_down)
                    {
                        return 3;
                    }
                    else if (GraphNobeArr[z, x].NodeRight != null&&GraphNobeArr[z, x].NodeRight.path_Element == MapType.player_right)
                    {
                        return 4;
                    }
                }
                

            }
        }
        Debug.Log("没有空位");
        return -1;

    }

    public int enemy_searchArea()//传出thisenemy到玩家的距离
    {
        int count = 0;
        //Record_EnemyFindPath.Clear();
        
        while (queue_EnemySearch.Count>0&&queue_EnemySearch[0].path_Element!=MapType.player_Pos)//小于步数
        {   
            count = queue_EnemySearch[0].pathcount;
            count++;
            Debug.Log(count);
            //RougeMapCreateScript.pathMap[queue_playerCanMoveto[0].z, queue_playerCanMoveto[0].x].SetActive(true);
            if (queue_EnemySearch[0].NodeDown!=null&& thisNodeCanWalk_EnemySearch(queue_EnemySearch[0].NodeDown) == true && queue_EnemySearch[0].NodeDown.isvisited != visited_State.hadvisited)
            {
                queue_EnemySearch.Add(queue_EnemySearch[0].NodeDown);
                //Debug.Log("进队列了");
                queue_EnemySearch[0].NodeDown.isvisited = visited_State.hadvisited;
                queue_EnemySearch[0].NodeDown.fatherNode = queue_EnemySearch[0];
                queue_EnemySearch[0].NodeDown.pathcount = count;

            }
            if (queue_EnemySearch[0].NodeLeft!=null&& thisNodeCanWalk_EnemySearch(queue_EnemySearch[0].NodeLeft) == true && queue_EnemySearch[0].NodeLeft.isvisited != visited_State.hadvisited)
            {
                queue_EnemySearch.Add(queue_EnemySearch[0].NodeLeft);
                queue_EnemySearch[0].NodeLeft.isvisited = visited_State.hadvisited;
                queue_EnemySearch[0].NodeLeft.fatherNode = queue_EnemySearch[0];
                queue_EnemySearch[0].NodeLeft.pathcount = count;

            }
            if (queue_EnemySearch[0].Nodeup !=null&& thisNodeCanWalk_EnemySearch(queue_EnemySearch[0].Nodeup) == true && queue_EnemySearch[0].Nodeup.isvisited != visited_State.hadvisited)
            {
                queue_EnemySearch.Add(queue_EnemySearch[0].Nodeup);
                queue_EnemySearch[0].Nodeup.isvisited = visited_State.hadvisited;
                queue_EnemySearch[0].Nodeup.fatherNode = queue_EnemySearch[0];
                queue_EnemySearch[0].Nodeup.pathcount = count;

            }
            if (queue_EnemySearch[0].NodeRight !=null&& thisNodeCanWalk_EnemySearch(queue_EnemySearch[0].NodeRight) == true && queue_EnemySearch[0].NodeRight.isvisited != visited_State.hadvisited)
            {
                queue_EnemySearch.Add(queue_EnemySearch[0].NodeRight);
                queue_EnemySearch[0].NodeRight.isvisited = visited_State.hadvisited;
                queue_EnemySearch[0].NodeRight.fatherNode = queue_EnemySearch[0];
                queue_EnemySearch[0].NodeRight.pathcount = count;

            }
            Debug.Log("remove前队列数目" + queue_EnemySearch.Count);
            queue_EnemySearch.RemoveAt(0);
            Debug.Log("remove后队列数目" + queue_EnemySearch.Count);
            if(queue_EnemySearch.Count<1)
            {   

                Debug.Log("队列为空退出");
                cannotfindplayer = true;
                break ;
              
            }
           
        }

        if (queue_EnemySearch.Count<1)
        {
            Debug.Log("没路了，path=20");
            int pathcount1 = 20;
            return pathcount1;
        }
        int pathcount = queue_EnemySearch[0].pathcount;
        //    if (queue_EnemySearch[0].path_Element == MapType.player_moveto)
        //         {
        //    Record_EnemyFindPath.Add(queue_EnemySearch[0]);
        //    int index = 0;
        //    while (Record_EnemyFindPath[index].fatherNode != null)
        //    {
        //        Record_EnemyFindPath.Add(Record_EnemyFindPath[index].fatherNode);
        //        index++;
               
        //    }
        //    Record_EnemyFindPath.Reverse();
        //    Debug.Log("得到路径list");
        //}
        Clear_Graph_Visited();
       
        return pathcount;
    }


    public void Player_findPath()//玩家用来找路，记录路径
    {
       // Debug.Log("开始找路");
        recordFindPlayerPath.Clear();
        int count = 0;

        while (queue_playerMoveto[0].path_Element != MapType.player_moveto)//小于步数
        {
            count = queue_playerMoveto[0].pathcount;
            count++;
           // Debug.Log("进入队列铺展");
            //RougeMapCreateScript.pathMap[queue_playerCanMoveto[0].z, queue_playerCanMoveto[0].x].SetActive(true);
            if (queue_playerMoveto[0].NodeDown != null && queue_playerMoveto[0].NodeDown.isvisited != visited_State.hadvisited)
            {
                queue_playerMoveto.Add(queue_playerMoveto[0].NodeDown);
                //Debug.Log("进队列了");
                queue_playerMoveto[0].NodeDown.isvisited = visited_State.hadvisited;
                queue_playerMoveto[0].NodeDown.fatherNode = queue_playerMoveto[0];
                queue_playerMoveto[0].NodeDown.pathcount = count;

            }
            if (queue_playerMoveto[0].NodeLeft != null && queue_playerMoveto[0].NodeLeft.isvisited != visited_State.hadvisited)
            {
                queue_playerMoveto.Add(queue_playerMoveto[0].NodeLeft);
                queue_playerMoveto[0].NodeLeft.isvisited = visited_State.hadvisited;
                queue_playerMoveto[0].NodeLeft.fatherNode = queue_playerMoveto[0];
                //Debug.Log("加了父节点");
                queue_playerMoveto[0].NodeLeft.pathcount = count;

            }
            if (queue_playerMoveto[0].Nodeup != null && queue_playerMoveto[0].Nodeup.isvisited != visited_State.hadvisited)
            {
                queue_playerMoveto.Add(queue_playerMoveto[0].Nodeup);
                queue_playerMoveto[0].Nodeup.isvisited = visited_State.hadvisited;
                queue_playerMoveto[0].Nodeup.fatherNode = queue_playerMoveto[0];
                queue_playerMoveto[0].Nodeup.pathcount = count;

            }
            if (queue_playerMoveto[0].NodeRight != null && queue_playerMoveto[0].NodeRight.isvisited != visited_State.hadvisited)
            {
                queue_playerMoveto.Add(queue_playerMoveto[0].NodeRight);
                queue_playerMoveto[0].NodeRight.isvisited = visited_State.hadvisited;
                queue_playerMoveto[0].NodeRight.fatherNode = queue_playerMoveto[0];
                queue_playerMoveto[0].NodeRight.pathcount = count;

            }
            queue_playerMoveto.RemoveAt(0);
        }
        if (queue_playerMoveto[0].path_Element == MapType.player_moveto)
        {
            recordFindPlayerPath.Add(queue_playerMoveto[0]);
            int index = 0;
            while (recordFindPlayerPath[index].fatherNode != null)
            {
                recordFindPlayerPath.Add(recordFindPlayerPath[index].fatherNode);
                index++;
                Debug.Log("index" + index);
            }
            recordFindPlayerPath.Reverse();
            Debug.Log("得到路径list");
        }
        Clear_Graph_Visited();
    }


    public void Enemy_startFindandRecordWay(MapType thisenemyPos,MapType near_playerPos)
    {   
        Debug.Log("进入记录路径函数");
       
        Reflash_GraphNodeArr();

        for (int z = 0; z < GraphNobeArr.GetLength(0); z++)
        {
            for (int x = 0; x < GraphNobeArr.GetLength(1); x++)
            {
                if (GraphNobeArr[z, x].path_Element == thisenemyPos)
                {
                    Debug.Log("这个具备找路条件开始找路的是" + thisenemyPos);
                    queue_Enemyusedtofindway.Clear();
                    queue_Enemyusedtofindway.Add(GraphNobeArr[z, x]);
                    //Debug.Log(GraphNobeArr[z, x].x + " " + GraphNobeArr[z, x].z);//开始点亮路径
                    //queue.Add(GraphNobeArr[z, x]);
                    GraphNobeArr[z, x].isvisited = visited_State.hadvisited;
                    enemy_findtheWay(near_playerPos);
                    
                    //StartCoroutine("queueRec");
                }
            }
        }

    }

    public  bool thisnodeCanWalk_EnemyWay(MapListNode thisNode)
    {
   
          //  Debug.Log("thisnode不等于null");
            if (thisNode.path_Element != MapType.player_Pos)
            {
                if (thisNode.path_Element != MapType.enemy1_Pos&&thisNode.path_Element!=MapType.enemy2_Pos&&thisNode.path_Element!=MapType.enemy3_Pos&& thisNode.path_Element != MapType.enemy4_Pos)
                {
                   // Debug.Log("thisnode可以走");
                    return true;
                }
                return false;
            }
                 
        //Debug.Log("墙不可走");
        return false;
    }

    public bool thisNodeCanWalk_EnemySearch(MapListNode thisNode)
    {
       
            //  Debug.Log("thisnode不等于null");
                if (thisNode.path_Element != MapType.enemy1_Pos && thisNode.path_Element != MapType.enemy2_Pos && thisNode.path_Element != MapType.enemy3_Pos && thisNode.path_Element != MapType.enemy4_Pos)
                {
                    Debug.Log("thisnode可以走");
                    return true;
                }
        
        Debug.Log("墙不可走");
        return false;
    }

    public void enemy_findtheWay(MapType near_playerpos)
    {
        int count = 0;
        Record_EnemyFindPath.Clear();
       
        while (queue_Enemyusedtofindway[0].path_Element!=near_playerpos)//小于步数
        {
          
            count = queue_Enemyusedtofindway[0].pathcount;
            count++;
           // Debug.Log(count+"这是enemy找路记录路径队列Count");
           
            if (queue_Enemyusedtofindway[0].NodeDown!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeDown)==true && queue_Enemyusedtofindway[0].NodeDown.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeDown);
                //Debug.Log("进队列了");
                queue_Enemyusedtofindway[0].NodeDown.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeDown.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeDown.pathcount = count;

            }
            if (queue_Enemyusedtofindway[0].NodeLeft!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeLeft) == true &&queue_Enemyusedtofindway[0].NodeLeft.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeLeft);
                queue_Enemyusedtofindway[0].NodeLeft.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeLeft.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeLeft.pathcount = count;

            }
            if (queue_Enemyusedtofindway[0].Nodeup!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].Nodeup) == true && queue_Enemyusedtofindway[0].Nodeup.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].Nodeup);
                queue_Enemyusedtofindway[0].Nodeup.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].Nodeup.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].Nodeup.pathcount = count;

            }
            if (queue_Enemyusedtofindway[0].NodeRight!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeRight) == true && queue_Enemyusedtofindway[0].NodeRight.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeRight);
                queue_Enemyusedtofindway[0].NodeRight.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeRight.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeRight.pathcount = count;

            }
           
            queue_Enemyusedtofindway.RemoveAt(0);

        }

        if (queue_Enemyusedtofindway[0].path_Element == near_playerpos)
        {
            Record_EnemyFindPath.Add(queue_Enemyusedtofindway[0]);
            int index = 0;
            while (Record_EnemyFindPath[index].fatherNode != null)
            {
                Record_EnemyFindPath.Add(Record_EnemyFindPath[index].fatherNode);
                index++;

            }
            Record_EnemyFindPath.Reverse();
            Debug.Log("记录敌人路径的长度" + Record_EnemyFindPath.Count);
            Debug.Log("得到路径list");
          
        }
        Clear_Graph_Visited();
    }
        
       

    public void player_attack_AreaActive()
    {
        Reflash_GraphNodeArr();
        for (int z = 0; z < GraphNobeArr.GetLength(0); z++)
        {
            for (int x = 0; x < GraphNobeArr.GetLength(1); x++)
            {

                if (GraphNobeArr[z, x].path_Element == MapType.player_Pos)
                {
                    if (GraphNobeArr[z, x].Nodeup!= null)
                    {
                        RougeMapCreateScript.pathMap[GraphNobeArr[z, x].Nodeup.z, GraphNobeArr[z, x].Nodeup.x].GetComponent<Renderer>().material.color = Color.yellow;
                        RougeMapCreateScript.pathMap[GraphNobeArr[z, x].Nodeup.z, GraphNobeArr[z, x].Nodeup.x].SetActive(true);
                    }
                     if(GraphNobeArr[z, x].NodeLeft != null)
                    {
                        RougeMapCreateScript.pathMap[GraphNobeArr[z, x].NodeLeft.z, GraphNobeArr[z, x].NodeLeft.x].GetComponent<Renderer>().material.color = Color.yellow;
                        RougeMapCreateScript.pathMap[GraphNobeArr[z, x].NodeLeft.z, GraphNobeArr[z, x].NodeLeft.x].SetActive(true);
                    }
                    if (GraphNobeArr[z, x].NodeDown != null)
                    {
                        RougeMapCreateScript.pathMap[GraphNobeArr[z, x].NodeDown.z, GraphNobeArr[z, x].NodeDown.x].GetComponent<Renderer>().material.color = Color.yellow;
                        RougeMapCreateScript.pathMap[GraphNobeArr[z, x].NodeDown.z, GraphNobeArr[z, x].NodeDown.x].SetActive(true);
                    }
                    if (GraphNobeArr[z, x].NodeRight != null)
                    {
                        RougeMapCreateScript.pathMap[GraphNobeArr[z, x].NodeRight.z, GraphNobeArr[z, x].NodeRight.x].GetComponent<Renderer>().material.color = Color.yellow;
                        RougeMapCreateScript.pathMap[GraphNobeArr[z, x].NodeRight.z, GraphNobeArr[z, x].NodeRight.x].SetActive(true);
                    }
                }
            }
        }
    }


    public void wanderPathFind(MapType thisenemyPos)
    {
        Debug.Log("Wander进入记录路径函数");

        Reflash_GraphNodeArr();

        for (int z = 0; z < GraphNobeArr.GetLength(0); z++)
        {
            for (int x = 0; x < GraphNobeArr.GetLength(1); x++)
            {
                if (GraphNobeArr[z, x].path_Element == thisenemyPos)
                {
                    Debug.Log("这个具备找路条件开始找路的是" + thisenemyPos);
                    queue_Enemyusedtofindway.Clear();
                    queue_Enemyusedtofindway.Add(GraphNobeArr[z, x]);
                    //Debug.Log(GraphNobeArr[z, x].x + " " + GraphNobeArr[z, x].z);//开始点亮路径
                    //queue.Add(GraphNobeArr[z, x]);
                    GraphNobeArr[z, x].isvisited = visited_State.hadvisited;
                    int dir = Random.Range(0, 4);
                    if (dir == 0)
                    {   
                        Wander_enemy_findtheWay_down_left(5);
                    }
                    else if(dir == 1)
                    {
                        Wander_enemy_findtheWay_up_right(5);
                    }
                    else if (dir == 2)
                    {
                        Wander_enemy_findtheWay_Right_Down(5);
                    }
                    else if (dir == 3)
                    {
                        Wander_enemy_findtheWay_left_up(5);
                    }
                    //StartCoroutine("queueRec");
                }
            }
        }
    }

    public void Wander_enemy_findtheWay_down_left(int Stepcount)
    {
        int count = 0;
        Record_EnemyFindPath.Clear();

        while (queue_Enemyusedtofindway[0].pathcount != Stepcount)//小于步数
        {

            count = queue_Enemyusedtofindway[0].pathcount;
            count++;
            // Debug.Log(count+"这是enemy找路记录路径队列Count");

            if (queue_Enemyusedtofindway[0].NodeDown!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeDown) == true && queue_Enemyusedtofindway[0].NodeDown.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeDown);
                //Debug.Log("进队列了");
                queue_Enemyusedtofindway[0].NodeDown.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeDown.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeDown.pathcount = count;

            }
            if (queue_Enemyusedtofindway[0].NodeLeft!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeLeft) == true && queue_Enemyusedtofindway[0].NodeLeft.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeLeft);
                queue_Enemyusedtofindway[0].NodeLeft.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeLeft.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeLeft.pathcount = count;

            }
            if (queue_Enemyusedtofindway[0].Nodeup!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].Nodeup) == true && queue_Enemyusedtofindway[0].Nodeup.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].Nodeup);
                queue_Enemyusedtofindway[0].Nodeup.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].Nodeup.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].Nodeup.pathcount = count;

            }
            if (queue_Enemyusedtofindway[0].NodeRight!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeRight) == true && queue_Enemyusedtofindway[0].NodeRight.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeRight);
                queue_Enemyusedtofindway[0].NodeRight.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeRight.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeRight.pathcount = count;

            }
            queue_Enemyusedtofindway.RemoveAt(0);

        }

        if (queue_Enemyusedtofindway[0].pathcount == Stepcount)
        {
            Record_EnemyFindPath.Add(queue_Enemyusedtofindway[0]);
            int index = 0;
            while (Record_EnemyFindPath[index].fatherNode != null)
            {
                Record_EnemyFindPath.Add(Record_EnemyFindPath[index].fatherNode);
                index++;

            }
            Record_EnemyFindPath.Reverse();
            Debug.Log("Wander状态记录敌人路径的长度" + Record_EnemyFindPath.Count);
            Debug.Log("得到路径list");

        }
        Clear_Graph_Visited();
    }

    public void Wander_enemy_findtheWay_up_right(int Stepcount)
    {
        int count = 0;
        Record_EnemyFindPath.Clear();

        while (queue_Enemyusedtofindway[0].pathcount != Stepcount)//小于步数
        {

            count = queue_Enemyusedtofindway[0].pathcount;
            count++;
            // Debug.Log(count+"这是enemy找路记录路径队列Count");

           
            
            if (queue_Enemyusedtofindway[0].Nodeup!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].Nodeup) == true && queue_Enemyusedtofindway[0].Nodeup.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].Nodeup);
                queue_Enemyusedtofindway[0].Nodeup.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].Nodeup.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].Nodeup.pathcount = count;

            }
            if (queue_Enemyusedtofindway[0].NodeRight!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeRight) == true && queue_Enemyusedtofindway[0].NodeRight.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeRight);
                queue_Enemyusedtofindway[0].NodeRight.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeRight.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeRight.pathcount = count;

            }

            if (queue_Enemyusedtofindway[0].NodeDown!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeDown) == true && queue_Enemyusedtofindway[0].NodeDown.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeDown);
                //Debug.Log("进队列了");
                queue_Enemyusedtofindway[0].NodeDown.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeDown.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeDown.pathcount = count;

            }
            if (queue_Enemyusedtofindway[0].NodeLeft!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeLeft) == true && queue_Enemyusedtofindway[0].NodeLeft.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeLeft);
                queue_Enemyusedtofindway[0].NodeLeft.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeLeft.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeLeft.pathcount = count;

            }
            queue_Enemyusedtofindway.RemoveAt(0);

        }

        if (queue_Enemyusedtofindway[0].pathcount == Stepcount)
        {
            Record_EnemyFindPath.Add(queue_Enemyusedtofindway[0]);
            int index = 0;
            while (Record_EnemyFindPath[index].fatherNode != null)
            {
                Record_EnemyFindPath.Add(Record_EnemyFindPath[index].fatherNode);
                index++;

            }
            Record_EnemyFindPath.Reverse();
            Debug.Log("Wander记录敌人路径的长度" + Record_EnemyFindPath.Count);
            Debug.Log("得到路径list");

        }
        Clear_Graph_Visited();
    }

    public void Wander_enemy_findtheWay_Right_Down(int Stepcount)
    {
        int count = 0;
        Record_EnemyFindPath.Clear();

        while (queue_Enemyusedtofindway[0].pathcount != Stepcount)//小于步数
        {

            count = queue_Enemyusedtofindway[0].pathcount;
            count++;
            // Debug.Log(count+"这是enemy找路记录路径队列Count");
            if (queue_Enemyusedtofindway[0].NodeRight!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeRight) == true && queue_Enemyusedtofindway[0].NodeRight.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeRight);
                queue_Enemyusedtofindway[0].NodeRight.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeRight.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeRight.pathcount = count;

            }

            if (queue_Enemyusedtofindway[0].NodeDown!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeDown) == true && queue_Enemyusedtofindway[0].NodeDown.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeDown);
                //Debug.Log("进队列了");
                queue_Enemyusedtofindway[0].NodeDown.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeDown.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeDown.pathcount = count;

            }

            if (queue_Enemyusedtofindway[0].NodeLeft!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeLeft) == true && queue_Enemyusedtofindway[0].NodeLeft.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeLeft);
                queue_Enemyusedtofindway[0].NodeLeft.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeLeft.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeLeft.pathcount = count;

            }


            if (queue_Enemyusedtofindway[0].Nodeup!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].Nodeup) == true && queue_Enemyusedtofindway[0].Nodeup.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].Nodeup);
                queue_Enemyusedtofindway[0].Nodeup.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].Nodeup.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].Nodeup.pathcount = count;

            }

            queue_Enemyusedtofindway.RemoveAt(0);

        }

        if (queue_Enemyusedtofindway[0].pathcount == Stepcount)
        {
            Record_EnemyFindPath.Add(queue_Enemyusedtofindway[0]);
            int index = 0;
            while (Record_EnemyFindPath[index].fatherNode != null)
            {
                Record_EnemyFindPath.Add(Record_EnemyFindPath[index].fatherNode);
                index++;

            }
            Record_EnemyFindPath.Reverse();
            Debug.Log("Wander记录敌人路径的长度" + Record_EnemyFindPath.Count);
            Debug.Log("得到路径list");

        }
        Clear_Graph_Visited();
    }
    public void Wander_enemy_findtheWay_left_up(int Stepcount)
    {
        int count = 0;
        Record_EnemyFindPath.Clear();

        while (queue_Enemyusedtofindway[0].pathcount != Stepcount)//小于步数
        {

            count = queue_Enemyusedtofindway[0].pathcount;
            count++;
            // Debug.Log(count+"这是enemy找路记录路径队列Count");

            if (queue_Enemyusedtofindway[0].NodeLeft!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeLeft) == true && queue_Enemyusedtofindway[0].NodeLeft.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeLeft);
                queue_Enemyusedtofindway[0].NodeLeft.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeLeft.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeLeft.pathcount = count;

            }


            if (queue_Enemyusedtofindway[0].Nodeup!=null&& thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].Nodeup) == true && queue_Enemyusedtofindway[0].Nodeup.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].Nodeup);
                queue_Enemyusedtofindway[0].Nodeup.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].Nodeup.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].Nodeup.pathcount = count;

            }

            if (queue_Enemyusedtofindway[0].NodeRight!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeRight) == true && queue_Enemyusedtofindway[0].NodeRight.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeRight);
                queue_Enemyusedtofindway[0].NodeRight.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeRight.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeRight.pathcount = count;

            }

            if (queue_Enemyusedtofindway[0].NodeDown!=null&&thisnodeCanWalk_EnemyWay(queue_Enemyusedtofindway[0].NodeDown) == true && queue_Enemyusedtofindway[0].NodeDown.isvisited != visited_State.hadvisited)
            {
                queue_Enemyusedtofindway.Add(queue_Enemyusedtofindway[0].NodeDown);
                //Debug.Log("进队列了");
                queue_Enemyusedtofindway[0].NodeDown.isvisited = visited_State.hadvisited;
                queue_Enemyusedtofindway[0].NodeDown.fatherNode = queue_Enemyusedtofindway[0];
                queue_Enemyusedtofindway[0].NodeDown.pathcount = count;

            }


            queue_Enemyusedtofindway.RemoveAt(0);

        }

        if (queue_Enemyusedtofindway[0].pathcount == Stepcount)
        {
            Record_EnemyFindPath.Add(queue_Enemyusedtofindway[0]);
            int index = 0;
            while (Record_EnemyFindPath[index].fatherNode != null)
            {
                Record_EnemyFindPath.Add(Record_EnemyFindPath[index].fatherNode);
                index++;

            }
            Record_EnemyFindPath.Reverse();
            Debug.Log("Wander记录敌人路径的长度" + Record_EnemyFindPath.Count);
            Debug.Log("得到路径list");

        }
        Clear_Graph_Visited();
    }


    //public void EnemyqueueRec_record()
    //{
    //    int count = 0;
    //    while (queue[0].path_Element != MapType.player_Pos)
    //    {
    //        count = queue[0].pathcount;
    //        count++;
    //        //RougeMapCreateScript.pathMap[queue[0].z, queue[0].x].SetActive(true);
    //        if (queue[0].nobedown != null && queue[0].nobedown.visited != 1)
    //        {
    //            queue.Add(queue[0].nobedown);
    //            queue[0].nobedown.visited = 1;
    //            mapset.pathMap[queue[0].i - 1, queue[0].j].GetComponentInChildren<TextMesh>().text = count.ToString();
    //            queue[0].nobedown.fatherNobe = queue[0];
    //            queue[0].nobedown.pathcount = count;
    //        }
    //        if (queue[0].nobeleft != null && queue[0].nobeleft.visited != 1)
    //        {
    //            queue.Add(queue[0].nobeleft);
    //            queue[0].nobeleft.visited = 1;
    //            mapset.pathMap[queue[0].i, queue[0].j - 1].GetComponentInChildren<TextMesh>().text = count.ToString();
    //            queue[0].nobeleft.fatherNobe = queue[0];
    //            queue[0].nobeleft.pathcount = count;
    //        }
    //        if (queue[0].nobeup != null && queue[0].nobeup.visited != 1)
    //        {
    //            queue.Add(queue[0].nobeup);
    //            queue[0].nobeup.visited = 1;
    //            mapset.pathMap[queue[0].i + 1, queue[0].j].GetComponentInChildren<TextMesh>().text = count.ToString();
    //            queue[0].nobeup.fatherNobe = queue[0];
    //            queue[0].nobeup.pathcount = count;
    //        }
    //        if (queue[0].noberight != null && queue[0].noberight.visited != 1)
    //        {
    //            queue.Add(queue[0].noberight);
    //            queue[0].noberight.visited = 1;
    //            mapset.pathMap[queue[0].i, queue[0].j + 1].GetComponentInChildren<TextMesh>().text = count.ToString();
    //            queue[0].noberight.fatherNobe = queue[0];
    //            queue[0].noberight.pathcount = count;
    //        }

    //        queue.RemoveAt(0);
    //        yield return new WaitForSeconds(0.01f);
    //    }
    //    if (queue[0].path_Element == MapType.player_Pos)
    //    {
    //        recordPath.Add(queue[0]);
    //        getpath();
    //        PrintPath();
    //    }
    //}

    //public void getpath()
    //{
    //    int index = 0;
    //    while (recordPath[index].fatherNobe != null)
    //    {
    //        recordPath.Add(recordPath[index].fatherNobe);
    //        index++;
    //    }

    //}
    //public void PrintPath()
    //{
    //    for (int i = 0; i < recordPath.Count; i++)
    //    {
    //        mapset.pathMap[recordPath[i].i, recordPath[i].j].GetComponent<Renderer>().material.color = Color.yellow;
    //    }
    //}

    //void Update()
    //{
    //    if (getPoints.over == true && iniBFSover == false)
    //    {
    //        initBFS();
    //        MyBFS();
    //    }

    //}

}
