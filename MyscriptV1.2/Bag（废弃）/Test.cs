using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform sp;
    private void Start()
    {
        transform.SetParent(sp,false);
    }
}
