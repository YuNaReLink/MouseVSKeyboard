using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class GameUIController : MonoBehaviour
{
    public enum UITag
    {
        Null = -1,
        Explanation,
        Go,
        PressKey,
        PressMouseKey,
        Result,
        WinResult,
        KeyScore,
        MouseScore,
        Poase,
        Retry,
        Return,
        DataEnd
    }
    [SerializeField]
    private List<GameObject> uiArray = new List<GameObject>();

    [SerializeField]
    private RectTransform explanationUITransform = null;
    [SerializeField]
    private Image explanationImage = null;
    [SerializeField]
    private List<Sprite> explanationSprite2DImages = new List<Sprite>();

    [SerializeField]
    private bool moveFlag = false;
    [SerializeField]
    private Vector2 baseExplanationPosition = Vector2.zero;
    [SerializeField]
    private Vector2 moveUIPos = Vector2.zero;
    public Vector2 MoveUIPos { get { return moveUIPos; } set { moveUIPos = value; } }

    [SerializeField]
    private MouseTexture mouseTexture = null;
    public MouseTexture GetMouseTexture() { return mouseTexture; }

    [SerializeField]
    private KeyBoardTexture keyBoardTexture = null;
    public KeyBoardTexture GetKeyBoardTexture() { return keyBoardTexture; }

    [SerializeField]
    private RectTransform resultUITransform = null;
    [SerializeField]
    private Image resultImage = null;
    [SerializeField]
    private List<Sprite> resultImageArray = new List<Sprite>();

    [SerializeField]
    private RectTransform winResultUITransform = null;
    [SerializeField]
    private Text winResultUIText = null;

    [SerializeField]
    private WinScore mouseScore = null;

    [SerializeField]
    private WinScore keyBoardScore = null;

    [SerializeField]
    private RectTransform poasePanelTransform = null;

    [SerializeField]
    private GameButtonController gameButtonController = null;
    private void Awake()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            uiArray.Add(transform.GetChild(i).gameObject);
        }
        winResultUIText = winResultUITransform.GetComponentInChildren<Text>();
        winResultUITransform.anchoredPosition = 
            MoveUIPositionData[(int)MoveUIPositionTag.ScreenOut];
        InitilaizeGameUISetting();

        baseExplanationPosition = explanationUITransform.transform.position;

        gameButtonController = GetComponent<GameButtonController>();
        gameButtonController.ActiveButton(false);

        mouseTexture = GetComponentInChildren<MouseTexture>();
        mouseTexture.Initialize();

        keyBoardTexture = GetComponentInChildren<KeyBoardTexture>();
        keyBoardTexture.Initialize();
    }
    
    public void InitilaizeGameUISetting()
    {
        moveUIPos = 
            MoveUIPositionData[(int)MoveUIPositionTag.ScreenIn];
        
    }

    public void MovePoasePanel(Vector2 movePos)
    {
        poasePanelTransform.anchoredPosition = Vector2.Lerp(poasePanelTransform.anchoredPosition, movePos, Time.deltaTime * 5f);
        Vector2 sub = poasePanelTransform.anchoredPosition - movePos;
        float dis = sub.magnitude;
        if (dis <= 0.1)
        {
            poasePanelTransform.anchoredPosition = movePos;
            moveFlag = true;
        }
    }

    public void MoveExplanationUI(Vector2 movePos)
    {
        if (!moveFlag) { return; }
        explanationUITransform.anchoredPosition = Vector2.Lerp(explanationUITransform.anchoredPosition,movePos,Time.deltaTime * 5f);
        Vector2 sub = explanationUITransform.anchoredPosition- movePos;
        float dis = sub.magnitude;
        if(dis <= 0.1)
        {
            explanationUITransform.anchoredPosition = movePos;
            moveFlag = false;
        }
    }

    public void MoveWinResultUI(Vector2 movePos)
    {
        winResultUITransform.anchoredPosition = Vector2.Lerp(winResultUITransform.anchoredPosition, movePos, Time.deltaTime * 5f);
        Vector2 sub = winResultUITransform.anchoredPosition - movePos;
        float dis = sub.magnitude;
        if (dis <= 0.1)
        {
            winResultUITransform.anchoredPosition = movePos;
            gameButtonController.ActiveButton(true);
        }
    }

    public void SetResultUI(VictoryPlayer _player,Vector2 pos)
    {
        if(_player == VictoryPlayer.Draw)
        {
            resultImage.sprite = resultImageArray[1];
        }
        else
        {
            resultImage.sprite = resultImageArray[0];
        }
        resultUITransform.anchoredPosition = pos;
    }

    public void SetKeyBoardText(KeyCode _key)
    {
        switch (_key)
        {
            case KeyCode.A:
                keyBoardTexture.ChangeTexture(1);
                break;
            case KeyCode.S:
                keyBoardTexture.ChangeTexture(2);
                break;
            case KeyCode.D:
                keyBoardTexture.ChangeTexture(3);
                break;
        }
    }

    public void SetMouseButtonUI(MouseCode code)
    {
        switch (code)
        {
            case MouseCode.Left:
                mouseTexture.ChangeTexture(1);
                break;
            case MouseCode.Right:
                mouseTexture.ChangeTexture(2);
                break;
            case MouseCode.Middle:
                mouseTexture.ChangeTexture(3);
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
        moveUIPos = 
            MoveUIPositionData[(int)MoveUIPositionTag.ScreenOut];
        keyBoardTexture.ChangeTexture(0);
        mouseTexture.ChangeTexture(0);
    }

    public void VictoryCountText(VictoryPlayer _player,int _keyNum,int _mouseNum) {
        switch (_player)
        {
            case VictoryPlayer.KeyBoard:
                keyBoardScore.ViewWinScore(_player);
                break;
            case VictoryPlayer.Mouse:
                mouseScore.ViewWinScore(_player);
                break;
            case VictoryPlayer.Draw:
                break;
        }
    }

    private void SetAllActiveUI(bool _enabled)
    {
        for (int i = 0; i < uiArray.Count; i++)
        {
            uiArray[i].SetActive(_enabled);
        }
    }

    public void WinResultUI(VictoryPlayer player)
    {
        string result = null;
        if(player == VictoryPlayer.KeyBoard)
        {
            result = "キーボードの勝利だ！";
        }
        else if(player == VictoryPlayer.Mouse)
        {
            result = "マウスの勝利だ!";
        }
        winResultUIText.text = result;
    }

    public void ActiveUIObject(int _num,bool _enabled)
    {
        uiArray[_num].SetActive(_enabled);
    }

    public void ChangeExplanationSprit(int _num)
    {
        explanationImage.sprite = explanationSprite2DImages[_num];
    }
}
