using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dragslot : MonoBehaviour
{
    public GameObject itembase;
    public Rect baseRect;
    public Item item;
    static public Dragslot instance;
    public Slot dragslot;
    [SerializeField]
    private Image imageitem;
    private void Start()
    {
        baseRect = itembase.GetComponent<RectTransform>().rect;
        instance = this;
    }
    // Start is called before the first frame update
    public void Dragsetimage(Image itmeimage)
    {
        imageitem.sprite = itmeimage.sprite;
        setcolor(1);
    }
    public void itemSet(Item item_)
    {
        item = item_;
    }
    public void setcolor(float alpha)
    {
        Color color = imageitem.color;
        color.a = alpha;
        imageitem.color = color;
    }

}
