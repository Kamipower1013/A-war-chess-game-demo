using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Data 
{
    private static Player3Data Player3Data_instance;
    private Player3Data() { }
    public static Player3Data instance//懒汉式
    {
        get
        {
            if (Player3Data_instance == null)
            {
                Player3Data_instance = new Player3Data();
            }
            return Player3Data_instance;
        }

    }
    public string player_name;
    public int playerData;
    public int gold_Coin;
    public int attack_value;
    public int level;
    public int HP;
    public int MP;
    public int dodge;
    public int defence_value;// Start is called be
}
