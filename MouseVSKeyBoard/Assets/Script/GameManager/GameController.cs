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
    private static bool inputEnabled = false;
    public static bool IsRapidPressFlag() {  return inputEnabled; }

    [SerializeField]
    private static bool preempt = false;
    public static bool Preempt {  get { return preempt; } set { preempt = value; } }
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
    public GameEventTimer GetGameEventTimer() { return gameEventTimer; }

    private bool poaseFlag = true;

    [SerializeField]
    private GameSceneSE gameSE = null;
    private int soundCount = 0;
    private int roundCount = 0;
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

        gameSE = GetComponent<GameSceneSE>();
    }

    private void InitializeGameSetting()
    {
        //現在の状態を設定
        GameManager.GameStateTag = GameManager.GameState.Game;
        //ゲームモードをランダムに設定
        SetGameMode();
        //ゲームが開始するまでのタイマーを設定
        gameEventTimer.GetTimerGameStartWait().StartTimer(2f);
        //初期化
        victoryPlayer = VictoryPlayer.Null;
        keyCount = 0;
        clickCount = 0;

        inputEnabled = false;
        preempt = false;
        
        mousePlayer.InitializePosition();
        keyBoardPlayer.InitializePosition();

        uIController.SetResultUI(victoryPlayer,new Vector2(0,2000));

        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                keyBoardPlayer.SetRandomKey(GameManager.GameModeTag);
                mousePlayer.SetRandomButton(GameManager.GameModeTag);
                break;
            case GameManager.GameMode.BurstPush:
                SetMaxKeyAndMouseClickCount();
                break;
            case GameManager.GameMode.MutualPush:
                keyBoardPlayer.SetRandomKey(GameManager.GameModeTag);
                mousePlayer.SetRandomButton(GameManager.GameModeTag);
                SetStartButton();
                break;
        }
        uIController.ChangeExplanationSprit((int)GameManager.GameModeTag);
    }
    private void SetGameMode()
    {
        int modeValue = Random.Range(0, (int)GameManager.GameMode.DataEnd);
        if(GameManager.GameModeTag == (GameManager.GameMode)modeValue)
        {
            SetGameMode();
        }
        GameManager.GameModeTag = (GameManager.GameMode)modeValue;
    }

    void Update()
    {
        if (InputController.AllPushKey() || InputController.AllPushMouseKey())
        {
            poaseFlag = false;
        }
        if (poaseFlag) { return; }
        MoveUI();
        gameEventTimer.TimerUpdate();
        

        if (gameEventTimer.GetTimerGameStartWait().IsEnabled()) { return; }
        if(GameManager.GameStateTag == GameManager.GameState.Result) { return; }

        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                if (!inputEnabled)
                {
                    SetRandomNumber();
                }
                break;
            case GameManager.GameMode.BurstPush:
            case GameManager.GameMode.MutualPush:
                inputEnabled = true;
                break;
        }
        if (gameEventTimer.GetTimerResetGameIdle().IsEnabled()) { return; }
        if(gameEventTimer.GetTimerResultOutputWait().IsEnabled()) { return; }
        ResultText();
        
    }

    private void MoveUI()
    {
        uIController.MovePoasePanel(new Vector2(0,1080));
        if(soundCount == 0)
        {
            gameSE.BattleStart();
            soundCount++;
        }
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
            inputEnabled = true;
        }
    }

    private void SetMaxKeyAndMouseClickCount()
    {
        maxCount = Random.Range(10, 20);
    }

    private void SetStartButton()
    {
        maxCount = 10;
    }

    private void ResultText()
    {
        if(victoryPlayer == VictoryPlayer.Null) { return; }
        DrawResult();

        switch (victoryPlayer)
        {
            case VictoryPlayer.KeyBoard:
                uIController.SetResultUI(victoryPlayer,new Vector2(-550, 200));
                uIController.ResultUI(victoryPlayer);
                gameSE.WinRound();
                keyBoardVictoryCount++;
                break;
            case VictoryPlayer.Mouse:
                uIController.SetResultUI(victoryPlayer,new Vector2(550, 200));
                uIController.ResultUI(victoryPlayer);
                gameSE.WinRound();
                mouseVictoryCount++;
                break;
            case VictoryPlayer.Draw:
                uIController.SetResultUI(victoryPlayer,new Vector2(0, 200));
                uIController.ResultUI(victoryPlayer);
                break;
        }
        uIController.VictoryCountText(victoryPlayer,keyBoardVictoryCount, mouseVictoryCount);
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
            case GameManager.GameMode.BurstPush:
            case GameManager.GameMode.MutualPush:
                bool keyBoardFire = keyBoardPlayer.GetMagicShot().Fire;
                bool mouseFire = mousePlayer.GetMagicShot().Fire;
                if(keyBoardFire&&mouseFire)
                {
                    victoryPlayer = VictoryPlayer.Draw;
                }
                break;
        }
    }

    public void SetViewPushKey(bool push,KeyCode _key)
    {
        if (gameEventTimer.GetTimerKeyClickUICoolDown().IsEnabled()) { return; }
        if (push)
        {
            gameEventTimer.GetTimerKeyClickUICoolDown().StartTimer(0.05f);
            uIController.GetKeyBoardTexture().PushChangeTexture(_key);
            gameSE.Push();
        }
        else
        {
            uIController.SetKeyBoardText(_key);
        }
    }
    public void SetViewPushMouseButton(bool push,MouseCode code)
    {
        if (gameEventTimer.GetTimerMouseClickUICoolDown().IsEnabled()) { return; }
        if (push)
        {
            gameEventTimer.GetTimerMouseClickUICoolDown().StartTimer(0.05f);
            uIController.GetMouseTexture().PushChangeTexture((int)code);
            gameSE.Push();
        }
        else
        {
            uIController.SetMouseButtonUI(code);
        }
    }

    private bool WinningResults()
    {
        bool noWin = keyBoardVictoryCount != 3 && mouseVictoryCount != 3;
        if (noWin) { return false; }
        GameManager.GameStateTag = GameManager.GameState.Result;
        if (keyBoardVictoryCount >= 3)
        {
            victoryPlayer = VictoryPlayer.KeyBoard;
            gameSE.WinGame();
        }
        else if (mouseVictoryCount >= 3)
        {
            victoryPlayer = VictoryPlayer.Mouse;
            gameSE.WinGame();
        }
        uIController.WinResultUI(victoryPlayer);
        return true;
    }
}
