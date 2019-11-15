using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public enum State
    {
        Ready,
        Play,
        GameOver
    }

    public State state;
    int score;

    public HumanController human;
    public GameObject blocks;
    public Text scoreLabel;
    public Text stateLabel;
    public GameObject Pause;
    public GameObject PauseBackGame;
    public Button BackGameButton;
    public bool StateGC;
    public static bool TweetBool;

    int ScoreSpace;
    int RankingSpace;
    int RankingSpace2;


    //Hassyaon
    AudioSource BGMSource;
    public AudioClip AudioStart;


    //   public static bool stopscreen = false;

    // Use this for initialization
    void Start () {
        Ready();
        BGMSource = GetComponent<AudioSource>();
        TweetBool = false;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        switch (state)
        {
            case State.Ready:
                if (Input.GetButtonDown("Fire1")) GameStart();
                break;
            case State.Play:
                if (human.IsDead())
                {
                    GameOver();
                }
                break;

        }
		
	}

    void Ready()
    {
        state = State.Ready;

        human.SetSteerActive(false);
 //       blocks.SetActive(false);

        scoreLabel.text = "Score : " + 0;

        BackGameButton.gameObject.SetActive(false);

        stateLabel.gameObject.SetActive(true);
        stateLabel.text = "Push Start";

        StateGC = false;
        TweetBool = false;

        Time.timeScale = 0;
    }

    void GameStart()
    {
        state = State.Play;

        Time.timeScale = 1;

        human.SetSteerActive(true);
        //        blocks.SetActive(true);

        BGMSource.PlayOneShot(AudioStart);

        stateLabel.gameObject.SetActive(false);
        stateLabel.text = "";

        StateGC = true;
    }

    public void Stop()
    {
        if (state == State.GameOver)
        {
            Retry();
        }
        else
        { 
            stateLabel.gameObject.SetActive(true);
            stateLabel.text = "Pause";
            BackGameButton.gameObject.SetActive(true);

            BGMSource.Pause();
            StateGC = false;

            Time.timeScale = 0;
        }

    }

    public void Backgame()
    {
        Time.timeScale = 1;

        BGMSource.UnPause();
        StateGC = true;

        stateLabel.gameObject.SetActive(false);
        BackGameButton.gameObject.SetActive(false);
    }

    public void Backtitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    void GameOver()
    {
        state = State.GameOver;

        ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject>();

        foreach (ScrollObject so in scrollObjects) so.enabled = false;

        StateGC = false;
        BGMSource.Pause();
        TweetBool = true;

        Pause.gameObject.SetActive(true);
        stateLabel.gameObject.SetActive(true);
        stateLabel.text = "GameOver";

        RankingSpace2 = score;

        for(int m=0; m < 5; m++)
        {
            if (RankingSpace2 > HighScoreSceneScript.Ranking[m])
            {
                RankingSpace = HighScoreSceneScript.Ranking[m];
                
                HighScoreSceneScript.Ranking[m] = RankingSpace2;

                RankingSpace2 = RankingSpace;
            }

        }

        PlayerPrefs.SetString(HighScoreSceneScript.RankingPref, HighScoreSceneScript.Ranking[0].ToString()+","+ HighScoreSceneScript.Ranking[1].ToString() + "," + HighScoreSceneScript.Ranking[2].ToString() + "," + HighScoreSceneScript.Ranking[3].ToString() + "," + HighScoreSceneScript.Ranking[4].ToString());
        
        Invoke("Backtitle", 20.0f);
    }

    public void Retry ()
    {
        Time.timeScale = 1;

        Pause.gameObject.SetActive(false);
        PauseBackGame.gameObject.SetActive(false);
        TweetBool = false;
        //       Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("Game");
    }
    
    public void IncreaseScore()
    {
        score += 100;
        scoreLabel.text = "Score : " + score;
    }

   
}
