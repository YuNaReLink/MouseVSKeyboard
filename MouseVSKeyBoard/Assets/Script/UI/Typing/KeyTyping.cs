using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputController;

public class KeyTyping : MonoBehaviour
{
    [SerializeField]
    private List<Text> keyText = new List<Text>();

    public void Initialize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            keyText.Add(transform.GetChild(i).GetComponentInChildren<Text>());
        }
        gameObject.SetActive(false);
    }

    public void InitilaizeKeyTextColor()
    {
        for (int i = 0; i < keyText.Count; i++)
        {
            keyText[i].color = Color.blue;
        }
    }

    public void KeyTypingCommand()
    {
        for (int i = 0; i < GetKeyCodeArray().Length; i++)
        {
            if (Input.GetKeyDown(GetKeyCodeArray()[i]))
            {
                if (GetPushKeyFlag()[i]) { continue; }
                switch (i)
                {
                    case 0:
                        keyText[0].color = Color.yellow;
                        break;
                    case 1:
                        if (!GetPushKeyFlag()[i - 1]) { return; }
                        keyText[1].color = Color.yellow;
                        break;
                    case 2:
                        if (!GetPushKeyFlag()[i - 1]) { return; }
                        keyText[2].color = Color.yellow;
                        break;
                    case 3:
                        if (!GetPushKeyFlag()[i - 1]) { return; }
                        keyText[3].color = Color.yellow;
                        break;
                }
                GetPushKeyFlag()[i] = true;
                return;
            }
        }
    }
    
    public void SetTypingKey(KeyCode[] keyCodeArray)
    {
        for(int i = 0; i < keyText.Count; i++)
        {
            string keyName = "";
            switch (keyCodeArray[i])
            {
                case KeyCode.A:
                    keyName = "A";
                    break;
                case KeyCode.S:
                    keyName = "S";
                    break;
                case KeyCode.D:
                    keyName = "D";
                    break;
            }
            keyText[i].text = keyName;
        }
    }
}
