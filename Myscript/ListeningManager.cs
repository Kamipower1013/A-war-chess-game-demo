using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeningManager : MonoBehaviour
{
    public static ListeningManager instance;

    public AddItemsTest AddItemsTestScript;
    public delegate void EnemyDie_afterAction();
    public EnemyDie_afterAction enemyDie_AfterAction;
    // Start is called before the first frame update

    
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        enemyDie_AfterAction += Enemydie_Additem;
        enemyDie_AfterAction += Menu.instance.Drop_Item_panel_Anim;
        // enemyDie_AfterAction += Enemydie_Additem;
    }


    public void Enemydie_Additem()
    {
        AddItemsTestScript.enemykill_Additems();
    }

    
    
    void Update()
    {
        
    }
}
