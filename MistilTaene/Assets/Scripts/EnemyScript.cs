using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemy;
    public static EnemyScript instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    public void die()
    {
        //ItemDatabase.instance.itemdrop(transform.position);
        enemy.SetActive(false);   
       
    }
}
