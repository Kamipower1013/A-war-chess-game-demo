using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Data 
{
    private static Player2Data Player2Data_instance;
    private Player2Data() { }
    public static Player2Data instance//懒汉式
    {
        get
        {
            if (Player2Data_instance == null)
            {
                Player2Data_instance = new Player2Data();
            }
            return Player2Data_instance;
        }

    }
    
    [SerializeField] private string _player_name;
    public string player_name
    {
        get => _player_name;
        set => _player_name = value;
    }
  
    public int playerData_number { get; } = 1;//存档编号

    private int _gold_Coin = 0;
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

}
