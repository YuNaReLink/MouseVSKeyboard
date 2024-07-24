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
        inputController.SetPressMouseKey();
    }
    private void Update()
    {
        if (magicShot.Fire) { return; }
        if (gameController.VictoryPlayer == VictoryPlayer.Mouse)
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
                ClickMouseCommand();
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
        }
    }

    private void PressKeyCommand()
    {
        gameController.SetViewPushMouseButton(InputController.GetMouseCode());
        if (inputController.RandomMouseKey())
        {
            gameController.VictoryPlayer = VictoryPlayer.Mouse;
        }
    }

}
