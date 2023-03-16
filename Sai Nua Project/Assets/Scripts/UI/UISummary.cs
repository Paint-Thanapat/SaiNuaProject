using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISummary : Singleton<UISummary>
{
    public GameObject summaryWindow;
    public bool lockScore;
    public bool isStartTheGame;

    [Header("Counter")]
    public float timeToSurvive;
    public int gemsTake;
    public int enemyKill;
    public int waveFullClear;
    public int surviveWave;

    [Header("Text")]
    public TMP_Text timeToSurviveText;
    public TMP_Text gemsTakeText;
    public TMP_Text enemyKillText;
    public TMP_Text waveFullClearText;
    public TMP_Text surviveWaveText;

    void Start()
    {
        summaryWindow.SetActive(false);

        gemsTake = 0;
        enemyKill = 0;
        waveFullClear = 0;
        surviveWave = 0;
    }

    void FixedUpdate()
    {
        if (isStartTheGame && !lockScore)
        {
            timeToSurvive += Time.deltaTime;
        }
    }

    public IEnumerator EnableWindow()
    {
        lockScore = true;

        yield return new WaitForSeconds(2f);

        summaryWindow.SetActive(true);

        //Calulate Time

        float currentTime = timeToSurvive;
        int mins = 0;

        for (int i = 60; i < currentTime;)
        {
            mins++;
            currentTime -= 60;
        }

        if (mins <= 9)
        {
            if (currentTime < 10)
            {
                timeToSurviveText.text = "0" + mins.ToString() + ".0" + currentTime.ToString("F2");
            }
            else
            {
                timeToSurviveText.text = "0" + mins.ToString() + "." + currentTime.ToString("F2");
            }
        }
        else
        {
            if (currentTime < 10)
            {
                timeToSurviveText.text = mins.ToString() + ".0" + currentTime.ToString("F2");
            }
            else
            {
                timeToSurviveText.text = mins.ToString() + "." + currentTime.ToString("F2");
            }
        }

        //
        gemsTakeText.text = gemsTake.ToString();
        enemyKillText.text = enemyKill.ToString();
        waveFullClearText.text = waveFullClear.ToString();
        surviveWaveText.text = surviveWave.ToString();
    }

    public void AddGem()
    {
        if (!lockScore)
        {
            gemsTake++;
        }
    }

    public void AddEnemyKill()
    {
        if (!lockScore)
        {
            enemyKill++;
        }
    }

    public void AddWaveFullClear()
    {
        if (!lockScore)
        {
            waveFullClear++;
        }
    }

    public void AddSurviveWave(int currentWave)
    {
        if (!lockScore)
        {
            surviveWave = currentWave;
        }
    }

    public void StartGame()
    {
        isStartTheGame = true;

        timeToSurvive = 0;

        summaryWindow.SetActive(false);

        gemsTake = 0;
        enemyKill = 0;
        waveFullClear = 0;
        surviveWave = 0;
    }
}
