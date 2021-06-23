using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemGetController : MonoBehaviour
{
    public bool istrig;
    [SerializeField]
    private float range;

    private bool pickupActive = false;

    private RaycastHit hitinfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Text GetText;
    // Start is called before the first frame update
    [SerializeField]
    private inventory theinventory;
    // Update is called once per frame
    void Update()
    {
        Checkitem();
        TryAction();
    }

    private void Checkitem()
    {

        if (PlayerMoving.Instance.isflip)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * range, Color.red);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.left), out hitinfo, range, layerMask))
            {
                if (hitinfo.transform.tag == "ITem")
                {
                    iteminfoAppear();
                }
            }
            else
            {
                disappearinfo();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * range, Color.red);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.right), out hitinfo, range, layerMask))
            {
                if (hitinfo.transform.tag == "ITem")
                {
                    iteminfoAppear();
                }
            }
            else
            {
                disappearinfo();
            }
        }
    
    }

    private void disappearinfo()
    {
        pickupActive = false;
        GetText.gameObject.SetActive(false);
      
    }

    private void iteminfoAppear()
    {
        pickupActive = true;
        GetText.gameObject.SetActive(true);
        GetText.text = hitinfo.transform.GetComponent<ItemPickup>().item.itemName + " E를 눌러 획득";
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Checkitem();
            Canpickup();
        }
    }

    private void Canpickup()
    {
        if (pickupActive)
        {
            if (hitinfo.transform != null)
            {
                Debug.Log("얻음");
                theinventory.Acquireitem(hitinfo.transform.GetComponent<ItemPickup>().item);
                Destroy(hitinfo.transform.gameObject);
                disappearinfo();
            }
        }
    }
}
