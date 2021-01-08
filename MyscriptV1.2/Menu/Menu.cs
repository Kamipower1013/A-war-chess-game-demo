using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class Menu : MonoBehaviour
{
    public static Menu instance;

    public bool is_enemykillbig;
    public bool is_Treasurebox_panel_big;
    public bool isChangeTest;
    public GameObject Drop_Item_panel_EnemyDie;
    public GameObject Drop_Item_Panel_TreasureBox;
    public Text drop_Item_Text;
    public string treasureBox_string_Text;
    public string Enemydead_string_Text;
    public void Start()
    {
        if (instance != null)
        {
            Destroy(this);

        }
        instance = this;


        is_enemykillbig = false;
        is_Treasurebox_panel_big = false;
        Drop_Item_panel_EnemyDie.SetActive(true);
        Enemydead_string_Text = "击败敌人！掉落了7件装备";
        treasureBox_string_Text = "宝箱被打开了，得到装备一件";
    }



    public void QuitGame()
    {
        Application.Quit();
    }

    public void return_MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void change_stringText()
    {

    }

    public void Drop_Item_Treasure_Panel_Anim()
    {
        if (is_Treasurebox_panel_big == true)
        {
            DOTween.PlayBackwards("disappear_TreasureBox");
            is_Treasurebox_panel_big = false;
        }
        else if (is_Treasurebox_panel_big== false)
        {
            DOTween.PlayForward("disappear_TreasureBox");
            is_Treasurebox_panel_big = true;
        }
    }


    public void Drop_Item_panel_Anim()
    {
    //    if (Drop_Item_panel.activeInHierarchy == false)
    //    {
    //        Drop_Item_panel.SetActive(true);
    //    }
        if (is_enemykillbig== true)
        {
            DOTween.PlayBackwards("disappear");
            is_enemykillbig = false;
        }
        else if (is_enemykillbig == false)
        {
            DOTween.PlayForward("disappear");
            is_enemykillbig = true;
        }
    }

    public void Defence_PanelAnim()
    {
        DOTween.PlayForward("DfeneceValue");
    }


    public void Replay_Defence_PanelAnim()
    {
        DOTween.PlayBackwards("DfeneceValue");
    }
    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        Drop_Item_panel_Anim();
    //    }
    //}

}
