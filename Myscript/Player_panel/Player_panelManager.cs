using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_panelManager : MonoBehaviour
{
    public static Player_panelManager instance;
    public Player_Panel Player_PanelBag;
    public PlayerCharactor PlayerCharactorScript;
    public AllItemsList AllItemsList;
    /// <summary>
    /// 面板数据在此
    ////// </summary>
    public GameObject HeadSlot;
    public GameObject BodySlot;
    public GameObject ShoeSlot;
    public GameObject Sword_Slot;
    public GameObject magic_Slot;
    public GameObject Shield_Slot;
    //
    public GameObject ItemDiscribe;
    public Text itemDiscribe;


    //Player_panel里面也有
    public int final_attack;//
    public int final_Defence;
    public int final_dodge;
    public int final_magic_attack;
    
    public item Theitemin_player;

    public Text final_attack_Text;
    public Text final_dfence_Text;
    public Text final_dodge_Text;
    public Text final_magic_attack_Text;
    public Text final_level;
    public Text final_ID;

    public void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        Theitemin_player = null;
        Debug.Log("角色面板初始化");
        openPlayer_Panel();
        final_ID.text = PlayerCharactorScript.Chara_name.ToString();
    }

    public void openPlayer_Panel()
    {
        Player_PanelBag.BodyItem = InventoryManager.instance.mybag.body_item;
        Player_PanelBag.HeadItem = InventoryManager.instance.mybag.Head_item;
        Player_PanelBag.ShoeItem = InventoryManager.instance.mybag.shoe_item;
        Player_PanelBag.Sword_weapon = InventoryManager.instance.mybag.hand_weapon_item;
        Player_PanelBag.magic_weapon = InventoryManager.instance.mybag.hand_magic_item;
        Player_PanelBag.sheild = InventoryManager.instance.mybag.shield_item;

        reflash_PlayerPanel();
        reflash_PlayerPanelData();
    }


    public void get_origin_playerData()
    {
        Player_PanelBag.get_FinalPlayer_Data(PlayerCharactorScript);
        final_attack = Player_PanelBag.final_attack;
        final_Defence = Player_PanelBag.final_Defence;
        final_dodge = Player_PanelBag.final_dodge;
        final_magic_attack = Player_PanelBag.final_magic_attack;
        PlayerCharactorScript.current_attack = final_attack;
        PlayerCharactorScript.current_defence = final_Defence;
        PlayerCharactorScript.current_dodge = final_dodge;
        PlayerCharactorScript.current_magic_attack = final_magic_attack;
    }


    public void reflash_PlayerPanelData()
    {
        get_origin_playerData();
        final_attack_Text.text = final_attack.ToString();
        final_dfence_Text.text = final_Defence.ToString();
        final_dodge_Text.text = final_dodge.ToString();
        final_magic_attack_Text.text = final_magic_attack.ToString();
        final_level.text = PlayerCharactorScript.level.ToString();
    }

    public void Save_PlayerPanelItemToData()
    {
        InventoryManager.instance.mybag.body_item = Player_PanelBag.BodyItem;
        InventoryManager.instance.mybag.Head_item = Player_PanelBag.HeadItem;
        InventoryManager.instance.mybag.shoe_item = Player_PanelBag.ShoeItem;
        InventoryManager.instance.mybag.hand_weapon_item = Player_PanelBag.Sword_weapon;
        InventoryManager.instance.mybag.hand_magic_item = Player_PanelBag.magic_weapon;
        InventoryManager.instance.mybag.shield_item = Player_PanelBag.sheild;
        InventoryManager.instance.mybag.Save_player_Panel_itemID();
    }

    


    public void reflash_PlayerPanel()
    {
        if (Player_PanelBag.HeadItem != null)
        {
            Debug.Log("头部装备存在");
            HeadSlot.GetComponent<PlayerPanelSlot>().has_Item(Player_PanelBag.HeadItem);
        }
        else
        {
            Debug.Log("头部装备不存在");
            HeadSlot.GetComponent<PlayerPanelSlot>().ThisItem.SetActive(false);
        }

        if (Player_PanelBag.BodyItem != null)
        {
            BodySlot.GetComponent<PlayerPanelSlot>().has_Item(Player_PanelBag.BodyItem);

        }
        else
        {
            BodySlot.GetComponent<PlayerPanelSlot>().ThisItem.SetActive(false);
        }

        if (Player_PanelBag.ShoeItem != null)
        {
            ShoeSlot.GetComponent<PlayerPanelSlot>().has_Item(Player_PanelBag.ShoeItem);

        }
        else
        {
            ShoeSlot.GetComponent<PlayerPanelSlot>().ThisItem.SetActive(false);

        }
        if (Player_PanelBag.Sword_weapon != null)
        {
            Sword_Slot.GetComponent<PlayerPanelSlot>().has_Item(Player_PanelBag.Sword_weapon);
        }
        else
        {
            Sword_Slot.GetComponent<PlayerPanelSlot>().ThisItem.SetActive(false);
        }
         if(Player_PanelBag.sheild != null)
        {
            Shield_Slot.GetComponent<PlayerPanelSlot>().has_Item(Player_PanelBag.sheild);
        }
        else
        {
            Shield_Slot.GetComponent<PlayerPanelSlot>().ThisItem.SetActive(false);
        }

        reflash_PlayerPanelData();

    }





    public void change_Player_Panel_bag(equipment equipment, item Player_panel_list)
    {
        if (equipment == equipment.body)
        {
            Player_PanelBag.BodyItem = Player_panel_list;
        }
        else if (equipment == equipment.head)
        {
            Player_PanelBag.HeadItem = Player_panel_list;
        }
        else if (equipment == equipment.shoe)
        {
            Player_PanelBag.ShoeItem = Player_panel_list;
        }
        else if (equipment == equipment.hand_sword)
        {
            Player_PanelBag.Sword_weapon = Player_panel_list;
        }
        else if (equipment == equipment.hand_magic)
        {
            Player_PanelBag.magic_weapon = Player_panel_list;
        }
        else if (equipment == equipment.sheild)
        {
            Player_PanelBag.sheild = Player_panel_list;
        }
    }

    public void equipmentItem(item equip_item)//equip_item是背包里面的Equipment
    {
        equipment whichneedEquip = equip_item.Data.equipment;
        Debug.Log("想装备的该装备是" + whichneedEquip);
        if (whichneedEquip != equipment.none)
        {
            if (whichneedEquip == equipment.body)
            {
                Theitemin_player = Player_PanelBag.BodyItem;
            }
            else if (whichneedEquip == equipment.head)
            {
                Theitemin_player = Player_PanelBag.HeadItem;
            }
            else if (whichneedEquip == equipment.shoe)
            {
                Theitemin_player = Player_PanelBag.ShoeItem;
            }
            else if (whichneedEquip == equipment.hand_sword)
            {
                Theitemin_player = Player_PanelBag.Sword_weapon;
            }
            else if (whichneedEquip == equipment.hand_magic)
            {
                Theitemin_player = Player_PanelBag.magic_weapon;
            }
            else if (whichneedEquip == equipment.sheild)
            {
                Theitemin_player = Player_PanelBag.sheild;
            }

            if (Theitemin_player != null)
            {
                Debug.Log("此装备栏不为空");
                if (Theitemin_player.Data.itemID == equip_item.Data.itemID)
                {
                    Debug.Log("相同装备存在，不需要替换");
                    return;
                }
                else if (Theitemin_player.Data.itemID != equip_item.Data.itemID)
                {
                    Debug.Log("装备的物品id为" + equip_item.Data.itemID);
                    for (int i = 0; i < Player_PanelBag.panelitemList.Count; i++)
                    {
                        if (Player_PanelBag.panelitemList[i].Data.itemID == equip_item.Data.itemID)
                        {

                            InventoryManager.instance.ItemReturnInventory(Theitemin_player);
                            change_Player_Panel_bag(whichneedEquip, Player_PanelBag.panelitemList[i]);


                            if (equip_item.Data.itemHeld > 1)
                            {
                                equip_item.Data.itemHeld -= 1;
                            }
                            else if (equip_item.Data.itemHeld == 1)
                            {
                                Debug.Log("因为装备数目为1，所以删除");
                                InventoryManager.instance.ItemEquiped_removeInventory(equip_item);

                            }
                            break;
                        }
                    }
                }
            }
            else if (Theitemin_player == null)
            {
                Debug.Log("装备的物品id为" + equip_item.Data.itemID);
                for (int i = 0; i < Player_PanelBag.panelitemList.Count; i++)
                {
                    if (Player_PanelBag.panelitemList[i].Data.itemID == equip_item.Data.itemID)
                    {
                        change_Player_Panel_bag(whichneedEquip, Player_PanelBag.panelitemList[i]);


                        if (equip_item.Data.itemHeld > 1)
                        {
                            equip_item.Data.itemHeld -= 1;
                            Debug.Log("因为装备数目大于1且装备栏为空，所以直接减1");
                        }
                        else if (equip_item.Data.itemHeld == 1)
                        {
                            Debug.Log("因为装备数目为1，所以删除");
                            InventoryManager.instance.ItemEquiped_removeInventory(equip_item);

                        }
                        break;
                    }
                }

            }

            reflash_PlayerPanel();
            InventoryManager.refreshItem();

        }
        else
        {
            Debug.Log("此物体不可装备");
        }


    }


    public void unloaditem_Inventory()
    {

    }



}
