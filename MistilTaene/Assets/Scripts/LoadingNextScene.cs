using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingNextScene : MonoBehaviour
{
    public int sceneNumber = 2;
    public Slider loadingbar;
    public Text loadingtext;
    public float load = 0;
    public float see;
    IEnumerator transitionNextScene(int num)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            load += Time.deltaTime;
            if (loadingbar.value < 1f)
            {
                loadingbar.value = Mathf.MoveTowards(loadingbar.value, 1f, Time.deltaTime);
                see = ao.progress / 0.9f;
                print(see);
                loadingtext.text = (ao.progress / 0.009f).ToString() + "%";
            }
            else
            {
                if (ao.progress >= 0.9f && load > 2)
                {
                    ao.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(transitionNextScene(sceneNumber));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
