using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AllTheCardList", menuName = "CardSystem/AllTheCardList")]
[System.Serializable]
public class AlltheCard : ScriptableObject
{
    public List<Card> AllCardList = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {

    }
    public AlltheCard(){

        for(int i = 0; i < 50; i++)
        {
            AllCardList.Add(null);
        }


    }
  


   
}
