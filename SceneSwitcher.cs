using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void Switch(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void RestartCurrentLevel()
    {
        Game.state = GameState.IN_PROGRESS;
        Game.ResetStaticVariables();
        Switch(SceneManager.GetActiveScene().name);
    }
}
