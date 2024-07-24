using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public enum UITag
    {
        Null = -1,
        Start,
        PressKey,
        PressMouseKey,
        Result,
        DataEnd
    }
    [SerializeField]
    private List<GameObject> uiArray = new List<GameObject>();
    [SerializeField]
    private GameObject StartUI = null;
    public GameObject GetStartUI() {  return StartUI; }

    [SerializeField]
    private Text startText = null;

    [SerializeField]
    private Text keyBoardText = null;

    [SerializeField]
    private GameObject keyA;
    
    [SerializeField]
    private GameObject keyS;

    [SerializeField]
    private GameObject keyD;

    [SerializeField]
    private Text resultText = null;

    [SerializeField]
    private Text victoryCountMouseText = null;

    [SerializeField]
    private Text victoryCountKeyBoardText = null;

    public int keyBoardVictoryCount = 0;

    public int mouseVictoryCount = 0;

    private bool ofLoop = false;

    private void Awake()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            uiArray.Add(transform.GetChild(i).gameObject);
        }


        InitilaizeGameUISetting();
    }
    
    public void InitilaizeGameUISetting()
    {
        if (uiArray[(int)UITag.Start] != null)
        {
            uiArray[(int)UITag.Start].SetActive(false);
        }
        
        resultText.text = "";
    }

    public void SetKeyBoardText(KeyCode _key)
    {
        switch (_key)
        {
            case KeyCode.A:
                keyA.SetActive(true);
                keyBoardText.text = "A";
                break;
            case KeyCode.S:
                keyS.SetActive(true);
                keyBoardText.text = "S";
                break;
            case KeyCode.D:
                keyD.SetActive(true);
                keyBoardText.text = "D";
                break;
        }
    }

    public void SetMouseButtonText(MouseCode code)
    {
        switch (code)
        {
            case MouseCode.Left:
                mouseText.text = "左クリック！";
                break;
            case MouseCode.Right:
                mouseText.text = "右クリック！";
                break;
            case MouseCode.Middle:
                mouseText.text = "ホイールクリック！";
                break;
        }
    }

    public void ResultUI(VictoryPlayer _player)
    {
        
        string result = "";
        
        switch (_player)
        {
            case VictoryPlayer.KeyBoard:
                result = "キーボードの勝利!";
                SetActiveOff();
                break;
            case VictoryPlayer.Mouse:
                result = "マウスの勝利!";
                break;
            case VictoryPlayer.Draw:
                result = "引き分け！";
                break;
        }
        if (!ofLoop)
        {
            if(result == "キーボードの勝利!")
            {
                keyBoardVictoryCount++;
            }
            else if(result == "マウスの勝利!")
            {
                mouseVictoryCount++;
            }
            ofLoop = true;
        }
        resultText.text = result;
        uiArray[(int)UITag.Start].SetActive(false);
        keyBoardText.text = "";
        mouseText.text = "";
    }

    public void VictoryCountText() {
        victoryCountKeyBoardText.text = "WIN : " + keyBoardVictoryCount.ToString();
        victoryCountMouseText.text = "WIN : " +  mouseVictoryCount.ToString();
    }

    private void SetActiveOff()
    {
        keyA.SetActive(false);
        keyS.SetActive(false);
        keyD.SetActive(false);
    }

    private void SetAllActiveUI(bool _enabled)
    {
        for (int i = 0; i < uiArray.Count; i++)
        {
            uiArray[i].SetActive(_enabled);
        }
    }

    public void SetStartText(string _text)
    {
        startText.text = _text;
    }
}
