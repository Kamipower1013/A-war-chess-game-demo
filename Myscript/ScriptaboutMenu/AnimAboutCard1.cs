using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

public class AnimAboutCard1: MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        
            DOTween.PlayForward("CardPerfab1Scale");
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
            DOTween.PlayBackwards("CardPerfab1Scale");
             
    }

   
}
