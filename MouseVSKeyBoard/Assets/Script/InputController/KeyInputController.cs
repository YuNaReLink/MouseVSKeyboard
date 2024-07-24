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

    public void SetPressKey()
    {
        int num = Random.Range(0, 3);
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
    }

    public static bool AllPushKey() { return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D); }

    private static MouseCode mouseCode = MouseCode.Null;
    public static MouseCode GetMouseCode() { return mouseCode; }

    public bool RandomMouseKey() { return Input.GetMouseButtonDown((int)mouseCode); }

    public void SetPressMouseKey()
    {
        int num = Random.Range(0, 2);
        mouseCode = (MouseCode)num;
    }

    public static bool AllPushMouseKey() { return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2); }
}
