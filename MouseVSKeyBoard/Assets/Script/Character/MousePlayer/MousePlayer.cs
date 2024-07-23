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
        if (gameController.VictoryPlayer == VictoryPlayer.Mouse)
        {
            transform.position = Vector2.Lerp(transform.position, victryPosition, Time.deltaTime * 10f);
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
