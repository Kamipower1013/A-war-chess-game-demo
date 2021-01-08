using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePlayerData
{
    private static NewGamePlayerData NewGamePlayer_instance;
    private NewGamePlayerData() { }
    public static NewGamePlayerData instance//懒汉式
    {
        get
        {
            if (NewGamePlayer_instance == null)
            {
                NewGamePlayer_instance = new NewGamePlayerData();
            }
            return NewGamePlayer_instance;
        }

    }
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

    public int attack_value = 10;
    public int level = 1;
    public int HP = 100;
    public int MP = 100;
    public int dodge = 5;
    public int defence_value = 5; 
}
