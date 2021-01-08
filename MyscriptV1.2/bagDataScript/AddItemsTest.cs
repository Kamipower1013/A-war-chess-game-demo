using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemsTest : MonoBehaviour
{   
    

    public item[] AllItem;
    public Inventory playerInventory;

    public GameObject Player;
    private string playerTag = "Player";
    public int create_ItemNum = 6;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Player.GetComponent<PlayerCharactor>().current_bag.name);
        playerInventory = Player.GetComponent<PlayerCharactor>().current_bag;
    }

    public void AddItems()
    {
        int whichItem = Random.Range(0, 5);
        AddnewItem(AllItem[whichItem]);
    }

    public void testRandom()
    {
        int whichItem = Random.Range(0, 5);
        Debug.Log(whichItem);
    }

    public void OnTriggerEnter(Collider other)//写到PlayerCharactor上
    {
        if (other.gameObject.CompareTag(playerTag) == true)
        {
            int whichItem = Random.Range(0, 5);
            AddnewItem(AllItem[whichItem]);
            Destroy(gameObject);
        }
    }

    public void AddnewItem(item newitem)
    {
        if (!playerInventory.items.Contains(newitem))
        {
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                if (playerInventory.items[i] == null)
                {
                    playerInventory.items[i] = newitem;
                    break;
                }
            }

        }
        else if (playerInventory.items.Contains(newitem))
        {
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                if (playerInventory.items[i] == newitem)
                {
                    playerInventory.items[i].Data.itemHeld += 1;

                }
            }

            //int index=playerInventory.items.FindIndex((item a)=>a.Equals(newitem));
            //Debug.Log("findindex查找" + index);
            //playerInventory.items[index].itemHeld += 1;

        }
        InventoryManager.refreshItem();


    }




    public void enemykill_Additems()
    {
        int count = 1;
        while (true)
        {
            AddItems();
            if (count > 6)
            {
                break;
            }
            count++;
        }
    }




    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        AddItems();
    //    }
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        testRandom();
    //    }
    //}
}
