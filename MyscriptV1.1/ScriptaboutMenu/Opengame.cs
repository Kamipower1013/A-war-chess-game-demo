using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using DG.Tweening;
public class Opengame : MonoBehaviour
{
    public static Opengame instance;
    //public DOTweenAnimation DOTweenAnimation1;
    public Button Button_Startgame;
    public Button Button_readSaveData;
    public Button Button_quitGame;

    public GameObject GameSaveManager;
    public GameObject loadScreen;
    public Slider loadslider;
    public Text loadpercent;
    public Text showStart;

    public bool isfirstopenNewGamePanel;
    public bool isfirstopenDataPanel;
   // public bool isbig;
    public void Awake()
    {


    }
    public void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        // DOTweenAnimation1.autoKill = true;
        DOTween.Play("BackgroundFade");
        DOTween.Play("menuButtonFadeup");
        //DOTweenAnimation1.DOPlayById("BackgroundFade");
        //DOTweenAnimation1.DOPlayAllById("menuButtonFadeup");
     //   Debug.Log(DOTweenAnimation1.id);
        isfirstopenDataPanel = true;
        isfirstopenNewGamePanel = true;
      //  isbig = true;
       // DOTween.Play("disappear");
    }


    //public void scaleANim()
    //{
    //    if (isbig == true)
    //    {
    //        DOTween.PlayForward("disappear");
    //        isbig = false;
    //    }
    //    else if (isbig == false)
    //    {
    //        DOTween.PlayBackwards("disappear");
    //        isbig = true;
    //    }
    //}

    public void clickNewGameButton()
    {
        
        if (isfirstopenNewGamePanel == true)
        {
            DOTween.Play("startNewGamePanel");
            isfirstopenNewGamePanel = false;
        }
        else if (isfirstopenNewGamePanel == false)
        {
            DOTween.Restart("startNewGamePanel");
        }
    }

  
    public void Playgame_load_next()
    {
       
        DOTween.PlayBackwards("menuButtonFadeup");
        DOTween.PlayBackwards("BackgroundFade");
        Invoke("start_newgame_killtheAllDotAnim", 2f);
        Invoke("startNewGame_Coroutine", 2f);
    }


    public void startNewGame_Coroutine()
    {
        StartCoroutine("loadGame");
    }


    public void QuitGame()//结束程序
    {
        Application.Quit();
    }


    public void set_menu_acctivefalse()
    {
        transform.gameObject.SetActive(false);
    }

    public void choseData()
    {

    }
    public void clicktest()
    {
        Debug.Log("还可以点");
    }


    public void KilltheAnim_button()
    {
        DOTween.Kill("menuButtonFadeup");
        Button_Startgame.gameObject.SetActive(false);
        Button_readSaveData.gameObject.SetActive(false);
        Button_quitGame.gameObject.SetActive(false);

    }

    public void ClickandOpenSaveDataPanel_Anim()//刷新存档界面动画
    {
        if (isfirstopenDataPanel == true)
        {
            DOTween.Play("DataPanelFadeup");
        }
        else if (isfirstopenDataPanel == false)
        {
            DOTween.Restart("DataPanelFadeup");
        }
    }



    public void CloseButton_DataPanel()
    {
        isfirstopenDataPanel = false; 
    }

    public void start_newgame_killtheAllDotAnim()
    {
        DOTween.KillAll();
        Button_Startgame.gameObject.SetActive(false);
        Button_readSaveData.gameObject.SetActive(false);
        Button_quitGame.gameObject.SetActive(false);
    }



    public IEnumerator loadGame()
    {
        loadScreen.SetActive(true);
        AsyncOperation theLoadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        theLoadOperation.allowSceneActivation = false;

        while (!theLoadOperation.isDone)
        {
           // Debug.Log(theLoadOperation.isDone);
            loadslider.value = theLoadOperation.progress;
            loadpercent.text = theLoadOperation.progress * 100 + "%";

            if (theLoadOperation.progress >= 0.9f)
            {
                loadslider.value = 1f;
                loadpercent.text = "100%";
                showStart.gameObject.SetActive(true);
              
                if (Input.anyKeyDown)
                {
                    FXpool.instance.EffectList.Clear();
                   
                    theLoadOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    public void readSaveData()
    {
        // DOTweenAnimation1.autoKill = true;
        DOTween.PlayBackwards("menuButtonFadeup");
        DOTween.PlayBackwards("BackgroundFade");
        Invoke("KilltheAnim_button", 2f);
        //DOTweenAnimation1.DOPlayBackwardsAllById("menuButtonFadeup");
        //DOTweenAnimation1. DOPlayBackwardsById("BackgroundFade");
        // DOTween.Kill("menuButtonFadeup");

    }
    public void menugame()
    {
        // GameObject.Find("Canvas/menu/UI").SetActive(true);

    }

   
}
