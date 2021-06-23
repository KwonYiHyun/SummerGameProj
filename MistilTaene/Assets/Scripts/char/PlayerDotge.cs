using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDotge : MonoBehaviour
{
    //addforce 
    public bool isdotge = false;
    public float dotgeRange;
    Rigidbody2D rigid;
    public float dotgeTime;
    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isdotge)
        {
            isdotge = true;
            if (isdotge)
            {
                PlayerMoving.Instance.canmove = false;

            }

            rigid.velocity = Vector2.zero;
            Vector2 force;
            if (PlayerMoving.Instance.isflip)
            {
                force = new Vector2(-dotgeRange, 0);
            }
            else
            {
                force = new Vector2(dotgeRange, 0);
            }
            rigid.AddForce(force, ForceMode2D.Impulse);
            dotgeTime = 0.5f;

        }
        if (isdotge)
        {
            if (dotgeTime > 0)
                dotgeTime -= Time.deltaTime;//타이머 작동
            if (dotgeTime <= 0)
            {
               
                rigid.velocity = Vector2.zero;
                PlayerMoving.Instance.canmove = true;
                isdotge = false;
            }
        }
    }
}
