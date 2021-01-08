using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class moveBag : MonoBehaviour,IDragHandler
{
    public Canvas TheCanvas;
    public RectTransform currentBagpos;
    public Scrollbar bagScrollBar;



    public void OnDrag(PointerEventData eventData)
    {
        currentBagpos.anchoredPosition += eventData.delta;//锚点坐标加上轻微的鼠标移动值
    }

   

    public void Awake()
    {
        currentBagpos = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bagScrollBar.size = 0.5f;
        bagScrollBar.value = 1f;
      
    }

}
