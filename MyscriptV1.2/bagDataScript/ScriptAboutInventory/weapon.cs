using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/new weapon")]
public class Weapon : item
{
    public int weaponID;
    public int weaponLevel;
    bool canequid;
    
    public bool equided(int weaponID)
    {
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
