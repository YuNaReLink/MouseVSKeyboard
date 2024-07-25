using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Text startButtonText = null;

    [SerializeField]
    private float overTime = 0;

    [SerializeField]
    private float nextTime = 0;

    private float currentTime = 0;

    private float goSceneTime = 0; 

    private bool goSceneFlg = false;

    private NextScene nextScene = null;

    private bool active = true;

    private void Awake()
    {
        nextScene = GetComponent<NextScene>();

        startButtonText = GetComponent<Text>();

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Escape)|| Input.GetKey(KeyCode.Escape))
        {
            
        }          
        else if (Input.anyKeyDown)
        {
            goSceneFlg = true;
            active = false;
        }
        StartActive();
        SceneNext();
    }

    private void SceneNext()
    {
        if (goSceneFlg)
        {
            goSceneTime += Time.deltaTime;
            if(nextTime < goSceneTime)
            {
                nextScene.GoScene();
                goSceneTime = 0;
            }
        }

    }

    private void StartActive()
    {
        currentTime += Time.deltaTime;
        startButtonText.enabled = active;
        if (overTime < currentTime && !goSceneFlg)
        {
            active = !active;

            currentTime = 0;
        }
    }
}
