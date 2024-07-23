using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MousePlayer : CharacterController
{
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
        PressKeyCommand();
    }

    private void PressKeyCommand()
    {
        if (GameController.IsRapidPressFlag())
        {
            if (gameController.VictoryPlayer != VictoryPlayer.Null) { return; }
            if (inputController.RandomMouseKey())
            {
                gameController.VictoryPlayer = VictoryPlayer.Mouse;
            }
        }
    }

}
