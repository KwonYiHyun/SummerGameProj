using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillMinions : MonoBehaviour
{
    public GameObject MinionFactory;
    public GameObject setPosition;
    public GameObject Minion;
  
    public bool summoned = false;
    // Start is called before the first frame update
    void Start()
    {
        Minion = Instantiate(MinionFactory);
        Minion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)&&summoned == false)
        {
                Minion.SetActive(true);
                Minion.transform.position = setPosition.transform.position;
                summoned = true;

        }
        else if(Input.GetKeyDown(KeyCode.G) && summoned)
        {
            Minion.SetActive(false);
            summoned = false;
        }
       
    }
}
