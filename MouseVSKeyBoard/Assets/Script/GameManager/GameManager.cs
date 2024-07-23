using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public enum GameMode
    {
        Null = -1,
        RapidPress,
        BurstPush,
        DataEnd
    }

    private static GameMode gameMode = GameMode.Null;
    public static GameMode GameModeTag { get { return gameMode; } set { gameMode = value; } }
}
