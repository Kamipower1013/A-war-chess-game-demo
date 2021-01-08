using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;
using UnityEngine.UI;
public class OpensenceSaveManager : MonoBehaviour
{
    public static OpensenceSaveManager instance
    {
        get;
        private set;
    }
    public GameObject savePanel;
    public Inventory mybagData;
    public Button savebutton;
    public Button loadbutton;
    public PlayerCharactor PlayerCharactorScript;
    public int thechoice_SavefileNUM;
    public InputField nameField;
    public InputField accountField;

    public void Awake()
    {
        instance = this;
    }


    public void SavePlayer1Bagdata()//存到一号档位的背包！！
    {

        if (!Directory.Exists(Application.persistentDataPath + "/demoV1_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/demoV1_data");//在c盘指定位置生成所谓的存档文件夹
        }

        BinaryFormatter bf = new BinaryFormatter();//创造用于二进制转换xuliehua

        FileStream SaveData_bag = File.Create(Application.persistentDataPath + "/demoV1_data/Player1BagData.txt");//创建或覆盖文件

        var Player1bagJson = JsonUtility.ToJson(mybagData);
        bf.Serialize(SaveData_bag, Player1bagJson);

        SaveData_bag.Close();
    }

    public void SavePlayer2Bagdata()
    {

        if (!Directory.Exists(Application.persistentDataPath + "/demoV1_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/demoV1_data");//在c盘指定位置生成所谓的存档文件夹
        }

        BinaryFormatter bf = new BinaryFormatter();//创造用于二进制转换xuliehua

        FileStream SaveData_Player2bag = File.Create(Application.persistentDataPath + "/demoV1_data/Player2BagData.txt");//创建或覆盖文件

        var Player2bagJson = JsonUtility.ToJson(mybagData);
        bf.Serialize(SaveData_Player2bag, Player2bagJson);

        SaveData_Player2bag.Close();
    }//存到2号档位背包

    public void SavePlayer1Data()
    {
        PlayerCharactorScript.create_recover_NewPlayer1Data();
        if (!Directory.Exists(Application.persistentDataPath + "/demoV1_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/demoV1_data");//在c盘指定位置生成所谓的存档文件夹
        }
        BinaryFormatter bf = new BinaryFormatter();//创造用于二进制转换xuliehua
        FileStream SaveData_Player1 = File.Create(Application.persistentDataPath + "/demoV1_data/Player1Data.txt");
        var Player1Json = JsonUtility.ToJson(Player1Data.instance);
        bf.Serialize(SaveData_Player1, Player1Json);
        SaveData_Player1.Close();
    }

    public void SavePlayer2Data()
    {
        PlayerCharactorScript.create_recover_NewPlayer2Data();
        if (!Directory.Exists(Application.persistentDataPath + "/demoV1_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/demoV1_data");//在c盘指定位置生成所谓的存档文件夹
        }
        BinaryFormatter bf = new BinaryFormatter();//创造用于二进制转换xuliehua
        FileStream SaveData_Player2 = File.Create(Application.persistentDataPath + "/demoV1_data/Player2Data.txt");
        var Player2Json = JsonUtility.ToJson(Player2Data.instance);
        bf.Serialize(SaveData_Player2, Player2Json);
        SaveData_Player2.Close();
    }

    public void loadPlayer1BagData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/demoV1_data/BagData.txt"))
        {
            FileStream OpensaveBagData = File.Open(Application.persistentDataPath + "/demoV1_data/Player1BagData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(OpensaveBagData), mybagData);
            OpensaveBagData.Close();
        }
    }


    public bool loadPlayer1Data()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/demoV1_data/Player1Data.txt"))
        {
            FileStream OpensaveBagData = File.Open(Application.persistentDataPath + "/demoV1_data/Player1Data.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(OpensaveBagData), mybagData);
            OpensaveBagData.Close();
            return true;
        }
        return false;
    }
    public void newgame_playerData()
    {
        NewGamePlayerData.instance.player_name = nameField.text;
        NewGamePlayerData.instance.level = 1;
    }



    public void loadPlayerData()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
