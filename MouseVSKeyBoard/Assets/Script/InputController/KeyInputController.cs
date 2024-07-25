using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�E�X�p�̃^�O
/// </summary>
public enum MouseCode
{
    Null = -1,
    Left,
    Right,
    Middle,
    DataEnd
}

public enum PushKeyCode
{
    Null = -1,
    A,
    S,
    D,
    DataEnd
}

/// <summary>
/// �v���C���[�����͂���L�[��}�E�X�{�^���̏���ݒ肷��N���X
/// </summary>
public class InputController
{
    
    private static KeyCode key = KeyCode.None;
    public static KeyCode GetKeyTag() {  return key; }
    public bool RandomGetKey() { return Input.GetKeyDown(key); }

    //���[�h�ɂ���ăL�[�̓��͂�ݒ肷��
    public void SetPushKey(GameManager.GameMode _mode)
    {
        int num = 0;
        switch (_mode)
        {
            case GameManager.GameMode.RapidPress:
                num = Random.Range(0, (int)PushKeyCode.DataEnd);
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

    private static KeyCode[] keyCodeArray = new KeyCode[4];
    public static KeyCode[] GetKeyCodeArray() { return keyCodeArray; }
    private static bool[] pushKeyFlag = new bool[4];
    public static bool[] GetPushKeyFlag() { return pushKeyFlag; }
    public void SetTypingKeyCode()
    {
        int num = 0;
        for (int i = 0;i < keyCodeArray.Length; i++)
        {
            num = Random.Range(0, (int)PushKeyCode.DataEnd);
            KeyCode key = KeyCode.None;
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
            keyCodeArray[i] = key;
            pushKeyFlag[i] = false;
        }
    }


    private static MouseCode mouseCode = MouseCode.Null;
    public static MouseCode GetMouseCode() { return mouseCode; }

    public bool RandomMouseKey() { return Input.GetMouseButtonDown((int)mouseCode); }
    //���[�h�ɂ���ă}�E�X�{�^����ݒ肷��
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

    private static bool[] pushClickFlag = new bool[3];
    public static bool[] GetPushClickFlag() { return pushClickFlag; }
}
