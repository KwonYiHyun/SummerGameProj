using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillrensa : MonoBehaviour
{
    public GameObject tusaFactory;
    public GameObject tusaPosition;
    public int poolSize = 5;
    public List<GameObject> tusaObjectPool;

    // Start is called before the first frame update
    void Start()
    {
        tusaObjectPool = new List<GameObject>(); //오브젝트풀에 넣기
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tusa = Instantiate(tusaFactory);
            tusaObjectPool.Add(tusa);
            tusa.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z)) //오브젝트 연사하기
        {
            if (tusaObjectPool.Count > 0)
            {
                GameObject tusa = tusaObjectPool[0];
                tusa.SetActive(true);
                tusaObjectPool.Remove(tusa);
                tusa.transform.position = tusaPosition.transform.position;
            }

        }
    }
}
