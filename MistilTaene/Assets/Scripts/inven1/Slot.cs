using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public GameObject dropitem;
    public Vector3 chartrans;
    private Vector3 origpos;
    public Item item;
    public int itemCnt; //아이템갯수
    public Image itemImg; //이미지
    [SerializeField]
    private Text text_count;
    [SerializeField]
    private GameObject go_CountImage; // Inventory_Base 이미지의 Rect 정보 받아 옴.
    private void Start()
    {

        origpos = transform.position;
    }
    private void setColor(float _alpha)
    {
        Color color = itemImg.color;
        color.a = _alpha;
        itemImg.color = color;
    }
    public void Additem(Item _item, int _count = 1)
    {
        item = _item;
        itemCnt = _count;
        itemImg.sprite = item.itemimg;

        if (item.itemType != Item.itemtype.equip)
        {
            go_CountImage.SetActive(true);
            text_count.text = itemCnt.ToString();
        }
        else
        {
            text_count.text = itemCnt.ToString();
            go_CountImage.SetActive(false);
        }
        setColor(1);

    }
    public void Setslot(int _count)
    {
        itemCnt += _count;
        text_count.text = itemCnt.ToString();
        if (itemCnt <= 0)
            clearslot();
    }

    private void clearslot()
    {
        item = null;
        itemCnt = 0;
        Dragslot.instance.item = null;
        itemImg.sprite = null;
        setColor(0);
        text_count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                    if(item.itemType == Item.itemtype.equip)
                    {

                    }
                    else
                    {
                        Debug.Log(item.itemName);
                        Setslot(-1);
                    }
                
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            Dragslot.instance.dragslot = this;
            Dragslot.instance.Dragsetimage(itemImg);
            Dragslot.instance.itemSet(item);
            Dragslot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            Dragslot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Dragslot.instance.transform.localPosition.x < Dragslot.instance.baseRect.xMin
       || Dragslot.instance.transform.localPosition.x > Dragslot.instance.baseRect.xMax
       || Dragslot.instance.transform.localPosition.y < Dragslot.instance.baseRect.yMin
       || Dragslot.instance.transform.localPosition.y > Dragslot.instance.baseRect.yMax)
        {
            if (itemCnt == 1)
            {
                chartrans = PlayerMoving.Instance.transform.position;
                dropitem = Instantiate(Dragslot.instance.item.itemPrepab);
                dropitem.transform.position = chartrans;
            }
            else
            {
                Debug.Log("다중 아이템");
            }
            Dragslot.instance.dragslot.clearslot();
            Dragslot.instance.setcolor(0);
            Dragslot.instance.dragslot = null;
            
        }
        Dragslot.instance.setcolor(0);
        Dragslot.instance.dragslot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(Dragslot.instance.dragslot != null)
            changeslot();
    }
    private void changeslot()
    {
        Item _tempitem = item;
        int _tempitemcnt = itemCnt;
        Additem(Dragslot.instance.dragslot.item, Dragslot.instance.dragslot.itemCnt);
        if (_tempitem != null)
        {
            Dragslot.instance.dragslot.Additem(_tempitem, _tempitemcnt);
        }
        else
        {
            Dragslot.instance.dragslot.clearslot();
        }
    }
}
