using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

//勝者の結果を出すためのenumクラス
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
    private GameUIController        uIController = null;
    /// <summary>
    /// 早押しの間隔をランダムで生成する変数
    /// 0:ランダムの最大値(この数値以下なら早押し開始)
    /// 1:ランダムの最大値を代入する入れ物
    /// 2:ランダムの最大値よりも小さいか計測する変数
    /// </summary>
    [SerializeField]
    private const int               measurementMaxValue = 1;
    [SerializeField]
    private int                     baseMeasurementValue = measurementMaxValue;
    [SerializeField]
    private int                     measurementNumber = 0;
    /// <summary>
    /// プレイヤーが入力できるか決めるフラグ
    /// </summary>
    [SerializeField]
    private static bool             inputEnabled = false;
    public static bool              IsInputEnabledFlag() {  return inputEnabled; }
    /// <summary>
    /// 先に魔法を放った勝者か判定するためのフラグ
    /// </summary>
    [SerializeField]
    private static bool             preempt = false;
    public static bool              Preempt {  get { return preempt; } set { preempt = value; } }
    /// <summary>
    /// 連打・交互押しの最大クリックカウントを決める変数
    /// maxCount:クリックの最大値(ランダム生成※初期値は10)
    /// keyCount:キーボードのクリックカウントを計測する変数
    /// clickCount:マウスのクリックカウントを計測する変数
    /// </summary>
    [SerializeField]
    private int                     maxCount = 10;
    public int                      GetMaxCount() {  return maxCount; }
    [SerializeField]
    private int                     keyCount = 0;
    public int                      KeyCount {  get { return keyCount; } set { keyCount = value; } }
    [SerializeField]
    private int                     clickCount = 0;
    public int                      ClickCount { get {return clickCount; } set { clickCount = value; } }
    /// <summary>
    /// キーボードとマウスで操作するプレイヤーのインスタンス生成
    /// </summary>
    [SerializeField]
    private KeyBoardPlayer          keyBoardPlayer = null;
    [SerializeField]
    private MousePlayer             mousePlayer = null;
    /// <summary>
    /// 勝者を判定するためのenumのインスタンス生成
    /// </summary>
    private VictoryPlayer           victoryPlayer = VictoryPlayer.Null;
    public VictoryPlayer            VictoryPlayer { get { return victoryPlayer; } set { victoryPlayer = value; } }
    /// <summary>
    /// キーボードとマウスの勝利回数を計測する変数関連
    /// maxVictoryCount:勝利の最大回数
    /// keyBoardVictoryCount:キーボードの勝利計測回数変数
    /// mouseVictoryCount:マウスの勝利計測回数変数
    /// </summary>
    [SerializeField]
    private const int               maxVictoryCount = 3;
    private int                     keyBoardVictoryCount = 0;
    public int                      GetKeyBoardVictoryCount() { return keyBoardVictoryCount; }
    private int                     mouseVictoryCount = 0;
    public int                      GetMouseVictoryCount() { return mouseVictoryCount; }
    /// <summary>
    /// ゲームコントローラーで使うタイマーを一式まとめたクラスのインスタンス生成
    /// </summary>
    private GameEventTimer          gameEventTimer = null;
    public GameEventTimer           GetGameEventTimer() { return gameEventTimer; }

    /// <summary>
    /// ゲーム開始時、ゲームの処理を止めるフラグ
    /// </summary>
    private bool                    poaseFlag = true;
    private void Start()
    {
        //ゲームシーンスタート時の初期化処理
        Initialize();
        //試合間に更新する初期化処理
        InitializeGameSetting();
    }

    private void Initialize()
    {
        //アタッチ処理
        uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<GameUIController>();
        keyBoardPlayer = GameObject.FindGameObjectWithTag("KeyBoardPlayer").GetComponent<KeyBoardPlayer>();
        mousePlayer = GameObject.FindGameObjectWithTag("MousePlayer").GetComponent<MousePlayer>();
        //カウントクラスの生成と初期化
        gameEventTimer = new GameEventTimer();
        gameEventTimer.InitializeAssignTimer();

        poaseFlag = true;
    }

    /// <summary>
    ///試合間に更新する初期化処理
    ///一度勝敗が決まった後に呼び出す処理
    ///もう一度試合を始めるための初期化
    /// </summary>
    private void InitializeGameSetting()
    {
        //現在の状態を設定
        GameStateTag = GameState.Game;
        //ゲームモードをランダムに設定
        SetGameMode();
        //ゲームが開始するまでのタイマーを設定
        gameEventTimer.GetTimerGameStartWait().StartTimer(5f);
        //勝者・キーボード、マウスのクリックカウント初期化
        victoryPlayer = VictoryPlayer.Null;
        keyCount = 0;
        clickCount = 0;

        inputEnabled = false;
        preempt = false;
        //プレイヤーの初期化
        mousePlayer.InitializePosition();
        keyBoardPlayer.InitializePosition();
        //上で設定したモードごとに初期化処理
        switch (GameModeTag)
        {
            case GameMode.RapidPress:
                keyBoardPlayer.SetRandomKey(GameModeTag);
                mousePlayer.SetRandomButton(GameModeTag);
                break;
            case GameMode.BurstPush:
                SetMaxKeyAndMouseClickCount();
                break;
            case GameMode.MutualPush:
                keyBoardPlayer.SetRandomKey(GameModeTag);
                mousePlayer.SetRandomButton(GameModeTag);
                SetStartButton();
                break;
        }
        //UIコントローラーの初期化
        uIController.SetResultUI(victoryPlayer,new Vector2(0,2000));
        uIController.ChangeExplanationSprit((int)GameModeTag);
    }
    private void SetGameMode()
    {
        //GameModeのenumからランダムに整数を取得
        int modeValue = Random.Range(0, (int)GameMode.DataEnd);
        //前回と同じモードなら違うモードになるまで繰り返す
        if(GameModeTag == (GameMode)modeValue)
        {
            SetGameMode();
        }
        //新しいモードを代入
        GameModeTag = (GameMode)modeValue;
    }

    private bool PoaseStop()
    {
        if (!poaseFlag) { return false; }
        if (InputController.AllPushKey() || InputController.AllPushMouseKey())
        {
            poaseFlag = false;
        }
        return true;
    }

    void Update()
    {
        if (PoaseStop()) { return; }
        MoveUI();
        //タイマーの更新
        gameEventTimer.TimerUpdate();

        GameEventUpdate();
        
    }

    /// <summary>
    /// キャンバス内にあるUIを動かす関数
    /// </summary>
    private void MoveUI()
    {
        uIController.MovePoasePanel(MoveUIPositionData[(int)MoveUIPositionTag.PoaseScreenOut]);
        uIController.MoveExplanationUI(uIController.MoveUIPos);
        if(GameStateTag == GameState.Result)
        {
            uIController.MoveWinResultUI(MoveUIPositionData[(int)MoveUIPositionTag.ScreenIn]);
        }
    }

    private void GameEventUpdate()
    {
        //ゲームスタート時に処理を待たせるタイマー
        if (gameEventTimer.GetTimerGameStartWait().IsEnabled()) { return; }
        //ゲーム状態により処理を変更
        switch (GameStateTag)
        {
            case GameState.Game:
                if (!inputEnabled)
                {
                    switch (GameModeTag)
                    {
                        case GameMode.RapidPress:
                            SetRandomNumber();
                            break;
                        case GameMode.BurstPush:
                        case GameMode.MutualPush:
                            inputEnabled = true;
                            uIController.ActiveUIObject((int)GameUIController.UITag.Go, true);
                            break;
                    }
                }
                else
                {
                    
                }
                ResultUpdate();
                break;
            case GameState.Result:
                break;
        }
    }

    private void ResultUpdate()
    {
        if (gameEventTimer.GetTimerResetGameIdle().IsEnabled()) { return; }
        if (gameEventTimer.GetTimerResultOutputWait().IsEnabled()) { return; }
        ResultText();
    }

    private void SetRandomNumber()
    {
        measurementNumber = Random.Range(0, MaxMeasurementNumber);
        if(measurementNumber < baseMeasurementValue)
        {
            inputEnabled = true;
            uIController.ActiveUIObject((int)GameUIController.UITag.Go, true);
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
        //引き分け処理
        DrawResult();
        //最終勝敗判定
        switch (victoryPlayer)
        {
            case VictoryPlayer.KeyBoard:
                uIController.SetResultUI(victoryPlayer, MoveUIPositionData[(int)MoveUIPositionTag.KeyBoardPlayer]);
                uIController.ResultUI(victoryPlayer);
                keyBoardVictoryCount++;
                break;
            case VictoryPlayer.Mouse:
                uIController.SetResultUI(victoryPlayer, MoveUIPositionData[(int)MoveUIPositionTag.MousePlayer]);
                uIController.ResultUI(victoryPlayer);
                mouseVictoryCount++;
                break;
            case VictoryPlayer.Draw:
                uIController.SetResultUI(victoryPlayer, MoveUIPositionData[(int)MoveUIPositionTag.Drow]);
                uIController.ResultUI(victoryPlayer);
                break;
        }
        //勝者のUIを表示
        uIController.VictoryCountText(victoryPlayer,keyBoardVictoryCount, mouseVictoryCount);
        uIController.ActiveUIObject((int)GameUIController.UITag.Go, false);
        //次の試合を開始・最終結果の処理を待機させるタイマーを起動
        gameEventTimer.GetTimerResetGameIdle().
            StartTimer(MaxResetGameIdleCount);
        gameEventTimer.GetTimerResetGameIdle().OnCompleted += () =>
        {
            if (WinningResults()) { return; }
            InitializeGameSetting();
            uIController.InitilaizeGameUISetting();
        };
    }

    private void DrawResult()
    {
        switch (GameModeTag)
        {
            case GameMode.RapidPress:
            case GameMode.BurstPush:
            case GameMode.MutualPush:
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
            gameEventTimer.GetTimerKeyClickUICoolDown().StartTimer(MaxClickUICoolDown);
            uIController.GetKeyBoardTexture().PushChangeTexture(_key);
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
            gameEventTimer.GetTimerMouseClickUICoolDown().StartTimer(MaxClickUICoolDown);
            uIController.GetMouseTexture().PushChangeTexture((int)code);
        }
        else
        {
            uIController.SetMouseButtonUI(code);
        }
    }

    private bool WinningResults()
    {
        bool noWin = keyBoardVictoryCount != maxVictoryCount&&
                     mouseVictoryCount != maxVictoryCount;
        if (noWin) { return false; }
        GameStateTag = GameState.Result;
        if (keyBoardVictoryCount >= maxVictoryCount)
        {
            victoryPlayer = VictoryPlayer.KeyBoard;
        }
        else if (mouseVictoryCount >= maxVictoryCount)
        {
            victoryPlayer = VictoryPlayer.Mouse;
        }
        uIController.WinResultUI(victoryPlayer);
        return true;
    }
}
