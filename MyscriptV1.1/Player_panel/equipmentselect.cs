using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipmentselect : MonoBehaviour
{   
    public item selsectItem;  // Start is called before the first frame update
    
    public void equipmentthe_item()
    {       

            Player_panelManager.instance.equipmentItem(selsectItem);  
        
    }

}
