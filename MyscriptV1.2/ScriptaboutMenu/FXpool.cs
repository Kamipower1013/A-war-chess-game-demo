using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXpool
{
    private static FXpool _instance;
    private FXpool() { }
    public static FXpool instance {
        get
        {
            if (_instance == null)
            {
                _instance = new FXpool();
            }

            return _instance;
        }

    }

   public List<RectTransform> EffectList = new List<RectTransform>();
   //public List<RectTransform> SaveList = new List<RectTransform>();
}
