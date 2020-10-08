using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager 
{
    private static ItemDataManager _Instance;
    public static ItemDataManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new ItemDataManager();
                _Instance.items = new List<ItemData>();
            }
            return _Instance;
        }
    }

    List<ItemData> items;

    int idCounter = 1;

    public ItemData AddItem(int jsonID)
    {
        ItemData data = new ItemData(idCounter);
        idCounter++;
        data.jsonID = jsonID;
        items.Add(data);
        // 随机品质
        int q = Random.Range(1, 10);
        data.quality = q;
        return data;
    }

    public void RemoveItem(int autoID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].autoID == autoID)
            {
                items.RemoveAt(i);
                return;
            }
        }
    }

    public void ShowAllItems()
    {
        Debug.Log("=================");
        for (int i = 0; i < items.Count; i++)
        {
            ItemData data = items[i];
            ItemJsonData jd = data.jsonData;
            Debug.LogFormat("{0}:{1}  品质{2}", data.autoID, jd.name, data.quality);
        }
    }
}
