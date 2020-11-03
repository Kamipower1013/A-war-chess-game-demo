using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Menu : MonoBehaviour
{
    public static Menu instance;

    public bool isbig;
    public GameObject Drop_Item_panel;
    
    public void Start()
    {
        if (instance != null)
        {
            Destroy(this);

        }
        instance = this;
        isbig = false;
        Drop_Item_panel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void return_MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Drop_Item_panel_Anim()
    {
    //    if (Drop_Item_panel.activeInHierarchy == false)
    //    {
    //        Drop_Item_panel.SetActive(true);
    //    }
        if (isbig == true)
        {
            DOTween.PlayBackwards("disappear");
            isbig = false;
        }
        else if (isbig == false)
        {
            DOTween.PlayForward("disappear");
            isbig = true;
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
