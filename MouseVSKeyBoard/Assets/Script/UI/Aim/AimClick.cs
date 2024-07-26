using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputController;

public class AimClick : MonoBehaviour
{
    [SerializeField]
    private List<Button> buttonArray = new List<Button>();
    [SerializeField]
    private List<Text> textArray = new List<Text> ();

    public void Initialize()
    {
        int num = transform.childCount;
        for(int i = 0; i < num; i++)
        {
            buttonArray.Add(transform.GetChild(i).GetComponent<Button>());
            textArray.Add(transform.GetChild (i).GetComponentInChildren<Text>());
        }
        gameObject.SetActive(false);
    }

    public void SetPushButtonSequence()
    {
        for(int i = 0;i< 3; i++)
        {
            buttonArray[i].onClick.RemoveAllListeners();
            GetPushClickFlag()[i] = false;
        }
        int value = 1;
        for(int i = 0;i < 3; i++)
        {
            textArray[i].text = value.ToString();
            switch (value)
            {
                case 1:
                    buttonArray[i].onClick.AddListener(OneButton);
                    break;
                case 2:
                    buttonArray[i].onClick.AddListener(TwoButton);
                    break;
                case 3:
                    buttonArray[i].onClick.AddListener(ThreeButton);
                    break;
            }
            value++;
        }
    }

    private void OneButton()
    {
        GetPushClickFlag()[0] = true;
    }
    private void TwoButton()
    {
        if (!GetPushClickFlag()[0]) { return; }
        GetPushClickFlag()[1] = true;
    }
    private void ThreeButton()
    {
        if (!GetPushClickFlag()[1]) { return; }
        GetPushClickFlag()[2] = true;
    }
}
