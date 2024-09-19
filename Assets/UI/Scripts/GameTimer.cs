using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public int minutes;
    public int seconds;
    public TextMeshProUGUI timerUI;

    public bool stopTimer = false;

    private void Start()
    {
        StartCoroutine(Time());
    }

    private void Update()
    {
        if (stopTimer)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Time()
    {

        while (true)
        {
            yield return new WaitForSeconds(1f);
            seconds += 1;
            if (seconds == 60)
            {
                minutes += 1;
                seconds = 0;
            }
            timerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }
    }
}
