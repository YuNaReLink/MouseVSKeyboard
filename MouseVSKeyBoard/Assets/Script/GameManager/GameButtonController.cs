using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonController : MonoBehaviour
{
    [SerializeField]
    private float time;
    [SerializeField]
    private float overTime = 1f;
    [SerializeField]
    private bool sceneChange = false;
    [SerializeField]
    private string sceneName = "";
    public enum ButtonTag
    {
        Null = -1,
        Retry,
        Return,
        DataEnd
    }
    [SerializeField]
    private List<GameObject> buttonArray = new List<GameObject>();

    public void ActiveButton(bool enabled)
    {
        buttonArray[(int)ButtonTag.Retry].SetActive(enabled);
        buttonArray[(int)ButtonTag.Return].SetActive(enabled);
    }

    private void Start()
    {
        sceneChange = false;
        time = 0;
        overTime = 1f;
    }

    private void Update()
    {
        if (sceneChange)
        {
            time += Time.deltaTime;
            if(time > overTime)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    public void ChangeScene(string _sceneName)
    {
        sceneChange = true;
        Time.timeScale = 1f;
        sceneName = _sceneName;
    }
}
