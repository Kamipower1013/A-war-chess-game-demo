using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class itemOnDrug : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
    public Transform originalParent;
    public Transform Concent;
    public Transform Canavs;
    public Inventory mybag;
    public GameObject Player;
    public UnityEvent rightClick;
    public GameObject Equipment_Panel_father;
    public GameObject Equipment_Panel;


    [SerializeField]private int currentItemID;//当前item的ID；也是mybag里面的item的ID！！！！！重点
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = transform.parent.GetComponent<Slot>().slotID;//格子的id
        transform.SetParent(transform.parent.parent.parent.parent);
        //transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;//图片会当到获取格子
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
         if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("进入右键事件监测");
            rightClick.Invoke();
        }
           
    }

    public void open_Equipmentpanel()
    {
        Debug.Log("激活版面");
        item thisitem = transform.parent.GetComponent<Slot>().slotItem;
        Equipment_Panel.SetActive(true);
        Equipment_Panel.GetComponent<equipmentselect>().selsectItem = thisitem;
        Equipment_Panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
      //  Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject!= null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //
                var tempItem = mybag.items[currentItemID];//mybag里和现在这个格子编号一致的那个item；
                mybag.items[currentItemID] = mybag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                mybag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = tempItem;
                //在refresh时，items[]和slots[]对应了
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.refreshItem();
                return;
            }
            //如果是空格子
            else if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).SetParent(originalParent);
                //bag交换
                Debug.Log("bug处currid" + currentItemID);
                var tempItem1 = mybag.items[currentItemID];
                mybag.items[currentItemID] = mybag.items[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID];
                mybag.items[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = tempItem1;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryManager.refreshItem();
                return;
            }
        }
        //拖到其他位置都归位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
       
    }

    


    // Start is called before the first frame update
    void Start()
    {
        //Canavs = GameObject.FindGameObjectWithTag("Canvas").transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        mybag = Player.GetComponent<PlayerCharactor>().current_bag;
        Equipment_Panel_father = GameObject.Find("Canvas");
        Equipment_Panel = Equipment_Panel_father.transform.Find("Equipment").gameObject;
        rightClick.AddListener(new UnityAction(open_Equipmentpanel));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
