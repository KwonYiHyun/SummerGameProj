using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tusamono : MonoBehaviour
{
    //투사체 레이로 거리 판정내기 
    Vector3 dir;
    public float speed = 5;
    public bool isflip;

    public GameObject enemy;
    public GameObject player;
    public float distance;
    public float distanceChar;
    public float a;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("testatk"); //적 찾기
        player = GameObject.Find("char"); //캐릭터 찾기
    }
    private void OnEnable()
    {
        
        isflip = GameObject.Find("char").GetComponent<PlayerMoving>().isflip; //캐릭터 방향전환
        if (isflip) //방향전환 따라서 바꾸기
        {
            transform.localScale = new Vector3(-5, 5, 5);
            dir = Vector3.left;
        }
        else if (isflip == false)
        {
            transform.localScale = new Vector3(5, 5, 5);
            dir = Vector3.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.activeSelf == true)
        {
            distance = Vector3.Distance(transform.position, enemy.transform.position); //적이랑 투사체 거리
        }
        distanceChar = Vector3.Distance(transform.position, player.transform.position); //캐릭터 투사체 거리

        if (distanceChar >= 20) //캐릭보다 20이상 멀면 없애기
        {
            gameObject.SetActive(false);
            PlayerSkillrensa player = GameObject.Find("char").
            GetComponent<PlayerSkillrensa>();
            player.tusaObjectPool.Add(gameObject);
        }
       
        transform.position += dir * speed * Time.deltaTime;
    }
}
