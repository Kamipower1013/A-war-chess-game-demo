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

    public MeshRenderer meshRenderer;
    public Material thisMat;
    public Shader thisenemyShader;
    public float dead_shader_dissolve_value;
    public float[] list;
    public float victory;
    public ParticleSystem Dead_Particle;
    public Transform Root;

    public EnemyChara(string name, int HP, int MP, int base_attack, int base_defence, int dodge) : base(name, HP, MP, base_attack, base_defence, dodge)
    {




    }

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
      
        thisMat = meshRenderer.material;
        thisenemyShader = thisMat.shader;
        dead_shader_dissolve_value = thisMat.GetFloat("Dissvion_Value");
        dead_shader_dissolve_value = 0;
    }

    public IEnumerator Enemy_shaderChange_dead_Coro()
    {
        dead_shader_dissolve_value = thisMat.GetFloat("Dissvion_Value");
        Dead_Particle.Play();
        while (dead_shader_dissolve_value<1f)
        {
            dead_shader_dissolve_value = thisMat.GetFloat("Dissvion_Value");

            dead_shader_dissolve_value = Mathf.SmoothDamp(dead_shader_dissolve_value,1f, ref victory,1.8f,0.5f);

            thisMat.SetFloat("Dissvion_Value", dead_shader_dissolve_value);
            Dead_Particle.gameObject.transform.position = Root.position;
            yield return new WaitForSeconds(0);
            
        }
       // Debug.Log("退出协程");
    }

    public void Start_corotine_Enemy_shaderChange_dead()
    {

        StartCoroutine(Enemy_shaderChange_dead_Coro());

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
           // dead_shader_dissolve_value= thisenemyShader.GetPropertyDefaultFloatValue(1);
            Invoke("enemydeadSetactive", 6f);
            Start_corotine_Enemy_shaderChange_dead();
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&enemyNum==0)
        {   
            HP = 0;
            Get_hurt_Reflash_Enemy_Status();
        }
        
    }

}
