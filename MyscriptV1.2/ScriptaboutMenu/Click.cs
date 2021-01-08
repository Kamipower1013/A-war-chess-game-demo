using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class Click : MonoBehaviour
{
    public RectTransform clickFXPerfab;
    public ParticleSystem clickFX;
    public RectTransform mouseClick_Pos;
    public Canvas FXcanvas;
    public Transform click_Pos;
    public Vector3 click_Pos_V3;

    // public Vector2 click_Pos_V2;
    // Start is called before the first frame update
    void Start()
    {
        //  clickFX = clickFXPerfab.GetComponentInChildren<ParticleSystem>();
       
    }

  
    public void initFX(Vector2 mousePos)
    {
        var newFX=Instantiate(clickFXPerfab, FXcanvas.transform);
        newFX.anchoredPosition = mousePos;
        newFX.GetComponent<myClickFX>().FXplay();
        newFX.GetComponent<myClickFX>().ThisDIsappear();

    }

    //public void disappear(RectTransform thisFx)
    //{   
    //    thisFx.gameObject.SetActive(false);

    //}


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 MousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (FXpool.instance.EffectList.Count > 0)
            {

                FXpool.instance.EffectList[0].GetComponent<myClickFX>().Remove_Appear(MousePos);
            }
            else
            {
                initFX(MousePos);
            }
         
        }

      


        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    Debug.Log("jinrudianjitexiao");
        //    clickFXPerfab.anchoredPosition = eventData.position;
        //    clickFX.Play();


        //}
    }
}
