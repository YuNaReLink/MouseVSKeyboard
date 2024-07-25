using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MousePlayer : CharacterController
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    public void SetRandomButton(GameManager.GameMode _mode)
    {
        gameMode = _mode;
        inputController.SetPressMouseKey(gameMode);
    }

    private void Update()
    {
        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.BurstPush:
            case GameManager.GameMode.MutualPush:
            case GameManager.GameMode.TypingAndAim:
                //–‚–@w‚Ìˆ—
                MagicCircleMakeItBigger(gameController.ClickCount);
                break;
        }
        ChangeAnimation();

        if (magicShot.Fire) { return; }
        //ƒ‚[ƒh•Ê‚Ì“ü—Íˆ—
        ModeCommand();
        if (!gameController.GetGameEventTimer().GetTimerResultOutputWait().IsEnabled() && !GameController.Preempt)
        {
            gameController.GetGameEventTimer().GetTimerResultOutputWait().StartTimer(0.1f);
        }
    }

    private void MagicShotCommand()
    {
        magicShot.MagicFire(180);
        GameController.Preempt = true;
    }

    private void ModeCommand()
    {
        if (!GameController.IsInputEnabledFlag()) { return; }
        if (GameController.Preempt&& !gameController.GetGameEventTimer().GetTimerResultOutputWait().IsEnabled()) { return; }
        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                PressKeyCommand();
                break;
            case GameManager.GameMode.BurstPush:
                ClickMouseCommand();
                break;
            case GameManager.GameMode.MutualPush:
                MutualClickMouseCommand();
                break;
            case GameManager.GameMode.TypingAndAim:
                AimClickMouseCommand();
                break;
        }
    }

    private void ClickMouseCommand()
    {
        gameController.SetViewPushMouseButton(false,MouseCode.Left);
        if (Input.GetMouseButtonDown(0))
        {
            gameController.SetViewPushMouseButton(true,MouseCode.Left);
            gameController.ClickCount++;
        }

        if(gameController.ClickCount >= gameController.GetMaxCount())
        {
            gameController.VictoryPlayer = VictoryPlayer.Mouse;
            MagicShotCommand();
        }
    }

    private void MutualClickMouseCommand()
    {
        if (inputController.RandomMouseKey())
        {
            gameController.SetViewPushMouseButton(true, InputController.GetMouseCode());
            gameController.ClickCount++;
            inputController.SetPressMouseKey(gameMode);
        }
        gameController.SetViewPushMouseButton(false,InputController.GetMouseCode());

        if (gameController.ClickCount >= gameController.GetMaxCount())
        {
            gameController.VictoryPlayer = VictoryPlayer.Mouse;
            MagicShotCommand();
        }
    }

    private void AimClickMouseCommand()
    {
        gameController.SetViewPushMouseButton(false, MouseCode.Left);
        if (Input.GetMouseButtonDown(0))
        {
            gameController.SetViewPushMouseButton(true, MouseCode.Left);
        }
        for (int i = 0; i < InputController.GetPushClickFlag().Length; i++)
        {
            if (!InputController.GetPushClickFlag()[i]) { return; }
            gameController.ClickCount = i + 1;
        }

        if (gameController.ClickCount >= gameController.GetMaxCount())
        {
            gameController.VictoryPlayer = VictoryPlayer.Mouse;
            MagicShotCommand();
        }
    }

    private void PressKeyCommand()
    {
        gameController.SetViewPushMouseButton(false, InputController.GetMouseCode());
        if (inputController.RandomMouseKey())
        {
            gameController.SetViewPushMouseButton(true, InputController.GetMouseCode());
            gameController.VictoryPlayer = VictoryPlayer.Mouse;
            MagicShotCommand();
        }
    }
}
