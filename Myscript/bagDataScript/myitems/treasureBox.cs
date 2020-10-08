using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasureBox : MonoBehaviour
{   
    public item[] AllItem;
    public Inventory playerInventory;
    private string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag)==true)
        {
            int whichItem = Random.Range(0, 4);
            AddnewItem(AllItem[whichItem]);
            Destroy(gameObject);
        }
    }

    public void AddnewItem(item newitem)
    {
        if (playerInventory.items.Contains(newitem))
        {
            playerInventory.items.Add(newitem);
        }
        else
        {
            newitem.Data.itemHeld+= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
