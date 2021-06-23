using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WirteName : MonoBehaviour
{
    public InputField Name;
    public static string fieldText;
    public void SaveName()
    {

      
    }
    void Update()
    {
        fieldText = Name.text;
        //print(fieldText);
    }
    public void str()
    {
        //if (Name.text !="")
        //{
        SceneManager.LoadScene(1);
        //}
    }
}
