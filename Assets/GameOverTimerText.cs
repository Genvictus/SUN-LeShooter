using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTimerText : MonoBehaviour
{
    [SerializeField] float remainingTime = 15f;

    public Text timerTextObj;

    void Awake()
    {
        EventManager.StartListening("ResetCountdown", ResetCountdown);
    }

    void OnDestroy()
    {
        EventManager.StopListening("ResetCountdown", ResetCountdown);
    }

    void ResetCountdown()
    {
        remainingTime = 15f;
        enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0 && enabled)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Math.Max(0, remainingTime);
            timerTextObj.text = Math.Ceiling(remainingTime).ToString();
        }
        else if (enabled)
        {
            enabled = false;
        }
    }
}
