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
        //–‚–@w‚Ìˆ—
        MagicCircleMakeItBigger();

        if (magicShot.Fire) { return; }
        if (gameController.VictoryPlayer == VictoryPlayer.Mouse)
        {
            spriteRenderer.sprite = sprites[1];
            magicShot.MagicFire(180);
        }
        ModeCommand();
    }

    private void MagicCircleMakeItBigger()
    {
        Vector3 scale = magicCircle.localScale;
        float rangeDifference = 0.5f - 0;
        float add = rangeDifference / (gameController.GetMaxCount() - 1);
        scale.x = 0 + (gameController.ClickCount * add);
        rangeDifference = 1f - 0;
        add = rangeDifference / (gameController.GetMaxCount() - 1);
        scale.y = 0 + (gameController.ClickCount * add);
        scale.z = 0 + (gameController.ClickCount * add);
        magicCircle.localScale = scale;
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
