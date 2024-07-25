using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

//���҂̌��ʂ��o�����߂�enum�N���X
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
    /// �������̊Ԋu�������_���Ő�������ϐ�
    /// 0:�����_���̍ő�l(���̐��l�ȉ��Ȃ瑁�����J�n)
    /// 1:�����_���̍ő�l����������ꕨ
    /// 2:�����_���̍ő�l�������������v������ϐ�
    /// </summary>
    [SerializeField]
    private const int               measurementMaxValue = 1;
    [SerializeField]
    private int                     baseMeasurementValue = measurementMaxValue;
    [SerializeField]
    private int                     measurementNumber = 0;
    /// <summary>
    /// �v���C���[�����͂ł��邩���߂�t���O
    /// </summary>
    [SerializeField]
    private static bool             inputEnabled = false;
    public static bool              IsInputEnabledFlag() {  return inputEnabled; }
    /// <summary>
    /// ��ɖ��@����������҂����肷�邽�߂̃t���O
    /// </summary>
    [SerializeField]
    private static bool             preempt = false;
    public static bool              Preempt {  get { return preempt; } set { preempt = value; } }
    /// <summary>
    /// �A�ŁE���݉����̍ő�N���b�N�J�E���g�����߂�ϐ�
    /// maxCount:�N���b�N�̍ő�l(�����_�������������l��10)
    /// keyCount:�L�[�{�[�h�̃N���b�N�J�E���g���v������ϐ�
    /// clickCount:�}�E�X�̃N���b�N�J�E���g���v������ϐ�
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
    /// �L�[�{�[�h�ƃ}�E�X�ő��삷��v���C���[�̃C���X�^���X����
    /// </summary>
    [SerializeField]
    private KeyBoardPlayer          keyBoardPlayer = null;
    [SerializeField]
    private MousePlayer             mousePlayer = null;
    /// <summary>
    /// ���҂𔻒肷�邽�߂�enum�̃C���X�^���X����
    /// </summary>
    private VictoryPlayer           victoryPlayer = VictoryPlayer.Null;
    public VictoryPlayer            VictoryPlayer { get { return victoryPlayer; } set { victoryPlayer = value; } }
    /// <summary>
    /// �L�[�{�[�h�ƃ}�E�X�̏����񐔂��v������ϐ��֘A
    /// maxVictoryCount:�����̍ő��
    /// keyBoardVictoryCount:�L�[�{�[�h�̏����v���񐔕ϐ�
    /// mouseVictoryCount:�}�E�X�̏����v���񐔕ϐ�
    /// </summary>
    [SerializeField]
    private const int               maxVictoryCount = 3;
    private int                     keyBoardVictoryCount = 0;
    public int                      GetKeyBoardVictoryCount() { return keyBoardVictoryCount; }
    private int                     mouseVictoryCount = 0;
    public int                      GetMouseVictoryCount() { return mouseVictoryCount; }
    /// <summary>
    /// �Q�[���R���g���[���[�Ŏg���^�C�}�[���ꎮ�܂Ƃ߂��N���X�̃C���X�^���X����
    /// </summary>
    private GameEventTimer          gameEventTimer = null;
    public GameEventTimer           GetGameEventTimer() { return gameEventTimer; }

    /// <summary>
    /// �Q�[���J�n���A�Q�[���̏������~�߂�t���O
    /// </summary>
    private bool                    poaseFlag = true;
    private void Start()
    {
        //�Q�[���V�[���X�^�[�g���̏���������
        Initialize();
        //�����ԂɍX�V���鏉��������
        InitializeGameSetting();
    }

    private void Initialize()
    {
        //�A�^�b�`����
        uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<GameUIController>();
        keyBoardPlayer = GameObject.FindGameObjectWithTag("KeyBoardPlayer").GetComponent<KeyBoardPlayer>();
        mousePlayer = GameObject.FindGameObjectWithTag("MousePlayer").GetComponent<MousePlayer>();
        //�J�E���g�N���X�̐����Ə�����
        gameEventTimer = new GameEventTimer();
        gameEventTimer.InitializeAssignTimer();

        poaseFlag = true;
    }

    /// <summary>
    ///�����ԂɍX�V���鏉��������
    ///��x���s�����܂�����ɌĂяo������
    ///������x�������n�߂邽�߂̏�����
    /// </summary>
    private void InitializeGameSetting()
    {
        //���݂̏�Ԃ�ݒ�
        GameStateTag = GameState.Game;
        //�Q�[�����[�h�������_���ɐݒ�
        SetGameMode();
        //�Q�[�����J�n����܂ł̃^�C�}�[��ݒ�
        gameEventTimer.GetTimerGameStartWait().StartTimer(5f);
        //���ҁE�L�[�{�[�h�A�}�E�X�̃N���b�N�J�E���g������
        victoryPlayer = VictoryPlayer.Null;
        keyCount = 0;
        clickCount = 0;

        inputEnabled = false;
        preempt = false;
        //�v���C���[�̏�����
        mousePlayer.InitializePosition();
        keyBoardPlayer.InitializePosition();
        //��Őݒ肵�����[�h���Ƃɏ���������
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
        //UI�R���g���[���[�̏�����
        uIController.SetResultUI(victoryPlayer,new Vector2(0,2000));
        uIController.ChangeExplanationSprit((int)GameModeTag);
    }
    private void SetGameMode()
    {
        //GameMode��enum���烉���_���ɐ������擾
        int modeValue = Random.Range(0, (int)GameMode.DataEnd);
        //�O��Ɠ������[�h�Ȃ�Ⴄ���[�h�ɂȂ�܂ŌJ��Ԃ�
        if(GameModeTag == (GameMode)modeValue)
        {
            SetGameMode();
        }
        //�V�������[�h����
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
        //�^�C�}�[�̍X�V
        gameEventTimer.TimerUpdate();

        GameEventUpdate();
        
    }

    /// <summary>
    /// �L�����o�X���ɂ���UI�𓮂����֐�
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
        //�Q�[���X�^�[�g���ɏ�����҂�����^�C�}�[
        if (gameEventTimer.GetTimerGameStartWait().IsEnabled()) { return; }
        //�Q�[����Ԃɂ�菈����ύX
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
        //������������
        DrawResult();
        //�ŏI���s����
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
        //���҂�UI��\��
        uIController.VictoryCountText(victoryPlayer,keyBoardVictoryCount, mouseVictoryCount);
        uIController.ActiveUIObject((int)GameUIController.UITag.Go, false);
        //���̎������J�n�E�ŏI���ʂ̏�����ҋ@������^�C�}�[���N��
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
