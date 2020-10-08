using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlltheEnemy : MonoBehaviour
{
    public static AlltheEnemy instance;


   public GameObject[] enemyList;
   
    public void Awake()
    {
        
    }
// Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        Serialization_EnemyNumber();
        //enemyList[1].GetComponent<EnemyChara>().HP = 1000;
    }
    public void Serialization_EnemyNumber()
    {
        for(int i = 0; i < enemyList.Length; i++)
        {
            enemyList[i].GetComponent<EnemyAI>().enemyNumber = i;
            enemyList[i].GetComponent<EnemyMovement>().enemyNumber = i;
            enemyList[i].GetComponent<EnemyChara>().enemyNum = i;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
