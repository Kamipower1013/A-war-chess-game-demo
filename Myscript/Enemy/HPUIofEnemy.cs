using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPUIofEnemy : MonoBehaviour
{
    Image hpslider;
   public float HPsliderAmount;
    EnemyChara EnemyCharaScript;
    void Start()
    {
        hpslider = GetComponentInChildren<Image>();
       // EnemyCharaScript = GetComponentInParent<EnemyChara>();
    }


    public void Refresh_enemy_HPsilder(float thisENemyhpsliderAmount)
    {
        Debug.Log("应该刷新了血条");
        hpslider.fillAmount = thisENemyhpsliderAmount;
    }
    // Update is called once per frame
    void Update()
    {
        //current_HPsilder();
        transform.rotation = Camera.main.transform.rotation;
    }
}
