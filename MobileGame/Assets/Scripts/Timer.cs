using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public sealed class Timer : MonoBehaviour
{
    public float timeLeft = 60;
    public bool timerOn = false;
    public TextMeshProUGUI timeText;
    private void Start()
    {
        // Starts the timer automatically
        timerOn = true;
    }
    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
            }
            else
            {
                
                timeLeft = 0;
                timerOn = false;
            }
        }
        if(timeLeft == 0)
        {
            DOTween.KillAll();
            SceneManager.LoadScene("LoserScreen");
        }
    }
    void DisplayTime(float timeDisplay)
    {
        timeDisplay += 1;
        float mintuets = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}",mintuets, seconds);
    }

}