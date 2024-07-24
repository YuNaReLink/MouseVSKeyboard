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

    public void SetRandomKey()
    {
        inputController.SetPressKey();
    }

    private void Update()
    {
        if (magicShot.Fire) { return; }
        if (gameController.VictoryPlayer == VictoryPlayer.KeyBoard)
        {
            magicShot.MagicFire();
        }
        ModeCommand();

    }
    private void ModeCommand()
    {
        if (!GameController.IsRapidPressFlag()) { return; }
        if (gameController.VictoryPlayer != VictoryPlayer.Null) { return; }
        switch (GameManager.GameModeTag)
        {
            case GameManager.GameMode.RapidPress:
                PressKeyCommand();
                break;
            case GameManager.GameMode.BurstPush:
                ClickKeyCommand();
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
        }
    }

    private void PressKeyCommand()
    {
        gameController.SetViewPushKey(InputController.GetKeyTag());
        if (inputController.RandomGetKey())
        {
            gameController.VictoryPlayer = VictoryPlayer.KeyBoard;
        }
    }
}
