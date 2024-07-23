using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardPlayer : MonoBehaviour
{
    [SerializeField]
    private GameController gameController = null;
    private InputController inputController = null;
    public InputController GetInputController() {  return inputController; }

    [SerializeField]
    private Vector2 basePosition = Vector2.zero;
    [SerializeField]
    private Vector2 victryPosition = Vector2.zero;
    [SerializeField]
    private bool victoryFlag = false;
    public bool IsVictory() {  return victoryFlag; }

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inputController = new InputController();
        inputController.SetPressKey();

        basePosition = transform.position;
        victryPosition = new Vector2(0,basePosition.y);
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
            gameController.GetPreesKey(InputController.GetKeyTag());
            if (inputController.RandomGetKey())
            {
                gameController.VictoryPlayer = VictoryPlayer.KeyBoard;
            }
        }
    }
}
