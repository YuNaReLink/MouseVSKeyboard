using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouseCode
{
    Null = -1,
    Left,
    Right,
    Middle,
    DataEnd
}

public class InputController
{
    private static KeyCode key = KeyCode.None;
    public static KeyCode GetKeyTag() {  return key; }
    public bool RandomGetKey() { return Input.GetKeyDown(key); }

    public void SetPushKey(GameManager.GameMode _mode)
    {
        int num = 0;
        switch (_mode)
        {
            case GameManager.GameMode.RapidPress:
                num = Random.Range(0, 3);
                switch (num)
                {
                    case 0:
                        key = KeyCode.A;
                        break;
                    case 1:
                        key = KeyCode.S;
                        break;
                    case 2:
                        key = KeyCode.D;
                        break;
                }
                break;
            case GameManager.GameMode.MutualPush:
                num = Random.Range(0, 2);
                switch (num)
                {
                    case 0:
                        if(key == KeyCode.A)
                        {
                            key = KeyCode.D;
                        }
                        else
                        {
                            key = KeyCode.A;
                        }
                        break;
                    case 1:
                        if(key == KeyCode.D)
                        {
                            key = KeyCode.A;
                        }
                        else
                        {
                            key = KeyCode.D;
                        }
                        break;
                }
                break;
        }
    }

    public static bool AllPushKey() { return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D); }

    private static MouseCode mouseCode = MouseCode.Null;
    public static MouseCode GetMouseCode() { return mouseCode; }

    public bool RandomMouseKey() { return Input.GetMouseButtonDown((int)mouseCode); }

    public void SetPressMouseKey(GameManager.GameMode _mode)
    {
        int num = 0;
        switch (_mode)
        {
            case GameManager.GameMode.RapidPress:
                num = Random.Range(0, 3);
                mouseCode = (MouseCode)num;
                break;
            case GameManager.GameMode.MutualPush:
                num = Random.Range(0, 2);
                switch (num)
                {
                    case 0:
                        if(mouseCode == MouseCode.Left)
                        {
                            mouseCode = MouseCode.Right;
                        }
                        else
                        {
                            mouseCode = MouseCode.Left;
                        }
                        break;
                    case 1:
                        if (mouseCode == MouseCode.Right)
                        {
                            mouseCode = MouseCode.Left;
                        }
                        else
                        {
                            mouseCode = MouseCode.Right;
                        }
                        break;
                }
                break;
        }
    }

    public static bool AllPushMouseKey() { return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2); }
}
