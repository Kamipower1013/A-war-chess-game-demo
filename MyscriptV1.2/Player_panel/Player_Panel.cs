using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/new playerPanel")]
public class Player_Panel :ScriptableObject
{
     //static public Player_Panel instance;
    public item HeadItem;
    public item BodyItem;
    public item Sword_weapon;
    public item magic_weapon;
    public item ShoeItem;
    public item sheild;
    public GameObject Slot;


    public List<item> panelitemList=new List<item>();

    public Player_Panel()
    {
         for(int i = 0; i< 50; i++)
           {   
            panelitemList.Add(null);
          }
    }

public void get_FinalPlayer_Data(PlayerCharactor playerCharactorScript)
    {
        final_attack = playerCharactorScript.base_attack;
        final_Defence = playerCharactorScript.base_defence;
        final_dodge = playerCharactorScript.dodge;
        final_magic_attack = playerCharactorScript.Magic_Attack;
        if (Sword_weapon != null)
        {
            final_attack = final_attack + Sword_weapon.Data.attackValue;
        }
        if (BodyItem != null)
        {
            final_Defence = final_Defence+ BodyItem.Data.defenceValue;
        }
        if (HeadItem != null)
        {
            final_Defence = final_Defence + HeadItem.Data.defenceValue;
        }

        if (ShoeItem != null)
        {
            final_dodge = final_dodge + ShoeItem.Data.dodge;
        }
        if (sheild != null)
        {
            final_Defence = final_Defence + sheild.Data.defenceValue;
        }
    }



   public int final_attack;
   public int final_Defence;
   public int final_dodge;
   public int final_magic_attack;
   public int final_goldcoin;
 
   public int Level;
   public int HP;
   public int MP;
    

}
