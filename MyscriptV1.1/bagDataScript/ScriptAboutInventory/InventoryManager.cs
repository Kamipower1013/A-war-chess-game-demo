using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum equipment
{
    none,
    head,
    body,
    hand_sword,
    hand_magic,
    sheild,
    shoe
}




public class InventoryManager : MonoBehaviour
{  
    static public InventoryManager instance;
    public  Inventory mybag;
    public GameObject slotGrid;
    public GameObject Player;
   // public Sword1 sword1Perfab;
    public Text itemDircribe;
    public GameObject emptySlot;
    public List<GameObject> slots = new List<GameObject>();
    public AllItemsList AllItemsList;

     void Awake()
    {    
        
    }

    public void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            Debug.Log("InventoryManager单例脚本创建时出现重复，销毁之");
        }
        instance = this;

        Debug.Log("InventoryManager单例脚本创建一次");
        mybag = Player.GetComponent<PlayerCharactor>().current_bag;
        refreshItem();
        instance.itemDircribe.text = "";
    }

    //private void OnEnable()
    //{
    //    refreshItem();
    //    instance.itemDircribe.text = "";
    //}

    public static void updateItemDricibe(string itemInfor)
    {
        instance.itemDircribe.text = itemInfor;
    }
    //public static void createNewItem(item item)
    //{
    //    Sword1 newitem = Instantiate(instance.sword1Perfab, instance.slotGrid.transform.position, Quaternion.identity);
    //    newitem.gameObject.transform.SetParent(instance.slotGrid.transform);
    //    newitem.sword1Item = item;
    //    newitem.sword1Image.sprite = item.myitemimage;
    //    newitem.sword1Num.text = item.itemHeld.ToString();
    //}

    public static void refreshItem()
    {
        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
           // Debug.Log("刷新背包删除环节");
            instance.slots.Clear();
        }



        for(int i = 0; i < instance.mybag.items.Count; i++)
        {
            // Debug.Log("刷新背包重新添加"+i);
            Debug.Log("刷新装备了");
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotID = i;//slotID和item【i】一一对应起来
           
            instance.slots[i].GetComponent<Slot>().setupSlot(instance.mybag.items[i]);//将mybag的item传进来
                //createNewItem(instance.mybag.items[i]);
        }

    }

    public void ItemEquiped_removeInventory(item remove_item)
    {
        for(int i = 0; i < mybag.items.Count; i++)
        {
            if (mybag.items[i] != null)
            {     
                if (mybag.items[i].Data.itemID == remove_item.Data.itemID)
                {
                    Debug.Log("此装备移除");
                    mybag.items[i] = null;
                    break;
                }
            }
        }
       
        refreshItem();

    }


    public void ItemReturnInventory(item addtoinentory)
    { bool isBaghasit = false;
        for(int i = 0; i < mybag.items.Count; i++)
        {
            if (mybag.items[i] != null)
            {
                if (mybag.items[i].Data.itemID == addtoinentory.Data.itemID)
                {
                    Debug.Log("此时bag里有相同的装备，加1");
                    isBaghasit = true;
                    mybag.items[i].Data.itemHeld += 1;
                    break;
                }
            }
        }
        if (isBaghasit == false)
        {
            for (int i = 0; i < AllItemsList.allitemlist.Count; i++)
            {
                if (AllItemsList.allitemlist[i].Data.itemID == addtoinentory.Data.itemID)
                {
                    for (int j = 0; j < mybag.items.Count; j++)
                    {
                        if (mybag.items[j] == null)
                        {
                            mybag.items[j] = AllItemsList.allitemlist[i];
                            break;
                        }
                }
                    break;
                }
            }
        }
        
    }

}
