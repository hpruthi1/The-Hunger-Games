using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI CountDownTime;
    public float currentTime=0;
    float startingTime=60f;
    public bool timerIsActive =true;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        if (timerIsActive)
        {
            currentTime -= 1 * Time.deltaTime;
            CountDownTime.text = currentTime.ToString("0");

            if (currentTime <= 0 )
            {
                currentTime = 0.0f;
                timerIsActive = false;
            }

            if (currentTime < 3.5f)
            {
                CountDownTime.color = Color.red;
            }

        }
    }
}
