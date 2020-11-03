using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyChara : Charactor
{
    public int enemyNum;
    public float HPsliderAmount;
    public float MPsliderAmount;
    public EnemyMovement EnemyMovementScript;
    public GameObject HPcanvas;
    public HPUIofEnemy HPUIofEnemyScript;
    public PlayerCharactor PlayerCharactorScript;
    public RougeMapCreate RougeMapCreateScript;
    public EnemyChara(string name, int HP, int MP, int base_attack, int base_defence, int dodge) : base(name, HP, MP, base_attack, base_defence, dodge)
    {




    }
    // Start is called before the first frame update
    void Start()
    {
        EnemyMovementScript = GetComponent<EnemyMovement>();
        HPUIofEnemyScript = HPcanvas.GetComponent<HPUIofEnemy>();
        this.Chara_name = "骷髅僵尸";
        this.HP = 100;
        this.MP = 200;
        this.base_attack = 25;
        this.base_defence = 20;
        this.dodge = 20;
        this.level = 1;
        
    }

    public void Enemy_attack_data()
    {
       
    }

    public void Enemy_attack(PlayerCharactor PlayerChara)
    {
        Debug.Log("进入enemy攻击主函数");
        normal_attack(PlayerChara);
        PlayerChara.Get_hurt_Reflash_playerStatus();
        PlayerChara.gameObject.GetComponent<playerMovement>().hurtAnim();
      
    }
    public void invokePlayerHurt(PlayerCharactor PlayerChara)
    {
        
    }

    public void Get_hurt_Reflash_Enemy_Status()
    {
        Debug.Log("进入了血条刷新");
        HPsliderAmount = (float)HP / 100;
        MPsliderAmount = (float)MP / 100;
        HPUIofEnemyScript.Refresh_enemy_HPsilder(HPsliderAmount);
        bool isdead = isthisEnemydead();
        if (isdead == true)
        {
            MapType thisEnemy_pos = thisEnemyMaptype1();
            RougeMapCreateScript.Refresh_Old_EnemyPos(thisEnemy_pos);
            PlayerCharactorScript.playerLevelup();
            EnemyMovementScript.enemy_deadAnim();
            Invoke("enemydeadSetactive", 3f);
            ListeningManager.instance.enemyDie_AfterAction();


        }
    }


    public void enemydeadSetactive()
    {
        transform.gameObject.SetActive(false);
        battleManager.instance.Refrash_AliveEnemy_Num();
    }
    public bool isthisEnemydead()
    {
        if (this.HP <=0)
        {
            return true;
        }
        return false;
    }

    public MapType thisEnemyMaptype1()
    {
        if (enemyNum == 0)
        {
            return MapType.enemy1_Pos;
        }
        else if (enemyNum == 1)
        {
            return MapType.enemy2_Pos;
        }
        else if (enemyNum == 2)
        {
            return MapType.enemy3_Pos;
        }
        else if (enemyNum == 3)
        {
            return MapType.enemy4_Pos;
        }
        
            return MapType.enemy5_Pos;
        
    }

    // Update is called once per frame
    void Update()
    {
       // Refresh_EnemyStatus();
    }
}
