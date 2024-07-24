using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonController : MonoBehaviour
{
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
        SceneManager.LoadScene(sceneName);
    }
}
