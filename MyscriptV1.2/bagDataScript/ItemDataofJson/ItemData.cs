using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{

    // 设计数据
    public int jsonID;      // 

    // 动态数据
    public int autoID;      // 道具管理自动累加ID
    public int quality;     // 品质

    // -------------------
    public ItemData(int autoID)
    {
        this.autoID = autoID;
    }

    public ItemJsonData jsonData
    {
        get
        {
            return JsonDataManager.Instance.GetItemJsonData(jsonID);
        }
    }
}
