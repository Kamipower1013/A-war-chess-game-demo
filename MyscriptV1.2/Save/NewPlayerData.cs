using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerData 
{
    private static NewPlayerData NewPlayerData_instance;
    private NewPlayerData() { }
    public static NewPlayerData instance//懒汉式
    {
        get
        {
            if (NewPlayerData_instance == null)
            {
                NewPlayerData_instance = new NewPlayerData();
            }
            return NewPlayerData_instance;
        }

    }
    // GameSaveManager GameSaveManager = new GameSaveManager();
    public string player_name;

    public int playerData_number { get; } = 0;//存档编号

    private int _gold_Coin = 100;
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

    public int attack_value = 45;
    public int level = 1;
    public int HP = 100;
    public int MP = 200;
    public int dodge = 5;
    public int defence_value = 5;
    public int magic_attack=20;

}
