using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class LoadOldDataOnScreen : MonoBehaviour
{
    public GameObject GameSaveManager_object;
    //public GameSaveManager GameSaveManagerScript;
    public Text player1Data_name;
    public Text player1Data_level;
    public Text player1Data_goldcion;
    public Text player2Data_name;
    public Text player2Data_level;
    public Text player2Data_goldcion;
    /// <summary>
    /// 
    /// </summary>
    public GameObject player;
    public PlayerCharactor PlayerCharactorScript;
    /// <summary>
    /// 
    /// </summary>
    public void openMenu()
    {
        player1Data_name = GameObject.FindGameObjectWithTag("Data1_PlayerName").GetComponent<Text>();
        player1Data_level = GameObject.FindGameObjectWithTag("Data1_PlayerLevel").GetComponent<Text>();
        player1Data_goldcion = GameObject.FindGameObjectWithTag("Data1_PlayerCion").GetComponent<Text>();
        player2Data_goldcion = GameObject.FindGameObjectWithTag("Data2_PlayerCion").GetComponent<Text>();
        player2Data_name = GameObject.FindGameObjectWithTag("Data2_PlayerName").GetComponent<Text>();
        player2Data_level = GameObject.FindGameObjectWithTag("Data2_PlayerLevel").GetComponent<Text>();

        GameSaveManager.instance.savedata1onScreen(player1Data_name, player1Data_level, player1Data_goldcion);
        GameSaveManager.instance.savedata2onScreen(player2Data_name, player2Data_level, player2Data_goldcion);
    }




    public void Choice_Data1_Save()
    {
        GameSaveManager.instance.ChoiceSave_Player1Data();
    }
    public void Choice_Data2_Save()
    {
        GameSaveManager.instance.ChoiceSave_Player2Data();
    }
    public void cancel_data_choice_save()
    {
        GameSaveManager.instance.cancel_SaveDataChoice();
    }

    public void sure_Whichone_save()
    {
        if (GameSaveManager.instance.readytoSaveNum == ReadytoSavePlayerData.playerData1)
        {
            Save_in_PlayerData1();

        }
        else if (GameSaveManager.instance.readytoSaveNum == ReadytoSavePlayerData.playerData2)
        {
            Save_in_PlayerData2();
        }
        else if (GameSaveManager.instance.readytoSaveNum == ReadytoSavePlayerData.none)
        {

        }
    }


    public void Save_in_PlayerData1()
    {
        Player_panelManager.instance.Save_PlayerPanelItemToData();
        //直接将panel处的item给背包inventroy
        GameSaveManager.instance.SavePlayer1Data();
        GameSaveManager.instance.SavePlayer1Bagdata(PlayerCharactorScript.current_bag);
        GameSaveManager.instance.savedata1onScreen(player1Data_name, player1Data_level, player1Data_goldcion);//关于player本身不用考虑

    }
    public void Save_in_PlayerData2()
    {
        Player_panelManager.instance.Save_PlayerPanelItemToData();//直接将panel处的item给背包inventroy
        GameSaveManager.instance.SavePlayer2Data();
        GameSaveManager.instance.SavePlayer2Bagdata(PlayerCharactorScript.current_bag);
        GameSaveManager.instance.savedata2onScreen(player2Data_name, player2Data_level, player2Data_goldcion);
    }



    public void Start()
    {
        //GameSaveManager_object = GameObject.FindGameObjectWithTag("GameSaveManager");
        //GameSaveManagerScript = GameSaveManager.GetComponent<GameSaveManager>();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayerCharactorScript = player.GetComponent<PlayerCharactor>();
            Debug.Log("loadOdataScreen脚本,playerCharactor链接");


        }


    }
}
