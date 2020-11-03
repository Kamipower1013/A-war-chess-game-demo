using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public  class Player1Data 
{
    private static Player1Data Player1Data_instance;
    private Player1Data() { }
    public static Player1Data instance//懒汉式
    {
        get
        {
            if (Player1Data_instance == null)
            {
                Player1Data_instance = new Player1Data();
            }
            return Player1Data_instance;
        }

    }
    // GameSaveManager GameSaveManager = new GameSaveManager();
    [SerializeField] private string _Player_name;
    public string Player_name
    {
        get => _Player_name;

        set
        {
            if (value != "")
            {
                _Player_name = value;
                Debug.Log("将名字赋给私有变量");
            }
        }
    }




    public int playerData_number { get; } = 1;//存档编号

   [SerializeField] private int _gold_Coin = 0;
    public int gold_Coin
    {
        get => _gold_Coin;
        set
        {
            if (value >= 0)
            {
                _gold_Coin = value;
            }
        }
    }

    public int attack_value;
    public int level;
    public int HP;
    public int MP;
    public int dodge;
    public int defence_value;
    public int magic_attack;
    public int killnum { get; set; }
}

