using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonController : MonoBehaviour
{
    [SerializeField]
    private float time;
    [SerializeField]
    private float overTime;
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


    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
