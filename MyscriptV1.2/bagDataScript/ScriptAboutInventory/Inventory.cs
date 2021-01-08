using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/new inventory")]

[System.Serializable]
public class Inventory : ScriptableObject
{
    public List<item> items = new List<item>();
    public List<Weapon> weapons = new List<Weapon>();

    // [SerializeField]public List<List<int>> itemsData = new List<List<int>>();
    /// <summary>
    /// 好烦啊！！！！！！！！为什么item作为一个不能序列化
    /// </summary>
    /// 
    public item Head_item;
    public item body_item;
    public item shoe_item;
    public item hand_weapon_item;
    public item hand_magic_item;
    public item shield_item;

    public List<thisItemData> allItemsData = new List<thisItemData>();
    public int Head_itemID;
    public int body_itemID;
    public int Shoe_itemID;
    public int hand_weapon_itemID;
    public int hand_magic_itemID;
    public int shield_itemID;                                                             //不得已，用普通类来记录装备变化
    public Inventory()
    {
        for (int i = 0; i < 50; i++)
        {

            items.Add(null);
            thisItemData new_thisItemData = new thisItemData();
            allItemsData.Add(new_thisItemData);
            Head_itemID = -1;
            body_itemID = -1;
            Shoe_itemID = -1;
            hand_weapon_itemID = -1;
            hand_magic_itemID = -1;
            shield_itemID = -1;                                                             //不得已，用普通类来记录装备变化

        }

    }


    public void Save_player_Panel_itemID()
    {
        if (Head_item != null)
        {
            Head_itemID = Head_item.Data.itemID;
        }
        if (body_item != null)
        {
            body_itemID = body_item.Data.itemID;
        }
        if (shoe_item != null)
        {
            Shoe_itemID = shoe_item.Data.itemID;
        }
        if (hand_weapon_item!= null)
        {
            hand_weapon_itemID = hand_weapon_item.Data.itemID;
        }
        if (hand_magic_item != null)
        {
            hand_magic_itemID = hand_magic_item.Data.itemID;
        }
        if (shield_item != null)
        {
            shield_itemID = shield_item.Data.itemID;
        }
    }

    public void Let_player_panelID_inventroytoPlayer1Bag()
    {
        Player1Bag.instance.body_itemID = body_itemID;
        Player1Bag.instance.Head_itemID = Head_itemID;
        Player1Bag.instance.hand_weapon_itemID = hand_weapon_itemID;
        Player1Bag.instance.Shoe_itemID = Shoe_itemID;
        Player1Bag.instance.shield_itemID = shield_itemID;
        Player1Bag.instance.hand_magic_itemID = hand_magic_itemID;
    }

    public void Let_player_panelID_inventroytoPlayer2Bag()
    {
        Player2Bag.instance.body_itemID = body_itemID;
        Player2Bag.instance.Head_itemID = Head_itemID;
        Player2Bag.instance.hand_weapon_itemID = hand_weapon_itemID;
        Player2Bag.instance.Shoe_itemID = Shoe_itemID;
        Player2Bag.instance.shield_itemID = shield_itemID;
        Player2Bag.instance.hand_magic_itemID = hand_magic_itemID;
    }

    public void save_Item_Player1bagIDData()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                Player1Bag.instance.allitemData[i].itemID = -1;
                Player1Bag.instance.allitemData[i].itemName = "无";
                Player1Bag.instance.allitemData[i].itemHeld = -2;
            }
            else if (items[i] != null)
            {
                Player1Bag.instance.allitemData[i].itemID = items[i].Data.itemID;//直接从items里赋值
                Player1Bag.instance.allitemData[i].itemHeld = items[i].Data.itemHeld;
            }
        }

        Let_player_panelID_inventroytoPlayer1Bag();
    }


    public void save_Item_Player2bagIDData()
    {   

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                Player2Bag.instance.allitemData[i].itemID = -1;
                Player2Bag.instance.allitemData[i].itemName = "无";
                Player2Bag.instance.allitemData[i].itemHeld = -2;
            }
            else if (items[i] != null)
            {
                Player2Bag.instance.allitemData[i].itemID = items[i].Data.itemID;//直接从items里赋值
                Player2Bag.instance.allitemData[i].itemHeld = items[i].Data.itemHeld;
            }
        }

        Let_player_panelID_inventroytoPlayer2Bag();
    }

    


    public void Load_from_Player1Bag(AllItemsList allItemsList, Player_Panel player_panel_list)
    {
        for (int i = 0; i < Player1Bag.instance.allitemData.Count; i++)
        {
            if (Player1Bag.instance.allitemData[i].itemID != -1)
            {
                for (int j = 0; j < allItemsList.allitemlist.Count; j++)
                {
                    if (allItemsList.allitemlist[j] != null)
                    {
                        if (allItemsList.allitemlist[j].Data.itemID == Player1Bag.instance.allitemData[i].itemID)
                        {
                            this.items[i] = allItemsList.allitemlist[j];
                            this.items[i].Data.itemHeld = Player1Bag.instance.allitemData[i].itemHeld;
                        }
                    }
                }
            }

        }

        //Debug.Log("Player1Bag.instance.Head_itemID" + Player1Bag.instance.Head_itemID);
        //Debug.Log("Player1Bag.instance.body_itemID" + Player1Bag.instance.body_itemID);
        //Debug.Log("Player1Bag.instance.Shoe_itemID" + Player1Bag.instance.Shoe_itemID);
        //Debug.Log("Player1Bag.instance.hand_weapon_itemID" + Player1Bag.instance.hand_weapon_itemID);


        if (Player1Bag.instance.Head_itemID != -1)
        {
           for(int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i]!= null)
                {
                    if(Player1Bag.instance.Head_itemID== player_panel_list.panelitemList[i].Data.itemID)
                    {   

                        this.Head_item = player_panel_list.panelitemList[i];

                    }

                }

            }
        }
        if (Player1Bag.instance.body_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player1Bag.instance.body_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.body_item= player_panel_list.panelitemList[i];
                    }

                }

            }
        }
        if (Player1Bag.instance.Shoe_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player1Bag.instance.Shoe_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.shoe_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }
        if (Player1Bag.instance.hand_weapon_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player1Bag.instance.hand_weapon_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.hand_weapon_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }
        if (Player1Bag.instance.hand_magic_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player1Bag.instance.hand_magic_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.hand_magic_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }

       
        if (Player1Bag.instance.shield_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player1Bag.instance.shield_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.shield_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }
    }


    public void Load_from_Player2Bag(AllItemsList allItemsList, Player_Panel player_panel_list)
    {
        for (int i = 0; i < Player2Bag.instance.allitemData.Count; i++)
        {
            if (Player2Bag.instance.allitemData[i].itemID != -1)
            {
                for (int j = 0; j < allItemsList.allitemlist.Count; j++)
                {
                    if (allItemsList.allitemlist[j] != null)
                    {
                        if (allItemsList.allitemlist[j].Data.itemID == Player2Bag.instance.allitemData[i].itemID)
                        {
                            this.items[i] = allItemsList.allitemlist[j];
                            this.items[i].Data.itemHeld = Player2Bag.instance.allitemData[i].itemHeld;
                        }
                    }
                }
            }

        }

        //Debug.Log("Player1Bag.instance.Head_itemID" + Player1Bag.instance.Head_itemID);
        //Debug.Log("Player1Bag.instance.body_itemID" + Player1Bag.instance.body_itemID);
        //Debug.Log("Player1Bag.instance.Shoe_itemID" + Player1Bag.instance.Shoe_itemID);
        //Debug.Log("Player1Bag.instance.hand_weapon_itemID" + Player1Bag.instance.hand_weapon_itemID);


        if (Player2Bag.instance.Head_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player2Bag.instance.Head_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {

                        this.Head_item = player_panel_list.panelitemList[i];

                    }

                }

            }
        }
        if (Player2Bag.instance.body_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player2Bag.instance.body_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.body_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }
        if (Player2Bag.instance.Shoe_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player2Bag.instance.Shoe_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.shoe_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }
        if (Player2Bag.instance.hand_weapon_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player2Bag.instance.hand_weapon_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.hand_weapon_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }
        if (Player2Bag.instance.hand_magic_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player2Bag.instance.hand_magic_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.hand_magic_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }


        if (Player2Bag.instance.shield_itemID != -1)
        {
            for (int i = 0; i < player_panel_list.panelitemList.Count; i++)
            {
                if (player_panel_list.panelitemList[i] != null)
                {
                    if (Player2Bag.instance.shield_itemID == player_panel_list.panelitemList[i].Data.itemID)
                    {
                        this.shield_item = player_panel_list.panelitemList[i];
                    }

                }

            }
        }
    }

    public void Load_save_item_held()
    {
        //Debug.Log("进入Held副本");

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                for (int j = 0; j < allItemsData.Count; j++)
                {
                    if (items[i].Data.itemID == allItemsData[j].itemID)
                    {
                        //Debug.Log("items[i].Data.itemID是" + items[i].Data.itemID + "allItemsData[j].itemID是" + allItemsData[j].itemID);
                        //Debug.Log(items[i].Data.itemID + "号装备更新Held");
                        items[i].Data.itemHeld = allItemsData[j].itemHeld;
                    }
                }
            }
        }
    }



    public void clear()
    {
        this.items.Clear();
        this.weapons.Clear();
        this.allItemsData.Clear();
        for (int i = 0; i < 50; i++)
        {

            items.Add(null);
            thisItemData new_thisItemData = new thisItemData();
            allItemsData.Add(new_thisItemData);

        }
    }


    public void itemreturn_inventory(item return_item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                if (items[i].Data.itemID == return_item.Data.itemID)
                {
                    items[i].Data.itemHeld += 1;
                }
            }
        }

    }


    public void Save_Item_held()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {


                allItemsData[i].itemName = items[i].Data.itemName;
                allItemsData[i].itemID = items[i].Data.itemID;
                allItemsData[i].itemHeld = items[i].Data.itemHeld;

                //  allItemsData[i].equipment = items[i].Data.equipment;

            }
            else if (items[i] == null)
            {

                Debug.Log("存了一个空背包数据");
                allItemsData[i].itemName = "无";
                allItemsData[i].itemID = -1;
                allItemsData[i].itemHeld = -2;
                allItemsData[i].equipment = equipment.none;

            }
        }
    }

}
