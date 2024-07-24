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
    private int keyBoardVictoryCount = 0;
    public int GetKeyBoardVictoryCount() { return keyBoardVictoryCount; }
    private int mouseVictoryCount = 0;
    public int GetMouseVictoryCount() { return mouseVictoryCount; }
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
        mousePlayer.InitializePosition();

        keyBoardPlayer.InitializePosition();
        keyBoardPlayer.SetRandomKey();

        uIController.SetResultUI(new Vector2(0,2000));

        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                break;
            case GameManager.GameMode.BurstPush:
                SetMaxKeyAndMouseClickCount();
                break;
        }
        uIController.ChangeExplanationSprit((int)GameManager.GameModeTag);
    }

    void Update()
    {
        gameEventTimer.TimerUpdate();
        MoveUI();
        

        if (gameEventTimer.GetTimerGameStartWait().IsEnabled()) { return; }
        if(GameManager.GameStateTag == GameManager.GameState.Result) { return; }

        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                if (!rapidPress)
                {
                    SetRandomNumber();
                }
                break;
            case GameManager.GameMode.BurstPush:
                rapidPress = true;
                break;
        }
        if (gameEventTimer.GetTimerResetGameIdle().IsEnabled()) { return; }
        ResultText();
        
    }

    private void MoveUI()
    {
        uIController.MoveExplanationUI(uIController.MoveUIPos);
        if(GameManager.GameStateTag == GameManager.GameState.Result)
        {
            uIController.MoveWinResultUI(new Vector2(0, 370));
        }
    }

    private void SetRandomNumber()
    {
        measurementNumber = Random.Range(0, 100);
        if(measurementNumber < baseMeasurementValue)
        {
            rapidPress = true;
        }
    }

    private void SetMaxKeyAndMouseClickCount()
    {
        maxCount = Random.Range(10, 20);
        
    }

    private void ResultText()
    {
        if(victoryPlayer == VictoryPlayer.Null) { return; }
        DrawResult();

        switch (victoryPlayer)
        {
            case VictoryPlayer.KeyBoard:
                uIController.SetResultUI(new Vector2(-550, 200));
                uIController.ResultUI(victoryPlayer);
                keyBoardVictoryCount++;
                break;
            case VictoryPlayer.Mouse:
                uIController.SetResultUI(new Vector2(550, 200));
                uIController.ResultUI(victoryPlayer);
                mouseVictoryCount++;
                break;
            case VictoryPlayer.Draw:
                uIController.ResultUI(victoryPlayer);
                break;
        }
        uIController.VictoryCountText(keyBoardVictoryCount,mouseVictoryCount);
        gameEventTimer.GetTimerResetGameIdle().StartTimer(5f);
        gameEventTimer.GetTimerResetGameIdle().OnCompleted += () =>
        {
            if (WinningResults()) { return; }
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

    private bool WinningResults()
    {
        bool noWin = keyBoardVictoryCount != 3 && mouseVictoryCount != 3;
        if (noWin) { return false; }
        GameManager.GameStateTag = GameManager.GameState.Result;
        if (keyBoardVictoryCount >= 3)
        {
            victoryPlayer = VictoryPlayer.KeyBoard;
        }
        else if (mouseVictoryCount >= 3)
        {
            victoryPlayer = VictoryPlayer.Mouse;
        }
        uIController.WinResultUI(victoryPlayer);
        return true;
    }
}
