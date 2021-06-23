using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public itemtype itemType;
    public Sprite itemimg;
    public GameObject itemPrepab;
    public enum itemtype
    {
        equip,
        used,
        etc
    }
}
