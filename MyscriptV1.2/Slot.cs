using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour
{
    public int slotID;//空格子的ID，也等于装备的ID
    public item slotItem;
    public Image slotImage;
    public Text slotNum;
    public GameObject iteminslot;
    public string slotInfor;

     /// <summary>
    /// 
    /// </summary>
    Transform _canvas;
    Transform _content;


    public void itemClick_infor()
    {
        InventoryManager.updateItemDricibe(slotInfor);

    }
    void Start()
    {
        
    }

    public void init_in_slot()
    {

    }
    public void setupSlot(item item)//在背包刷新初始化的时候调用
    {
        if (item == null)
        {
            iteminslot.SetActive(false);//如果这个格子没有item，则将其设为false；
            return;
        }
        slotImage.sprite = item.Data.myitemimage;
        slotNum.text = item.Data.itemHeld.ToString();
        slotInfor = item.Data.discribe;
        slotItem = item;
    }
    // Update is called once per frame
   
}
