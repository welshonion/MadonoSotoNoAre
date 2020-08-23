﻿using UnityEngine;

public class ScrollObject : MonoBehaviour {
    
    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;
    public float firstPosition;  

    public GameController ControllerSO;
    public GameObject ObjectSO;
    bool StateSO;

    public double acceltime;
    double firsttime;

    private void Start()
    {

        ObjectSO = GameObject.Find("GameController");
        if (ObjectSO != null)
        {
            ControllerSO = ObjectSO.GetComponent<GameController>();
        }
        else
        {
            StateSO = false;
        }


        transform.Translate(firstPosition, 0, 0);
    }

    // Update is called once per frame
    void Update () {

        transform.Translate(-1 * speed * Time.deltaTime * ((float)acceltime + (float)firsttime), 0, 0);

        if (transform.position.x <= endPosition) ScrollEnd();

        if (ControllerSO != null)
        {
            StateSO = ControllerSO.GetComponent<GameController>().StateGC;
        }

        //       Debug.Log(accelbool);

        if (StateSO == true && firsttime <= 0.6)
        {
            firsttime += 0.2 * Time.deltaTime;
        }
        else if(StateSO == true)
        {
            acceltime += 0.01 * Time.deltaTime;
        }
        else{
            acceltime = 0;
        }


    }

    void ScrollEnd()
    {

        transform.Translate(-1 * (endPosition - startPosition), 0, 0);

        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);

    }

}
