using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;


public class AnimAboutCard3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        DOTween.PlayForward("CardPerfab3Scale");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.PlayBackwards("CardPerfab3Scale");
    }
}
