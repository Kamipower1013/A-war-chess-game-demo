using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myClickFX : MonoBehaviour
{
    public ParticleSystem myFX;

    public RectTransform thisRect;

    public void ThisDIsappear()
    {
        Invoke("BacktoListDisappear", 2f);
    }

    public void Remove_Appear(Vector2 MousePos)
    {
        
        FXpool.instance.EffectList.Remove(thisRect);
        thisRect.anchoredPosition = MousePos;
        gameObject.SetActive(true);
        FXplay();
        ThisDIsappear();
    }

    public void FXplay()
    {
        myFX.Play();

    }

    public void BacktoListDisappear()
    {
        FXpool.instance.EffectList.Add(thisRect);
        gameObject.SetActive(false);
       
    }

}
