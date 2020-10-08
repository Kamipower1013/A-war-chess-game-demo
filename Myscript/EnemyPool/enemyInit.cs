using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInit : MonoBehaviour
{   public GameObject enemyprefabs;
    public  void  init_enemy()
    {
        if (EnemyPoolManager.GetInstance.enemyList.Count < 4)
        {
            EnemyPoolManager.GetInstance.enemyList.Add(Instantiate(enemyprefabs));
        }
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
