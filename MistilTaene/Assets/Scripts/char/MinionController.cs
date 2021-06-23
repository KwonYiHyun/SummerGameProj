using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    //공격범위 레이로 판정내기
    Vector3 dir;
    public float speed = 20;
    public bool arrival = false;
    public bool isflip;
    public GameObject minion;
    public GameObject enemy;
    public float distance;
    public float a;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("testatk"); //적 찾기
        minion = GameObject.Find("gaeguri(Clone)"); //소환수 넣기
    }

    // Update is called once per frame
    void Update()
    {
        a += Time.deltaTime; //공격간격 4초
        if (a >= 4)
        {
            a = 4;
        }
      
        if (enemy.activeSelf == true)
        {
            
            distance = Vector3.Distance(minion.transform.position, enemy.transform.position); //소환수 적 거리 재기
        }
        if (distance <= 5&&a==4) //거리에 따라서 공격 하기
        {
            a = 0;
            enemyHp.Instance.Hpcont = enemyHp.Instance.Hpcont - 100;
        }
        isflip = GameObject.Find("char").GetComponent<PlayerMoving>().isflip; //좌우반전용
        if (arrival == false)
        {
            GameObject target = GameObject.Find("summonpos");
            dir = target.transform.position - transform.position;
            dir.Normalize();
            transform.position += dir * speed * Time.deltaTime;
        }
        if (isflip)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }else if (isflip == false)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("summonpos"))//소환위치 인가요
        {
            arrival = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("summonpos"))//소환위치 인가요
        {
            arrival =false;
        }
    }
}