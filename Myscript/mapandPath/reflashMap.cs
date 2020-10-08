using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflashMap : MonoBehaviour
{
    public  RougeMapCreate RougeMapCreateScript;
    public findTheWayPlayer findTheWayPlayerScript;
    // Start is called before the first frame update
    void Start()
    {
        RougeMapCreateScript = GetComponent<RougeMapCreate>();
        findTheWayPlayerScript = GetComponent<findTheWayPlayer>();
    }
    //public void ReflashPlayerPos(Battle_State currentState)
    //{
    //    if (currentState == Battle_State.player_arrive)
    //    {   for(int z = 0; z < RougeMapCreateScript.passableMap.GetLength(0); z++)
    //        {
    //            for(int x = 0; x < RougeMapCreateScript.passableMap.GetLength(1); x++)
    //            { if (RougeMapCreateScript.passableMap[z, x] == MapType.player_Pos)
    //                {
    //                    RougeMapCreateScript.passableMap[z, x] = MapType.canWalk;
    //                }
    //              //if()
    //            }
    //        }
          
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
