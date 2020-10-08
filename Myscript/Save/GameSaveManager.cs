using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public enum theChoicen_playerData
{
    none,
    newplayerData,
    player1Data,
    player2Data
}



public enum ReadytoloadPlayerData
{
    none,
    playerData1,
    playerData2
}

public enum ReadytoSavePlayerData
{
    none,
    playerData1,
    playerData2
}

public enum choice_whichoneSave
{
    none,
    player1Data,
    player2Data
}

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance
    {
        get;
        private set;
    }

    public ReadytoloadPlayerData readytoLoadNum;
    public theChoicen_playerData thedataNum;
    public ReadytoSavePlayerData readytoSaveNum;
    public choice_whichoneSave save_in_num;

    //
    public InputField InputFieldof_name;
   // public Bagdata mybagData
    public Button loadbutton2;
    public PlayerCharactor PlayerCharactorScript;
    public Opengame opengameScript;

    //背包
    public Inventory newPlayerBagData;
    public Inventory player1BagData;
    public Inventory player2BagData;
    public AllItemsList allitemList;
    public Player_Panel Player_PanelList;

    //背包
   
    public GameObject Sure_LoadDataStartGame_panel;
    public GameObject NewGameSetPanel; //在函数setallnewgamewell中调用

    public int thescenceNum;
    //
    public Text SaveData1_Player1_name;
    public Text SaveData1_player1_level;
    public Text SaveData1_player1_goldcion;
    //
    public Text SaveData2_Player2_name;
    public Text SaveData2_player2_level;
    public Text SaveData2_player1_goldcion;
    //
    public int whichData;

    //
    public bool isHadPlayer1Data;
    public bool isHadPlayer2Data;
    //
    //
    



    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;//饿汉模式
        Debug.Log("Gamesavemanager的单例脚本被创建了一次");
        thedataNum = theChoicen_playerData.none;
        save_in_num = choice_whichoneSave.none;
        readytoLoadNum = ReadytoloadPlayerData.none;
        readytoSaveNum = ReadytoSavePlayerData.none;
    }


    public void Start()
    {
        DontDestroyOnLoad(this);
    }


    public void Startnewgame()
    {
        if (thedataNum == theChoicen_playerData.newplayerData)
        {

            loadnextScence();
            // movetonewScence();
        }
        else if (thedataNum == theChoicen_playerData.player1Data)
        {
            loadPlayer1Data();
            loadPlayer1BagData();
            loadnextScence();
            // movetonewScence();

        }
        else if (thedataNum == theChoicen_playerData.player2Data)
        {
            loadPlayer2Data();
            loadPlayer2BagData();
            loadnextScence();
            // movetonewScence();
        }

    }

    public void ChoiceLoad_Player1Data()
    {
        if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player1Data.json") == true)
        {   
            readytoLoadNum = ReadytoloadPlayerData.playerData1;
        }
        else if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player1Data.json") == false)
        {
            readytoLoadNum = ReadytoloadPlayerData.none;
            Sure_LoadDataStartGame_panel.SetActive(false);

        }
    }
    public void ChoiceLoad_Player2Data()
    {
        if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player2Data.json") == true)
        {
            readytoLoadNum = ReadytoloadPlayerData.playerData2;


        }
        else if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player2Data.json") == false)
        {
            readytoLoadNum = ReadytoloadPlayerData.none;
            Sure_LoadDataStartGame_panel.SetActive(false);
        }
    }

    public void sure_LoadData()
    {
        if (readytoLoadNum == ReadytoloadPlayerData.playerData1)
        {
            thedataNum = theChoicen_playerData.player1Data;
            Startnewgame();

        }
        else if (readytoLoadNum == ReadytoloadPlayerData.playerData2)
        {
            thedataNum = theChoicen_playerData.player2Data;
            Startnewgame();
        }
        else if (readytoLoadNum == ReadytoloadPlayerData.none)
        {
            thedataNum = theChoicen_playerData.none;
            Startnewgame();
        }
    }

    public void ChoiceSave_Player1Data()
    {  
        readytoSaveNum = ReadytoSavePlayerData.playerData1;
           
    }

    public void ChoiceSave_Player2Data()
    {
        readytoSaveNum = ReadytoSavePlayerData.playerData2;

    }

    public void cancel_SaveDataChoice()
    {
        readytoSaveNum = ReadytoSavePlayerData.none;
    }



    public void cancel_DataChoice()
    {
        readytoLoadNum = ReadytoloadPlayerData.none;
        thedataNum = theChoicen_playerData.none;
    }


    public void Sure_NewGame()
    {
        thedataNum = theChoicen_playerData.newplayerData;
    }

    public void cancel_StartNewGame()
    {
        thedataNum = theChoicen_playerData.none;
        readytoLoadNum = ReadytoloadPlayerData.none;
    }

    public void SetPlayerName()
    {
        NewPlayerData.instance.player_name = InputFieldof_name.text;
       // thedataNum = theChoicen_playerData.newplayerData;
        Debug.Log("创建新名称");
    }

    public void setallNewGamewell()
    {
        if (NewPlayerData.instance.player_name != null)
        {
            Sure_NewGame();
            NewGameSetPanel.SetActive(false);
            Startnewgame();
            newPlayerBagData.clear();
           // Ishad_currentBag();
        }
    }

    public void loadnextScence()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        opengameScript.Playgame_load_next();
        //thescenceNum = SceneManager.GetActiveScene().buildIndex + 1;
       
    }
    

    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Player1玩家1存档——存储
    /// </summary>
    public void SavePlayer1Bagdata(Inventory current_bag)
    {

        if (!Directory.Exists(Application.persistentDataPath + "/demoV2_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/demoV2_data");//在c盘指定位置生成所谓的存档文件夹
        }
        //current_bag.Save_Item_held();
        current_bag.save_Item_Player1bagIDData();
        
        BinaryFormatter bf = new BinaryFormatter();//创造用于二进制转换xuliehua

        FileStream SaveData1_bag = File.Create(Application.persistentDataPath + "/demoV2_data/Player1BagData.json");//创建或覆盖文件

        var Player1bagJson = JsonUtility.ToJson(Player1Bag.instance);
        bf.Serialize(SaveData1_bag,Player1bagJson);
        Debug.Log("存储player1背包成功");
        SaveData1_bag.Close();

    }
    

    public void SavePlayer1Data()
    {
        PlayerCharactorScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharactor>();
        PlayerCharactorScript.create_recover_NewPlayer1Data();
        if (!Directory.Exists(Application.persistentDataPath + "/demoV2_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/demoV2_data");//在c盘指定位置生成所谓的存档文件夹
        }
        BinaryFormatter bf = new BinaryFormatter();//创造用于二进制转换xuliehua
        FileStream SaveData_Player1 = File.Create(Application.persistentDataPath + "/demoV2_data/Player1Data.json");
        var PlayerJson = JsonUtility.ToJson(Player1Data.instance);
        bf.Serialize(SaveData_Player1, PlayerJson);
        SaveData_Player1.Close();
        Debug.Log("存储player1Data成功");
    }
    /// <summary>
    /// player2玩家2存档——存储
    /// </summary>
    public void SavePlayer2Data()
    {
        PlayerCharactorScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharactor>();
        PlayerCharactorScript.create_recover_NewPlayer2Data();
        if (!Directory.Exists(Application.persistentDataPath + "/demoV2_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/demoV2_data");//在c盘指定位置生成所谓的存档文件夹
        }
        BinaryFormatter bf = new BinaryFormatter();//创造用于二进制转换xuliehua
        FileStream SaveData_Player2 = File.Create(Application.persistentDataPath + "/demoV2_data/Player2Data.json");
        var PlayerJson = JsonUtility.ToJson(Player1Data.instance);
        bf.Serialize(SaveData_Player2, PlayerJson);
        SaveData_Player2.Close();
        Debug.Log("存储player2Data成功");
    }

    public void SavePlayer2Bagdata(Inventory current_bag)
    {

        if (!Directory.Exists(Application.persistentDataPath + "/demoV2_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/demoV2_data");//在c盘指定位置生成所谓的存档文件夹
        }
        current_bag.save_Item_Player2bagIDData();
        BinaryFormatter bf = new BinaryFormatter();//创造用于二进制转换xuliehua

        FileStream SaveData2_bag = File.Create(Application.persistentDataPath + "/demoV2_data/Player2BagData.json");//创建或覆盖文件

        var Player2bagJson = JsonUtility.ToJson(Player2Bag.instance);
        bf.Serialize(SaveData2_bag, Player2bagJson);
        Debug.Log("存储player1背包成功");
        SaveData2_bag.Close();

    }

    /// <summary>
    /// 玩家1——Load
    /// </summary>
    public void loadPlayer1BagData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player1BagData.json"))
        {
           // player1BagData.clear();
            FileStream OpensaveBagData = File.Open(Application.persistentDataPath + "/demoV2_data/Player1BagData.json", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(OpensaveBagData),Player1Bag.instance);
            Debug.Log("背包1被复写");
            OpensaveBagData.Close();

            player1BagData.Load_from_Player1Bag(allitemList, Player_PanelList);
        }
    }

    public bool loadPlayer1Data()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player1Data.json"))
        {
            FileStream Openplayer1data = File.Open(Application.persistentDataPath + "/demoV2_data/Player1Data.json", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(Openplayer1data), Player1Data.instance);
            Openplayer1data.Close();
            thedataNum = theChoicen_playerData.player1Data;
            return true;
        }
        return false;
    }

    public void movetonewScence()
    {
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName("GameSence"));
    }
    /// <summary>
    /// 玩家2——Load
    /// </summary>
    /// <returns></returns>
    public bool loadPlayer2Data()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player2Data.json"))
        {
            FileStream OpenPlayer2data = File.Open(Application.persistentDataPath + "/demoV2_data/Player2Data.json", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(OpenPlayer2data), Player2Data.instance);
            OpenPlayer2data.Close();
            thedataNum = theChoicen_playerData.player2Data;
            return true;
        }
        return false;
    }
    public void loadPlayer2BagData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player2BagData.txt"))
        {
           // player2BagData.clear();
            FileStream OpensaveBagData = File.Open(Application.persistentDataPath + "/demoV2_data/Player2BagData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(OpensaveBagData), Player2Bag.instance);
            Debug.Log("背包1被复写");
            OpensaveBagData.Close();
            player2BagData.Load_from_Player2Bag(allitemList, Player_PanelList);
        }
    }

    public bool savedata1onScreen(Text SaveData1_Player1_name, Text SaveData1_player1_level,Text SaveData1_GoldCion)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player1Data.json"))
        {
            FileStream Openplayer1data = File.Open(Application.persistentDataPath + "/demoV2_data/Player1Data.json", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(Openplayer1data), Player1Data.instance);

            SaveData1_Player1_name.text = Player1Data.instance.Player_name;
            SaveData1_player1_level.text = Player1Data.instance.level.ToString();
            SaveData1_GoldCion.text = Player1Data.instance.gold_Coin.ToString();
            Openplayer1data.Close();
            return true;
        }
        else if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player1Data.json") == false)
        {

            SaveData1_Player1_name.text = "无";
            SaveData1_player1_level.text = "无";
            SaveData1_GoldCion.text = "无";
            return false;

        }
        return false;
    }

    public bool savedata2onScreen(Text SaveData2_Player2_name, Text SaveData2_player2_level,Text SaveData2_player2_GoldCion)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player2Data.json"))
        {

            FileStream Openplayer2data = File.Open(Application.persistentDataPath + "/demoV2_data/Player2Data.json", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(Openplayer2data), Player2Data.instance);
            SaveData2_Player2_name.text = Player2Data.instance.player_name;
            SaveData2_player2_level.text = Player2Data.instance.level.ToString();
            SaveData2_player2_GoldCion.text = Player2Data.instance.gold_Coin.ToString();
            Openplayer2data.Close();
            return true;


        }
        else if (File.Exists(Application.persistentDataPath + "/demoV2_data/Player2Data.json") == false)
        {

            SaveData2_Player2_name.text = "无";
            SaveData2_player2_level.text = "无";
            SaveData2_player2_GoldCion.text = "无";
            return false;

        }

        return false;

    }


    public void Ishad_currentBag()
    {
        string Bagpath = "Assets/Resources/baglist";
       
        if (!File.Exists(Bagpath + "/NewBag.asset"))
        {
            Debug.Log("不存在Newbag,现在创建");
            Inventory newbag = ScriptableObject.CreateInstance<Inventory>();
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(newbag, Bagpath+ "/NewBag.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            newPlayerBagData = Resources.Load("Assets/Resources/baglist/NewBag",typeof(Inventory)) as Inventory;

#endif
        }
        else if(File.Exists(Bagpath + "/NewBag.asset"))
        {
             
            newPlayerBagData = Resources.Load<Inventory>("baglist/NewBag");
            newPlayerBagData.clear();
            Debug.Log("执行了NewBag，连接");

        }

    }
    public void Ishad_Player1Bag()
    {
        
        string Bagpath = "Assets/Resources/baglist";
        
        if (!File.Exists(Bagpath + "/Player1Bag.asset"))
        {
            Debug.Log("不存在Player1bag,现在创建");
            Inventory newbag = ScriptableObject.CreateInstance<Inventory>();
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(newbag, Bagpath + "/Player1Bag.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            player1BagData = Resources.Load("Assets/Resources/baglist/Player1Bag.asset", typeof(Inventory)) as Inventory;
#endif
        }
        else if (File.Exists(Bagpath + "/Player1Bag.asset"))
        {
            player1BagData = Resources.Load<Inventory>("baglist/Player1Bag");
            player1BagData.clear();
            Debug.Log("执行了Player1Bag，连接");
        }

    }
    public void Ishad_Player2Bag()
    {
        string Bagpath = "Assets/Resources/baglist";
       
        if (!File.Exists(Bagpath + "/Player2Bag.asset"))
        {
            Debug.Log("不存在Player2bag,现在创建");
            Inventory newbag = ScriptableObject.CreateInstance<Inventory>();
#if UNITY_EDITOR
            AssetDatabase.CreateAsset(newbag, Bagpath + "/Player2Bag.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
            player2BagData = Resources.Load<Inventory>("baglist/Player2Bag");
        }
        else if (File.Exists(Bagpath + "/Player2Bag.asset"))
        {
            player2BagData = Resources.Load<Inventory>("baglist/Player2Bag");
            player2BagData.clear();
            Debug.Log("执行了Player2Bag，连接");
        }

    }

    public void save_Bag_test()
    {
      
    }


}
