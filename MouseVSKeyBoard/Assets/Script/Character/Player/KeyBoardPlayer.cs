using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardPlayer : CharacterController
{
    protected override void Start()
    {
        base.Start();
        inputController.SetPressKey();
    }


    private void Update()
    {
        if (magicShot.Fire) { return; }
        if (gameController.VictoryPlayer == VictoryPlayer.KeyBoard)
        {
            magicShot.MagicFire();
        }
        PressKeyCommand();

    }

    private void PressKeyCommand()
    {
        if (GameController.IsRapidPressFlag())
        {
            if(gameController.VictoryPlayer != VictoryPlayer.Null) { return; }
            gameController.GetPreesKey(InputController.GetKeyTag());
            if (inputController.RandomGetKey())
            {
                gameController.VictoryPlayer = VictoryPlayer.KeyBoard;
            }
        }
    }
}
