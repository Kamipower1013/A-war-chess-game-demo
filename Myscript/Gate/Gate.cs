using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public MapListNode GatePoint;
    public string playerTag = "Player";
    public GameObject Player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag) == true)
        {   

            Player = other.gameObject;
            Player.GetComponent<playerMovement>().IsEnter_Gate = true;

            Invoke("Enter_Gate_Move_Posion", 0.05f);
            
        }
       

    }

   public void Enter_Gate_Move_Posion()
    {
        Player.GetComponent<playerMovement>().Enter_Gate_Move(GatePoint.TheContectGate().The_transLate_Point());

    }



    public void Turn_Cricle()
    {
        Quaternion q = Quaternion.AngleAxis(10 * Time.deltaTime,transform.up);
        transform.rotation = q * transform.rotation;
    }


}
