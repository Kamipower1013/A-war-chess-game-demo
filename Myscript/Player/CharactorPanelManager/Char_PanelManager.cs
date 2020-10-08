using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_PanelManager : MonoBehaviour
{
    public GameObject emptySolt;
    public Inventory playerChar_Equipment;
    public Inventory newPlayer_Char_panel;
    public Inventory player1_Bag;
    public Inventory player2_Bag;
    public void Start()
    {
        newPlayer_Char_panel.clear();
    }

    public void whichData_choicen()
    {
        if(GameSaveManager.instance.thedataNum == theChoicen_playerData.newplayerData)
        {
            playerChar_Equipment = newPlayer_Char_panel;
        }
        else if (GameSaveManager.instance.thedataNum == theChoicen_playerData.player1Data)
        {

        }
    }
}
