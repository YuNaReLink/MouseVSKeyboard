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

    private void MagaicShotCommand()
    {
        magicShot.MagicFire(180);
        GameController.Preempt = true;
    }

    private void ModeCommand()
    {
        if (!GameController.IsRapidPressFlag()) { return; }
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
        }
    }

    private void ClickMouseCommand()
    {
        gameController.SetViewPushMouseButton(MouseCode.Left);
        if (Input.GetMouseButtonDown(0))
        {
            gameController.ClickCount++;
        }

        if(gameController.ClickCount >= gameController.GetMaxCount())
        {
            gameController.VictoryPlayer = VictoryPlayer.Mouse;
            MagaicShotCommand();
        }
    }

    private void MutualClickMouseCommand()
    {
        gameController.SetViewPushMouseButton(InputController.GetMouseCode());
        if (inputController.RandomMouseKey())
        {
            gameController.ClickCount++;
            inputController.SetPressMouseKey(gameMode);
        }

        if (gameController.ClickCount >= gameController.GetMaxCount())
        {
            gameController.VictoryPlayer = VictoryPlayer.Mouse;
            MagaicShotCommand();
        }
    }

    private void PressKeyCommand()
    {
        gameController.SetViewPushMouseButton(InputController.GetMouseCode());
        if (inputController.RandomMouseKey())
        {
            gameController.VictoryPlayer = VictoryPlayer.Mouse;
            MagaicShotCommand();
        }
    }
}
