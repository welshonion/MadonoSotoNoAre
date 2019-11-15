﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScript : MonoBehaviour {

    AudioSource LoopSource;

    public AudioClip AudioTrain;

    double BGMDelay;
    public double DecideDelayTime;
    bool NowPlay;

    public GameController ControllerRS;
    public GameObject ObjectRS;
    bool StateRS;

    public bool HumanStateRS;

    // Use this for initialization
    void Start () {

        LoopSource = GetComponent<AudioSource>();
        LoopPause();

        HumanStateRS = false;

        ObjectRS = GameObject.Find("GameController");
        ControllerRS = ObjectRS.GetComponent<GameController>();

    }
	
	// Update is called once per frame
	void Update () {

        StateRS = ControllerRS.GetComponent<GameController>().StateGC;
        BGMDelay += Time.deltaTime;

        if (BGMDelay>= DecideDelayTime && NowPlay == false)
        { 
            LoopUnpause();

            HumanStateRS = true;

            NowPlay = true;
        }


        if (StateRS == false)
        {
            LoopPause();

            HumanStateRS = false;
        }
    }

    void LoopUnpause()
    {
        LoopSource.UnPause();
    }

    void LoopPause()
    {
        LoopSource.Pause();
        NowPlay = false;
    }
}
