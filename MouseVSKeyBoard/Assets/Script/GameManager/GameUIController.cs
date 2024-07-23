using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject StartUI = null;
    public GameObject GetStartUI() {  return StartUI; }

    [SerializeField]
    private Text keyBoardText = null;
    // Start is called before the first frame update
    void Start()
    {
        if(StartUI != null)
        {
            StartUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKeyBoardText(KeyCode _key)
    {
        switch (_key)
        {
            case KeyCode.W:
                keyBoardText.text = "W";
                break;
            case KeyCode.A:
                keyBoardText.text = "A";
                break;
            case KeyCode.S:
                keyBoardText.text = "S";
                break;
            case KeyCode.D:
                keyBoardText.text = "D";
                break;
        }
    }
}
