using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINotify : Singleton<UINotify>
{
    public GameObject notifyWindow;
    public TMP_Text timeRemainingText;
    public TMP_Text waveCountText;
    public Animator animWaveCountText;
    public Animator animTreasureSpawnText;
    public Animator animDieText;

    private AudioSource _source;
    void Start()
    {
        _source = GetComponent<AudioSource>();
        timeRemainingText.gameObject.SetActive(false);
    }

    public void SetShowTimeRemaining(float time)
    {
        if (GameManager.Instance.playerCharacter.GetComponent<PlayerHealth>().isDead)
        {
            timeRemainingText.gameObject.SetActive(false);
            return;
        }

        if (!timeRemainingText.gameObject.activeInHierarchy)
            timeRemainingText.gameObject.SetActive(true);

        timeRemainingText.text = "Time Remaining : " + time.ToString("F2");
    }

    public void ShowWaveCount(int currentWave)
    {
        if (GameManager.Instance.playerCharacter.GetComponent<PlayerHealth>().isDead)
            return;

        waveCountText.text = "Wave " + currentWave.ToString();
        animWaveCountText.SetTrigger("isActivate");

        _source.Play();
    }

    public void ShowClearWave()
    {
        if (GameManager.Instance.playerCharacter.GetComponent<PlayerHealth>().isDead)
            return;

        timeRemainingText.gameObject.SetActive(false);

        animTreasureSpawnText.SetTrigger("isActivate");
    }

    public void ShowDie()
    {
        animDieText.SetTrigger("isActivate");
    }
}
