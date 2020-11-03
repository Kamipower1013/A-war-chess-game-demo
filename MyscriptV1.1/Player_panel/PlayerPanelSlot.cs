using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPanelSlot : MonoBehaviour
{
    public GameObject ThisItem;//装备图片
    public int SlotID;
    public Image itemImage;
    public string ItemInfo; 
   


    public void SetupSolt()
    {

    }

    public void has_Item(item ThisSlotItem)
    {
            ThisItem.SetActive(true);
            itemImage.sprite = ThisSlotItem.Data.myitemimage;
            ItemInfo = ThisSlotItem.Data.discribe;
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
