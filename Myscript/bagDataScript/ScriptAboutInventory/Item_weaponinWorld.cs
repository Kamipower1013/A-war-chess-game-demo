using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_inWorld : MonoBehaviour
{
    public item Thisitem;
    public Inventory Player_inventory;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            addthisItem();
        }
        
    }
    void Start()
    {
        
    }
    public void addthisItem()
    {
        if (!Player_inventory.items.Contains(Thisitem))
        {
            //Player_inventory.items.Add(Thisitem);
            //InventoryManager.createNewItem(Thisitem);
            for(int i = 0; i < Player_inventory.items.Count; i++)
            {
                if (Player_inventory.items[i] == null)
                {
                    Player_inventory.items[i] = Thisitem;
                }
            }

        }
        else if (Player_inventory.items.Contains(Thisitem))
        {
            Thisitem.Data.itemHeld += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
