using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        //uIController = GameObject.FindGameObjectWithTag("UI").GetComponent<GameUIController>();
    }

    void Update()
    {
        if (!rapidPress)
        {
            SetRandomNumber();
        }
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

    public void GetPreesKey(KeyCode _key)
    {
        uIController.SetKeyBoardText(_key);
    }
}
