using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public enum UITag
    {
        Null = -1,
        Explanation,
        PressKey,
        PressMouseKey,
        Result,
        WinResult,
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
    private GameObject mouseButtonUIObject = null;
    [SerializeField]
    private Image mouseButtonImage = null;
    [SerializeField]
    private List<Sprite> mouseSprite2DImage = new List<Sprite>();

    [SerializeField]
    private GameObject keyButtonUIObject = null;
    [SerializeField]
    private Image keyButtonImage = null;
    [SerializeField]
    private List<Sprite> keySprite2DImage = new List<Sprite>();

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
        winResultUITransform.anchoredPosition = new Vector2(0, 750);
        InitilaizeGameUISetting();

        baseExplanationPosition = explanationUITransform.transform.position;

        gameButtonController = GetComponent<GameButtonController>();
        gameButtonController.ActiveButton(false);
    }
    
    public void InitilaizeGameUISetting()
    {
        moveUIPos = new Vector2(0, 370);
        moveFlag = true;
    }

    public void MovePoasePanel(Vector2 movePos)
    {
        poasePanelTransform.anchoredPosition = Vector2.Lerp(poasePanelTransform.anchoredPosition, movePos, Time.deltaTime * 5f);
        Vector2 sub = poasePanelTransform.anchoredPosition - movePos;
        float dis = sub.magnitude;
        if (dis <= 0.1)
        {
            poasePanelTransform.anchoredPosition = movePos;
            
        }
    }

    public void MoveExplanationUI(Vector2 movePos)
    {
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
                keyButtonImage.sprite = keySprite2DImage[1];
                break;
            case KeyCode.S:
                keyButtonImage.sprite = keySprite2DImage[2];
                break;
            case KeyCode.D:
                keyButtonImage.sprite = keySprite2DImage[3];
                break;
        }
        keyButtonImage.enabled = true;
    }

    public void SetMouseButtonText(MouseCode code)
    {
        switch (code)
        {
            case MouseCode.Left:
                mouseButtonImage.sprite = mouseSprite2DImage[1];
                break;
            case MouseCode.Right:
                mouseButtonImage.sprite = mouseSprite2DImage[2];
                break;
            case MouseCode.Middle:
                mouseButtonImage.sprite = mouseSprite2DImage[3];
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
        moveUIPos = new Vector2(0, 750);
        keyButtonImage.sprite = keySprite2DImage[0];
        mouseButtonImage.sprite = mouseSprite2DImage[0];
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

    public void ChangeExplanationSprit(int _num)
    {
        explanationImage.sprite = explanationSprite2DImages[_num];
    }
}
