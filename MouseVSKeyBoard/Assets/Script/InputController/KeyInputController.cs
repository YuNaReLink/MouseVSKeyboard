using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                key = KeyCode.W;
                break;
            case 1:
                key = KeyCode.A;
                break;
            case 2:
                key = KeyCode.S;
                break;
            case 3:
                key = KeyCode.D;
                break;
        }
    }
}
