using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor:MonoBehaviour
{   public string Chara_name
    {
        get;
        set;
    }
    public int HP;
    public int MP;
    public int base_attack;
    public int base_defence;
    public int dodge;
    public int level;//闪避
   [SerializeField] private int _coin;
    public int coin
    {
        set =>_coin=value;
        get => _coin;
    }


    public Charactor(string name, int HP, int MP, int base_attack, int base_defence,int dodge)
    {
        this.Chara_name = name;
        this.HP = HP;
        this.MP = MP;
        this.base_attack = base_attack;
        this.base_defence = base_defence;
        this.dodge = dodge;
    }




    public  virtual void normal_attack(Charactor other)
    {   Debug.Log("玩家攻击了");

       
      int damage = base_attack - other.base_defence;
        other.HP = other.HP - damage;
    }

    public virtual void normal_attack(PlayerCharactor other)
    {
        Debug.Log("敌人对玩家的HP造成损失");
        if (base_attack - other.current_defence > 0)
        {
            int damage = base_attack - other.current_defence;
            other.HP = other.HP - damage;
        }
    }
    
   

    public virtual void Defence()
    {
        base_defence += 10;
    }

    public virtual void return_Defence00()
    {

    }
}
