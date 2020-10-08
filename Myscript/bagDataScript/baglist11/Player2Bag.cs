using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Bag : MonoBehaviour
{
    private static Player2Bag _instance;

    public static Player2Bag instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Player2Bag();
            }
            return _instance;
        }
    }

    private Player2Bag()
    {
        for (int i = 0; i < 50; i++)
        {
            thisItemData new_thisitemdata = new thisItemData();
            allitemData.Add(new_thisitemdata);

        }
        //Head_itemID = -1;
        //body_itemID = -1;
        //Shoe_itemID = -1;
        //hand_weapon_itemID = -1;
        //hand_magic_itemID = -1;
        //shield_itemID = -1;
    }

    public int Head_itemID;
    public int body_itemID;
    public int Shoe_itemID;
    public int hand_weapon_itemID;
    public int hand_magic_itemID;
    public int shield_itemID;
    public List<thisItemData> allitemData = new List<thisItemData>();
    public List<int> item_whichSlot = new List<int>();

}
