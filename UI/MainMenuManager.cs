using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public Text highscoreValue;
    public GameObject helpWindow;
	// Use this for initialization
	void Awake ()
    {
        if (PlayerPrefs.HasKey("highscore"))
            highscoreValue.text = PlayerPrefs.GetInt("highscore").ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void PlayNewGame()
    {
        SceneManager.LoadScene("mainLevel");
    }

    public void OpenHelpWindow()
    {
        helpWindow.SetActive(true);
    }

    public void CloseHelpWindow()
    {
        helpWindow.SetActive(false);
    }
}
