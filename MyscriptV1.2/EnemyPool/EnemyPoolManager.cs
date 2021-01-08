using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager
{
   private static EnemyPoolManager _Instance;
    
   public static EnemyPoolManager GetInstance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new EnemyPoolManager();
            }
            return _Instance;
        }
    }

    public int EnemyCount=4;// Start is called before the first frame update
    public List<GameObject> enemyList = new List<GameObject>();
   
}
