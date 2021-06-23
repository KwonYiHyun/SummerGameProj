using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyHp : MonoBehaviour
{
    public static enemyHp Instance = null;
    public int EnemyHp = 100;
    public Text EnemyHpUI;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        EnemyHpUI.text = "체력 :" + EnemyHp;
    }
    public void Update()
    {

    }
    public int Hpcont
    {
        get
        {
            return EnemyHp;
        }
        set
        {
            EnemyHp = value;
            if (EnemyHp == 0)
            {
                EnemyHp = 101;
                EnemyScript.instance.die();
            }
            EnemyHpUI.text = "체력 :" + EnemyHp;

        }
    }
}
