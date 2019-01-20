using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public static class Game
{
    public static GameState state;
    public static Action gameEnded;

    static Game()
    {
        state = GameState.IN_PROGRESS;
    }

    public static bool IsGameOver()
    {
        return state == GameState.GAME_OVER;
    }

    public static void ResetStaticVariables()
    {
        ScoreAndCashManager.Score = 0;
        Enemy.count = 0;
        PlayerTurret.count = 0;
        ScoreAndCashManager.Cash = 0;
        PlayerPerksManager.ResetPerks();
    }

    public static void PlayerDied()
    {
        if (gameEnded != null)
            gameEnded();
    }
}
