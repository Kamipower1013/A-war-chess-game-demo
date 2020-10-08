using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//public class DragImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
//{
    //Transform canvas;
    //Transform content;
    //Transform ori;//原位置

    //public void Init(Transform _canvas, Transform _content)
    //{
    //    canvas = _canvas;
    //    content = _content;
    //}

    //public void OnPointerEnter(PointerEventData eventData)//鼠标进入时
    //{
    //    GetComponent<Outline>().enabled = true;
    //}

    //public void OnPointerExit(PointerEventData eventData)//鼠标离开时
    //{
    //    GetComponent<Outline>().enabled = false;
    //}
    //public void OnBeginDrag(PointerEventData eventData)//开始拖拽时
    //{
    //    GetComponent<Outline>().enabled = false;
    //    ori = transform.parent;
    //    transform.SetParent(canvas);
    //}
    //public void OnDrag(PointerEventData eventData)//鼠标持续拖拽时
    //{
    //    transform.position = eventData.position;
    //}
    //public void OnEndDrag(PointerEventData eventData)//结束拖拽时
    //{
    //    for (int i = 0; i < content.childCount; i++)
    //    {
    //        RectTransform slot = content.GetChild(i).GetComponent<RectTransform>();
    //        if (slot.rect.Contains(Input.mousePosition - slot.position))
    //        {
    //            if (slot.childCount > 0)
    //            {
    //                slot.GetChild(0).SetParent(ori);
    //                ori.GetChild(0).localPosition = Vector3.zero;
    //            }
    //            transform.SetParent(slot);
    //            transform.localPosition = Vector3.zero;
    //            return;
    //        }
    //    }
    //    transform.SetParent(ori);
    //    transform.localPosition = Vector3.zero;
    //}
//}
