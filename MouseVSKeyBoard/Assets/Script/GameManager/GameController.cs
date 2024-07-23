using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VictoryPlayer
{
    Null = -1,
    KeyBoard,
    Mouse,
    Draw,
    DataEnd
}

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameUIController uIController = null;

    [SerializeField]
    private const int baseMeasurementValue = 1;
    [SerializeField]
    private int measurementNumber = 0;

    [SerializeField]
    private static bool rapidPress = false;

    public static bool IsRapidPressFlag() {  return rapidPress; }

    [SerializeField]
    private KeyBoardPlayer keyBoardPlayer = null;
    [SerializeField]
    private MousePlayer mousePlayer = null;

    private VictoryPlayer victoryPlayer = VictoryPlayer.Null;
    public VictoryPlayer VictoryPlayer { get { return victoryPlayer; } set { victoryPlayer = value; } }

    private void Start()
    {
        uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<GameUIController>();
        keyBoardPlayer = GameObject.FindGameObjectWithTag("KeyBoardPlayer").GetComponent<KeyBoardPlayer>();
        mousePlayer = GameObject.FindGameObjectWithTag("MousePlayer").GetComponent<MousePlayer>();

    }

    void Update()
    {
        if (!rapidPress)
        {
            SetRandomNumber();
        }

        ResultText();

    }

    private void SetRandomNumber()
    {
        measurementNumber = Random.Range(0, 100);
        if(measurementNumber < baseMeasurementValue)
        {
            uIController.GetStartUI().SetActive(true);
            rapidPress = true;
        }
    }

    private void ResultText()
    {
        if(keyBoardPlayer.GetInputController().RandomGetKey()&&Input.GetMouseButtonDown(0))
        {
            victoryPlayer = VictoryPlayer.Draw;
        }

        switch (victoryPlayer)
        {
            case VictoryPlayer.KeyBoard:
                uIController.ResultUI(victoryPlayer);
                break;
            case VictoryPlayer.Mouse:
                uIController.ResultUI(victoryPlayer);
                break;
            case VictoryPlayer.Draw:
                uIController.ResultUI(victoryPlayer);
                break;
        }
        
    }

    public void GetPreesKey(KeyCode _key)
    {
        uIController.SetKeyBoardText(_key);
    }
}
