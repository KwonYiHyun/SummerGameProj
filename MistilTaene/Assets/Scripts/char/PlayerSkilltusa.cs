using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkilltusa : MonoBehaviour
{
    public GameObject tusaFactory;
    public GameObject tusaPosition;
    public GameObject tusa;
    public bool summoned = false;
    // Start is called before the first frame update
    void Start()
    {
        tusa = Instantiate(tusaFactory); //하나만 쏘기
        tusa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && summoned == false)
        {
            tusa.SetActive(true);
            tusa.transform.position = tusaPosition.transform.position;
            summoned = true;
        }

    }
}
