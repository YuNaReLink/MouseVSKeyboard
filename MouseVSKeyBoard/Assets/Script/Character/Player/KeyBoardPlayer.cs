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
        //–‚–@w‚Ìˆ—
        MagicCircleMakeItBigger();

        //–‚–@‚ð”­ŽË‚·‚éˆ—
        if (magicShot.Fire) { return; }
        if (gameController.VictoryPlayer == VictoryPlayer.KeyBoard)
        {
            spriteRenderer.sprite = sprites[1];
            magicShot.MagicFire(0);
        }
        ModeCommand();

    }

    private void MagicCircleMakeItBigger()
    {
        Vector3 scale = magicCircle.localScale;
        /*
         */
        float rangeDifference = 0.5f - 0;
        float add = rangeDifference / (gameController.GetMaxCount() - 1);
        scale.x = 0 + (gameController.KeyCount * add);
        rangeDifference = 1f - 0;
        add = rangeDifference / (gameController.GetMaxCount() - 1);
        scale.y = 0 + (gameController.KeyCount * add);
        scale.z = 0 + (gameController.KeyCount * add);
        //scale = Vector3.Lerp(scale, new Vector3(0.05f, 0.1f, 0.1f), 1);
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
