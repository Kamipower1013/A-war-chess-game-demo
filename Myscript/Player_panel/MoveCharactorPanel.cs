using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MoveCharactorPanel : MonoBehaviour, IDragHandler
{
    public Canvas TheCanvas;
    public RectTransform currentCharactorPanelpos;
    
    public void OnDrag(PointerEventData eventData)
    {
        currentCharactorPanelpos.anchoredPosition += eventData.delta;//锚点坐标加上轻微的鼠标移动值
    }
    

    public void Start()
    {
        currentCharactorPanelpos = GetComponent<RectTransform>();
    }

}