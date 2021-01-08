using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/new item")]
[System.Serializable]
public class item : ScriptableObject
{
    [SerializeField]
    public thisItemData Data;

}
    [Serializable]
    public class thisItemData
    {
        public int itemID;
        public string itemName;
        public Sprite myitemimage;
        public GameObject thisItemModel;
        public int attackValue;
        public int magic_attack_value;
        public int defenceValue;
        public int add_HP;
        public int add_MP;
        public int dodge;
        public int itemHeld;
        public equipment equipment;
        [TextArea] public string discribe;


    public int GetItemID()
    {
        return itemID;
    }
    }
   
    

    
