using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bagmanager : MonoBehaviour
{
    public static bagmanager instance; 
    public GameObject mybag;
    bool isopen;
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);

        }
        instance = this;
        isopen = false;
    }
    public void OpenMybag()
    {
        isopen = mybag.activeSelf;
        isopen = !isopen;
        Debug.Log(isopen);
        mybag.SetActive(isopen);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenMybag();
            
        }
    }
}
