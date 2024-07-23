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
        if (gameController.VictoryPlayer == VictoryPlayer.KeyBoard)
        {
            transform.position = Vector2.Lerp(transform.position, victryPosition, Time.deltaTime * 10f);
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
