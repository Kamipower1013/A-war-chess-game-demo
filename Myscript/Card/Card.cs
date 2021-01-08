using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Card",menuName ="CardSystem/Card")]
[System.Serializable]
public class Card :ScriptableObject
{
    public int CardID;
    public CardType thisCardType;
    public string CardName;
    
    public int player_Path;
    public int enemy_Path;
    public int Add_attackValue;
    public int add_magic_attack_value;
    public int Add_defenceValue;
    public int add_HP;
    public int add_MP;
    public int add_dodge;
    [TextArea] public string discribe;
    // Update is called once per frame

}

