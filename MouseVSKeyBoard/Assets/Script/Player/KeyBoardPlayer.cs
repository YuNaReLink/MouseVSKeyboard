using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardPlayer : MonoBehaviour
{
    [SerializeField]
    private GameController gameController = null;
    private InputController inputController = null;

    [SerializeField]
    private Vector2 basePosition = Vector2.zero;
    [SerializeField]
    private Vector2 victryPosition = Vector2.zero;
    [SerializeField]
    private bool victoryFlag = false;
    // Start is called before the first frame update
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inputController = new InputController();
        inputController.SetPressKey();

        basePosition = transform.position;
        victryPosition = new Vector2(0,basePosition.y);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameController.IsRapidPressFlag())
        {
            gameController.GetPreesKey(InputController.GetKeyTag());
            if (inputController.RandomGetKey())
            {
                victoryFlag = true;
            }
        }

        if (victoryFlag)
        {
            transform.position = Vector2.Lerp(transform.position, victryPosition, Time.deltaTime * 10f);
            
        }
    }
}
