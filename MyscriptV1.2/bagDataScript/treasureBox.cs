using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasureBox : MonoBehaviour
{   
    public item[] AllItem;


    public Inventory playerInventory;
    private string playerTag = "Player";
    public GameObject Player;

    public int TreasureBox_Num;

    public GameObject Canvas_Father;

    public SkinnedMeshRenderer part1;
    public SkinnedMeshRenderer part2;
    public Material thisMat;
    public Shader thisenemyShader;

    public float shader_dissolve_value;
    public float victory;
    void Start()
    {
    }

    public void Material_Set_All()
    {
        part1.material = thisMat;
        part2.material = thisMat;
        shader_dissolve_value = thisMat.GetFloat("Dissvion_Value");
        thisMat.SetFloat("Dissvion_Value", -1f);
    }

    public void test(float value)
    {
        part1.material.SetFloat("Dissvion_Value", value);
        part2.material.SetFloat("Dissvion_Value", value);
        //thisMat.SetFloat("Dissvion_Value", shader_dissolve_value);
    }

    public void Start_corotine_ThisTreasureBox_shaderChange_dead()
    {

        StartCoroutine(TreasureBox_shaderChange_disappear_Coro());

    }

    public IEnumerator TreasureBox_shaderChange_disappear_Coro()
    {
       
        shader_dissolve_value = thisMat.GetFloat("Dissvion_Value");
        
        while (shader_dissolve_value < 1f)
        {
            shader_dissolve_value = thisMat.GetFloat("Dissvion_Value");

            shader_dissolve_value = Mathf.SmoothDamp(shader_dissolve_value, 1f, ref victory, 1.8f, 0.5f);

            thisMat.SetFloat("Dissvion_Value", shader_dissolve_value);
            //Dead_Particle.gameObject.transform.position = Root.position;
            yield return new WaitForSeconds(0);

        }
        // Debug.Log("退出协程");
    }


    public void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag(playerTag) == true)
        {
            Debug.Log("进入了Collider组件,OnCollisionStay");
        }
    }

    public void disappear()
    {
        transform.gameObject.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)//写到PlayerCharactor上
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = Player.GetComponent<PlayerCharactor>().current_bag;
        if (other.gameObject.CompareTag(playerTag) == true)
        {
            Debug.Log("进入了Collider组件OnTriggerEnter");
            Start_corotine_ThisTreasureBox_shaderChange_dead();
            int RadNum = Random.Range(1, 6);
            AddnewItem(AllItem[RadNum]);
            Menu.instance.Drop_Item_Treasure_Panel_Anim();
        }
        Invoke("disappear", 4f);
        //if (other.gameObject.CompareTag(playerTag) == true)
        //{
        //    int whichItem = Random.Range(0, 5);
        //    AddnewItem(AllItem[whichItem]);
        //  //  Destroy(gameObject);
        //}
    }



    public void AddnewItem(item newitem)
    {
        if (!playerInventory.items.Contains(newitem))
        {
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                if (playerInventory.items[i] == null)
                {
                    playerInventory.items[i] = newitem;
                    break;
                }
            }

        }
        else if (playerInventory.items.Contains(newitem))
        {
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                if (playerInventory.items[i] == newitem)
                {
                    playerInventory.items[i].Data.itemHeld += 1;

                }
            }

            //int index=playerInventory.items.FindIndex((item a)=>a.Equals(newitem));
            //Debug.Log("findindex查找" + index);
            //playerInventory.items[index].itemHeld += 1;

        }
        InventoryManager.refreshItem();


    }


   


}


  

