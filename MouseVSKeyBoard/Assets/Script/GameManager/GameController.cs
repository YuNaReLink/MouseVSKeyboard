using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VictoryPlayer
{
    Null = -1,
    KeyBoard,
    Mouse,
    Draw,
    DataEnd
}

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameUIController uIController = null;

    [SerializeField]
    private const int baseMeasurementValue = 1;
    [SerializeField]
    private int measurementNumber = 0;

    [SerializeField]
    private static bool rapidPress = false;

    public static bool IsRapidPressFlag() {  return rapidPress; }

    [SerializeField]
    private int maxCount = 10;
    public int GetMaxCount() {  return maxCount; }
    [SerializeField]
    private int keyCount = 0;
    public int KeyCount {  get { return keyCount; } set { keyCount = value; } }
    [SerializeField]
    private int clickCount = 0;
    public int ClickCount { get {return clickCount; } set { clickCount = value; } }

    [SerializeField]
    private KeyBoardPlayer keyBoardPlayer = null;
    [SerializeField]
    private MousePlayer mousePlayer = null;

    private VictoryPlayer victoryPlayer = VictoryPlayer.Null;
    public VictoryPlayer VictoryPlayer { get { return victoryPlayer; } set { victoryPlayer = value; } }


    private GameEventTimer gameEventTimer = null;
    private void Start()
    {
        Initialize();

        InitializeGameSetting();
    }

    private void Initialize()
    {
        uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<GameUIController>();
        keyBoardPlayer = GameObject.FindGameObjectWithTag("KeyBoardPlayer").GetComponent<KeyBoardPlayer>();
        mousePlayer = GameObject.FindGameObjectWithTag("MousePlayer").GetComponent<MousePlayer>();

        gameEventTimer = new GameEventTimer();
        gameEventTimer.InitializeAssignTimer();
    }

    private void InitializeGameSetting()
    {
        int modeValue = Random.Range(0, (int)GameManager.GameMode.DataEnd);
        GameManager.GameModeTag = (GameManager.GameMode)modeValue;

        gameEventTimer.GetTimerGameStartWait().StartTimer(2f);

        victoryPlayer = VictoryPlayer.Null;
        keyCount = 0;
        clickCount = 0;

        rapidPress = false;
        mousePlayer.GetMagicShot().Fire = false;
        keyBoardPlayer.GetMagicShot().Fire = false;

        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.BurstPush:
                SetMaxKeyAndMouseClickCount();
                break;
        }
    }

    void Update()
    {
        gameEventTimer.TimerUpdate();
        if (gameEventTimer.GetTimerGameStartWait().IsEnabled()) { return; }

        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                if (!rapidPress)
                {
                    SetRandomNumber();
                }
                break;
            case GameManager.GameMode.BurstPush:
                uIController.GetStartUI().SetActive(true);
                rapidPress = true;
                break;
        }
        if (gameEventTimer.GetTimerResetGameIdle().IsEnabled()) { return; }
        ResultText();
        
    }

    private void SetRandomNumber()
    {
        measurementNumber = Random.Range(0, 100);
        if(measurementNumber < baseMeasurementValue)
        {
            uIController.GetStartUI().SetActive(true);
            uIController.SetStartText("‰Ÿ‚¹I");
            rapidPress = true;
        }
    }

    private void SetMaxKeyAndMouseClickCount()
    {
        maxCount = Random.Range(10, 20);
        uIController.SetStartText("˜A‘Å‚µ‚ëI");
        
    }

    private void ResultText()
    {
        if(victoryPlayer == VictoryPlayer.Null) { return; }
        DrawResult();

        switch (victoryPlayer)
        {
            case VictoryPlayer.KeyBoard:
                uIController.ResultUI(victoryPlayer);
                break;
            case VictoryPlayer.Mouse:
                uIController.ResultUI(victoryPlayer);
                break;
            case VictoryPlayer.Draw:
                uIController.ResultUI(victoryPlayer);
                break;
        }
        uIController.VictoryCountText();
        gameEventTimer.GetTimerResetGameIdle().StartTimer(5f);
        gameEventTimer.GetTimerResetGameIdle().OnCompleted += () =>
        {
            InitializeGameSetting();
            uIController.InitilaizeGameUISetting();
        };
    }

    private void DrawResult()
    {
        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                if (keyBoardPlayer.GetInputController().RandomGetKey() && Input.GetMouseButtonDown(0))
                {
                    victoryPlayer = VictoryPlayer.Draw;
                }
                break;
            case GameManager.GameMode.BurstPush:
                if(victoryPlayer == VictoryPlayer.Null) { return; }
                if(keyCount == clickCount)
                {
                    victoryPlayer = VictoryPlayer.Draw;
                }
                break;
        }
    }

    public void SetViewPushKey(KeyCode _key)
    {
        uIController.SetKeyBoardText(_key);
    }
    public void SetViewPushMouseButton(MouseCode code)
    {
        uIController.SetMouseButtonText(code);
    }
}
