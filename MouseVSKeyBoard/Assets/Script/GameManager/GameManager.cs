using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager
{
    /// <summary>
    /// �Q�[�����[�h��enum�^�O
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
    //����̃C���X�^���X�錾
    private static GameMode         gameMode = GameMode.Null;
    public static GameMode          GameModeTag { get { return gameMode; } set { gameMode = value; } }
    /// <summary>
    /// �Q�[����Ԃ�enum�^�O
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
    //�̃C���X�^���X�錾
    private static GameState        gameState = GameState.Null;
    public static GameState         GameStateTag { get {return gameState; } set {gameState = value; } }

    //�����_�����l�����̍ő�l
    public static readonly int MaxMeasurementNumber = 100;

    /// <summary>
    /// UI���ړ������邽�߂̎g���ϐ��E�C���X�^���X�錾�֘A
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
    /// �Q�[���R���g���[���[�Ŏg���^�C�}�[�̃J�E���g�ϐ�
    /// </summary>
    public static readonly float MaxResetGameIdleCount = 5f;
    public static readonly float MaxClickUICoolDown = 0.05f;
}
