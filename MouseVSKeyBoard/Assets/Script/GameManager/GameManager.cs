using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager
{
    /// <summary>
    /// ゲームモードのenumタグ
    /// </summary>
    public enum GameMode
    {
        Null = -1,
        RapidPress,
        BurstPush,
        MutualPush,
        TypingAndAim,
        DataEnd
    }
    //それのインスタンス宣言
    private static GameMode         gameMode = GameMode.Null;
    public static GameMode          GameModeTag { get { return gameMode; } set { gameMode = value; } }
    /// <summary>
    /// ゲーム状態のenumタグ
    /// </summary>
    public enum GameState
    {
        Null = -1,
        Title,
        Game,
        Poase,
        Result,
        DataEnd
    }
    //のインスタンス宣言
    private static GameState        gameState = GameState.Null;
    public static GameState         GameStateTag { get {return gameState; } set {gameState = value; } }

    //ランダム数値生成の最大値
    public static readonly int MaxMeasurementNumber = 100;

    /// <summary>
    /// UIを移動させるための使う変数・インスタンス宣言関連
    /// </summary>
    public enum MoveUIPositionTag
    {
        Null = -1,
        ScreenOut,
        ScreenIn,
        PoaseScreenOut,
        ScreenInitilaize,
        KeyBoardPlayer,
        MousePlayer,
        Drow,
        DataEnd
    }

    public static readonly Vector2[] MoveUIPositionData = 
    {
        new Vector2(0,750),
        new Vector2(0,370),
        new Vector2(0,1080),
        new Vector2(0,0),
        new Vector2(-550,200),
        new Vector2(550,200),
        new Vector2(0,200),
    };

    /// <summary>
    /// ゲームコントローラーで使うタイマーのカウント変数
    /// </summary>
    public static readonly float MaxResetGameIdleCount = 5f;
    public static readonly float MaxClickUICoolDown = 0.05f;
}
