using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum State
{
    Ready = 0,
    Play = 1,
    Pause = 2,
    GameOver = 3
}

public class GameController : MonoBehaviour {

    //ForGameState
    public State state;

    public HumanController human;
    public GameObject blocks;
    public Text scoreLabel;
    public Text stateLabel;
    public GameObject Pause;
    public GameObject PauseBackGame;
    public Button BackGameButton;
    public bool StateGC;
    public static bool TweetBool;

    //ForScore
    int score;
    int scoreForRanking;
    int scoreForRankingTmp;

    public static string RankingPref;
    public static int RankingNum = 5;
    public static int[] Ranking;


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
        getRanking();
        setRanking();
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


        getRanking();
        setRanking();

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

    void getRanking()
    {
        string originalRanking = PlayerPrefs.GetString(RankingPref, "0,0,0,0,0");

        if (originalRanking.Length > 0)
        {
            string[] RankingScore = originalRanking.Split(","[0]);

            Ranking = new int[RankingNum];

            for (int k = 0; k < originalRanking.Length && k < RankingNum; k++)
            {
                Ranking[k] = int.Parse(RankingScore[k]);
            }
        }
    }

    void setRanking()
    {
        scoreForRanking = score;

        for (int m = 0; m < 5; m++)
        {
            if (scoreForRanking > Ranking[m])
            {
                scoreForRankingTmp = Ranking[m];

                Ranking[m] = scoreForRanking;

                scoreForRanking = scoreForRankingTmp;
            }

        }

        PlayerPrefs.SetString(RankingPref, Ranking[0].ToString() + "," + Ranking[1].ToString() + "," + Ranking[2].ToString() + "," + Ranking[3].ToString() + "," + Ranking[4].ToString());

        score = 0;
    }


}
