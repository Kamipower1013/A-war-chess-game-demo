using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AllItemList", menuName = "Inventory/AllItemList")]

[System.Serializable]
public class AllItemsList : ScriptableObject
{
    public List<item> allitemlist=new List<item>();

    public AllItemsList()
    {
        for(int i = 0; i < 50; i++)
        {
            allitemlist.Add(null);
        }
    }

}
