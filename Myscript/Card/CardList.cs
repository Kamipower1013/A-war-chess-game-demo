using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{

    public List<Card> Cardlist;
    

    public void Awake()
    {
        Cardlist = new List<Card>();
    }
    // Start is called before the first frame update
 
    
}
