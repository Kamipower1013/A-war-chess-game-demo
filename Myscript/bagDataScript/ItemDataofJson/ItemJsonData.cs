using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class ItemJsonData 
{
    public int id { get; }            //ID
    public int type { get; }          //类型
    public string name { get; }       //名字
    public float attack { get; }      //攻击力
    public float crit { get; }        //暴击率
    public float defense { get; }     //防御
    public float dodge { get; }       //闪避
    public string modelPath { get; }  //模型加载路径
    public string imagePath { get; }  //图片加载路径

    public ItemJsonData(int _id, int _type, string _name, float _attack, float _crit, float _defense, float _dodge, string _modelPath, string _imagePath)
    {
        id = _id;
        type = _type;
        name = _name;
        attack = _attack;
        crit = _crit;
        defense = _defense;
        dodge = _dodge;
        modelPath = _modelPath;
        imagePath = _imagePath;
    }
}

public class JsonDataManager
{
    //单例模式
    private static JsonDataManager _Instance;
    public static JsonDataManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new JsonDataManager();
                _Instance.InitItemData();
            }
            return _Instance;
        }
    }

    //读取表信息,将每条数据分别存入一个ItemData类中
    private Dictionary<int, ItemJsonData> itemDatas = new Dictionary<int, ItemJsonData>();

    public ItemJsonData GetItemJsonData(int id)
    {
        if (!itemDatas.ContainsKey(id))
        {
            Debug.LogWarning("道具ID" + id + "不存在");
            return null;
        }
        return itemDatas[id];
    }

    private void InitItemData() //初始化表数据
    {
        JsonData allData = JsonMapper.ToObject(Resources.Load<TextAsset>("Data/ItemData").text);//
        for (int i = 0; i < allData.Count; i++)
        {
            itemDatas.Add(int.Parse(allData[i]["ID"].ToString()), new ItemJsonData(
                int.Parse(allData[i]["ID"].ToString()),
                int.Parse(allData[i]["Type"].ToString()),
                allData[i]["Name"].ToString(),
                float.Parse(allData[i]["Attack"].ToString()),
                float.Parse(allData[i]["Crit"].ToString()),
                float.Parse(allData[i]["Defense"].ToString()),
                float.Parse(allData[i]["Dodge"].ToString()),
                allData[i]["ModelPath"].ToString(),
                allData[i]["ImagePath"].ToString()
                ));
        }
    }

}
