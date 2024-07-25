using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField]
    private int nextScene;

    private void Update()
    {
        
    }

    public void GoScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
