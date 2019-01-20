using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreAndCashManager : MonoBehaviour
{
    public static Text scoreToDisplay;
    public static Text cashToDisplay;

    private static int score;
    private static float cash;

    public static int Score
    {
        get
        {
            return score;
        }

        set
        {
            
            score = value;
            scoreToDisplay.text = string.Format("Score: {0}", value);
        }
    }

    public static float Cash
    {
        get
        {
            return cash;
        }

        set
        {
            cash = value;
            cashToDisplay.text = string.Format("Cash: {0}", (int)Cash);
        }
    }

    // Use this for initialization
    void Awake ()
    {
        Game.gameEnded += OnGameEnded;
        scoreToDisplay = GameObject.FindGameObjectWithTag("ScoreDisplay").GetComponent<Text>();
        cashToDisplay = GameObject.FindGameObjectWithTag("CashDisplay").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleCheats();
	}

    void OnGameEnded()
    {
        int highscore = 0;
        if (PlayerPrefs.HasKey("highscore"))
            highscore = PlayerPrefs.GetInt("highscore");
        if (score > highscore)
        {
            Debug.Log("Saving new highscore: " + score);
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
        }
    }

    void HandleCheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Score += 100;
            Cash += 10;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Player p = Player.instance;
            p.Health += 10f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Player p = Player.instance;
            p.ScrapAmount += 10;
        }


    }
}
