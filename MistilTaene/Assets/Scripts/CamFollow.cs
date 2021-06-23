using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class CamFollow : MonoBehaviour
{
	private float move; //움직임 확인

	public GameObject ch;  //캐릭터 불러오
	void Start()
	{
		
	}



	// Update is called once per frame()

	void FixedUpdate()
	{
		move = PlayerMoving.Instance.moveSpeed;
		if (move != 0)
		{
			Vector3 targetPos = new Vector3(ch.transform.position.x, ch.transform.position.y+1, -1);//쭉따라가게
			transform.position = Vector3.Lerp(transform.position, targetPos, move); //따라가기
        }
        else
        {
			Vector3 targetPos = new Vector3(ch.transform.position.x, ch.transform.position.y+1, -1);//쭉따라가게
			transform.position = Vector3.Lerp(transform.position, targetPos, 20); //따라가기
		}
	}

}
