using System.Collections;
using System.Collections.Generic;
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
    private float currentTime = 0;

    private NextScene nextScene = null;

    bool active = true;

    private void Awake()
    {
        nextScene = GetComponent<NextScene>();

        startButtonText = GetComponent<Text>();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            nextScene.GoScene();
        }

        currentTime += Time.deltaTime;
        if(overTime < currentTime)
        {
            active = !active;

            startButtonText.enabled = active;
            currentTime = 0;
        }

    }
}
