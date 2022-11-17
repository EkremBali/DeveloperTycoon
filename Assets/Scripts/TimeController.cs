using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;



[System.Serializable]
public class GameTime
{
    public int year;
    public int mounth;
    public int week;
    public int day;
}


public class TimeController : MonoBehaviour
{
    public GameTime time;

    //Date text and time pause button text
    public TextMeshProUGUI date;
    public TextMeshProUGUI pauseButtonText;

    // Is game pause properties
    public bool isGamePaused { get; private set; }

    void Awake()
    {
        date.text = "Y: " + time.year + " M: " + time.mounth + " W: " + time.week + " D: " + time.day;
    }

    void Start()
    {
        StartCoroutine("StartGameTime");
        PauseTime();
    }

    //Time Counter
    IEnumerator StartGameTime()
    {
        while (!isGamePaused)
        {
            yield return new WaitForSeconds(.5f);

            time.day++;
            if (time.day == 8)
            {
                time.day = 1;
                time.week++;

                if (time.week == 5)
                {
                    time.week = 1;
                    time.mounth++;

                    if (time.mounth == 13)
                    {
                        time.mounth = 1;
                        time.year++;
                    }
                }
            }

            date.text = "Y: " + time.year + " M: " + time.mounth + " W: " + time.week + " D: " + time.day;
        }

    }

    //Pause Button Clicked
    public void PauseTime()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            StopCoroutine("StartGameTime");
            pauseButtonText.text = "Continue";
        }
        else
        {
            StartCoroutine("StartGameTime");
            pauseButtonText.text = "Pause";
        }
        
    }



}
