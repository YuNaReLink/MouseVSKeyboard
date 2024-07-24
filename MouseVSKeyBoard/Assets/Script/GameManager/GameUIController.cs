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
    private GameObject ExplanationUI = null;
    public GameObject GetExplanationUI() {  return ExplanationUI; }

    [SerializeField]
    private Text explanationText = null;

    [SerializeField]
    private Text mouseText = null;

    [SerializeField]
    private GameObject keyButtonUIObject = null;
    [SerializeField]
    private Image keyButtonImage = null;
    [SerializeField]
    private List<Sprite> sprite2DImage = new List<Sprite>();

    [SerializeField]
    private Text resultText = null;

    [SerializeField]
    private Text victoryCountMouseText = null;

    [SerializeField]
    private Text victoryCountKeyBoardText = null;

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
                keyButtonImage.sprite = sprite2DImage[0];
                break;
            case KeyCode.S:
                keyButtonImage.sprite = sprite2DImage[1];
                break;
            case KeyCode.D:
                keyButtonImage.sprite = sprite2DImage[2];
                break;
        }
        keyButtonImage.enabled = true;
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
                result = "青の勝利!";
                break;
            case VictoryPlayer.Mouse:
                result = "赤の勝利!";
                break;
            case VictoryPlayer.Draw:
                result = "引き分け！";
                break;
        }
        resultText.text = result;
        uiArray[(int)UITag.Start].SetActive(false);
        keyButtonImage.enabled = false;
        mouseText.text = "";
    }

    public void VictoryCountText(int _keyNum,int _mouseNum) {
        victoryCountKeyBoardText.text = "WIN : " + _keyNum.ToString();
        victoryCountMouseText.text = "WIN : " +  _mouseNum.ToString();
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
        explanationText.text = _text;
    }

    public void WinResultUI(VictoryPlayer player)
    {
        string result = null;
        if(player == VictoryPlayer.KeyBoard)
        {
            result = "キーボードが栄光を掴んだ!";
        }
        else if(player == VictoryPlayer.Mouse)
        {
            result = "マウスが栄光を掴んだ!";
        }
        resultText.text = result;
    }
}
