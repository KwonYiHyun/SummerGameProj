using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public static bool inventoryactive = false;
    
    [SerializeField]
    
    private GameObject go_inventoryBase;

    [SerializeField]

    private GameObject go_slotparents;

    private Slot[] slots;


    // Start is called before the first frame update
    void Start()
    {
        slots = go_slotparents.GetComponentsInChildren<Slot>();

    }

    // Update is called once per frame
    void Update()
    {
        go_openinventory();
    }
    private void go_openinventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryactive = !inventoryactive;
            if (inventoryactive)
                openinventory();
            else
                closeinvnetory(); 
        }
    }

    private void openinventory()
    {
        go_inventoryBase.SetActive(true);

    }

    private void closeinvnetory()
    {
        go_inventoryBase.SetActive(false);
    }
    public void Acquireitem(Item _item, int count = 1)
    {
        if (Item.itemtype.equip != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {

                if (slots[i].item != null)
                {

                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].Setslot(count);
                        return;
                    }
                }

            }
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].Additem(_item, count);
                    return;
                }

            }
        }
    }
}
