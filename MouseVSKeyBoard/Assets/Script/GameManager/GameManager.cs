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

    public enum GameState
    {
        Null = -1,
        Title,
        Game,
        Result,
        DataEnd
    }
    private static GameState gameState = GameState.Null;
    public static GameState GameStateTag { get {return gameState; } set {gameState = value; } }
}
