using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlltheEnemy : MonoBehaviour
{
   public static AlltheEnemy instance;
   public GameObject[] enemyList;

    public Text Current_Enemy_Attack_Value;
    public Text Current_Enemy_defence_Value;
    public Text Current_Enemy_step_Num;
   
   
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
    
    public int theAliveEnemy_Num()
    {
        int count=0;
        for (int i = 0; i < enemyList.Length; i++)
        {
            if (enemyList[i].activeInHierarchy==true)
            {
                count++;
            }
           
        }
        return count;
    }
    //public int Get_Enemy_Attack(int enemyNum ,ref )
    //{
       
    //     return enemyList[enemyNum].GetComponent<EnemyChara>().base_attack;
        
    //}

    public void Change_Allenemy_attack_Value(int change_AttackValue)
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            enemyList[i].GetComponent<EnemyChara>().buff_Attackvalue = change_AttackValue;
        }
    }

    public void Change_Allenemy_Step(int change_StepValue)
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            enemyList[i].GetComponent<EnemyChara>().buff_PathStep = change_StepValue;
        }
    }

    public void change_Allenemy_Defence(int change_Defence)
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            enemyList[i].GetComponent<EnemyChara>().buff_DefenceValue=change_Defence;
        }
    }

    public void Refrash_All_Enemy_Staus()
    {
        Current_Enemy_Attack_Value.text = (enemyList[0].GetComponent<EnemyChara>().base_attack + enemyList[0].GetComponent<EnemyChara>().buff_Attackvalue).ToString();
            Current_Enemy_defence_Value.text = (enemyList[0].GetComponent<EnemyChara>().base_defence + enemyList[0].GetComponent<EnemyChara>().buff_DefenceValue).ToString();
        Current_Enemy_step_Num.text= (enemyList[0].GetComponent<EnemyMovement>().Enemycanwalk_step+ enemyList[0].GetComponent<EnemyChara>().buff_PathStep).ToString();

    }
  
}
