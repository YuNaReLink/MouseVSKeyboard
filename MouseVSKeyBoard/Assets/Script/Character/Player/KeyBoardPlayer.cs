using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardPlayer : CharacterController
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    public void SetRandomKey(GameManager.GameMode _mode)
    {
        gameMode = _mode;
        inputController.SetPushKey(gameMode);
    }

    private void Update()
    {
        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.BurstPush:
            case GameManager.GameMode.MutualPush:
                //魔法陣の処理
                MagicCircleMakeItBigger(gameController.KeyCount);
                break;
        }
        ChangeAnimation();

        //魔法を発射する処理
        if (magicShot.Fire) { return; }
        //モード別の入力処理
        ModeCommand();
        if (!gameController.GetGameEventTimer().GetTimerResultOutputWait().IsEnabled()&& !GameController.Preempt)
        {
            gameController.GetGameEventTimer().GetTimerResultOutputWait().StartTimer(0.1f);
        }
    }

    private void MagicShotCommand()
    {
        magicShot.MagicFire(0);
        GameController.Preempt = true;
    }

    private void ModeCommand()
    {
        if (!GameController.IsRapidPressFlag()) { return; }
        if (GameController.Preempt && !gameController.GetGameEventTimer().GetTimerResultOutputWait().IsEnabled()) { return; }
        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                PressKeyCommand();
                break;
            case GameManager.GameMode.BurstPush:
                ClickKeyCommand();
                break;
            case GameManager.GameMode.MutualPush:
                MutualClickKeyCommand();
                break;
        }
    }
    private void ClickKeyCommand()
    {
        gameController.SetViewPushKey(KeyCode.A);
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameController.KeyCount++;
        }

        if (gameController.KeyCount >= gameController.GetMaxCount())
        {
            gameController.VictoryPlayer = VictoryPlayer.KeyBoard;
            MagicShotCommand();
        }
    }

    private void MutualClickKeyCommand()
    {
        gameController.SetViewPushKey(InputController.GetKeyTag());
        if (inputController.RandomGetKey())
        {
            gameController.KeyCount++;
            inputController.SetPushKey(gameMode);
        }

        if (gameController.KeyCount >= gameController.GetMaxCount())
        {
            gameController.VictoryPlayer = VictoryPlayer.KeyBoard;
            MagicShotCommand();
        }
    }

    private void PressKeyCommand()
    {
        gameController.SetViewPushKey(InputController.GetKeyTag());
        if (inputController.RandomGetKey())
        {
            gameController.VictoryPlayer = VictoryPlayer.KeyBoard;
            MagicShotCommand();
        }
    }
}
