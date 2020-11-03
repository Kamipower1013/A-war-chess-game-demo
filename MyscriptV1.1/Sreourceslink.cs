using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sreourceslink : MonoBehaviour
{
    public Text test;
    public Resources测试 text;
    public GameObject cub;// Start is called before the first frame update
    void Start()
    {
        cub = Resources.Load<GameObject>("baglist/Cube");
       //text = Resources.Load<Resources测试>("baglist/Resources测试");
       // test.text = text.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
