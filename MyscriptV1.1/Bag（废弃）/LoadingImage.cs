using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    //public Transform content;
    //public Transform canvas;
    //void Start()
    //{
    //    Loading();
    //}

    //void Loading()
    //{
    //    Image[] images = Resources.LoadAll<Image>("Image");
    //    Vector2 size = content.GetComponent<GridLayoutGroup>().cellSize;
    //    for (int i = 0; i < 5; i++)
    //    {
    //        //获取当前格子
    //        Transform slot = content.GetChild(i);
    //        if (slot.childCount == 0)
    //        {
    //            Image image = Instantiate(images[Random.Range(0, 3)], slot);
    //            image.rectTransform.sizeDelta = size;

    //            image.gameObject.AddComponent<DragImage>().Init(canvas, content);
    //        }
    //    }
    //}
    //public void ArrangePack()//一键整理
    //{
    //    int index = 0;
    //    for (int i = 0; i < content.childCount; i++)
    //    {
    //        if (content.GetChild(i).childCount > 0)
    //        {
    //            Transform image = content.GetChild(i).GetChild(0);
    //            image.SetParent(content.GetChild(index++));
    //            image.localPosition = Vector3.zero;
    //        }
    //    }
    //}
}
