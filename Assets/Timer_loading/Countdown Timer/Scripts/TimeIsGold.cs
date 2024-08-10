using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeIsGold : MonoBehaviour
{
    #region Variables
    [Header("Current Date/Time")]
    public TextMeshProUGUI currentTime;
    public TextMeshProUGUI currentDate;

    [Header("Countdown Setting")]
    public float startTimeCountdown = 10;
    public float endTimeCountdown = 1;
    public TextMeshProUGUI countdownTMP;
    public TextMeshProUGUI countdownProgressTMP;
    public Slider countdownSlider;
    private bool canCountdown = false;

    private float resetCountdownValue;

    [Header("StopWatch Setting")]
    public float startTimeStopWatch = 1;
    public float endTimeStopWatch = 10;
    public TextMeshProUGUI stopWatchTMP;
    public TextMeshProUGUI stopwatchProgressTMP;
    public Slider stopWatchSlider;
    private bool stopwatchCount = false;
    private float resetStopWatchValue;

    #endregion Variables

    private void Start()
    {
        resetCountdownValue = startTimeCountdown;
        resetStopWatchValue = startTimeStopWatch;
    }

    // Update is called once per frame
    private void Update()
    {
        Countdown();
        StopWatch();
        RealTime();
    }

    public void RealTime()
    {
        currentTime.text = System.DateTime.Now.ToString("hh:mm:ss tt");
        currentDate.text = System.DateTime.Now.ToString(" MMMM dd, yyyy");
        // https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
    }

    public void Countdown()
    {
        // Time Countdown (-)
        if (startTimeCountdown >= (endTimeCountdown) && canCountdown.Equals(true))
        {
            startTimeCountdown -= 1 * Time.deltaTime;
            countdownTMP.text = Mathf.FloorToInt(startTimeCountdown).ToString();
        }
        else
        {
            // Do Something
            countdownTMP.text = "End Countdown";
            canCountdown = false;
        }

        var countdownProgress = Mathf.Clamp01(startTimeCountdown / resetCountdownValue);
        countdownSlider.value = countdownProgress;
        countdownProgressTMP.text = (countdownProgress * 100).ToString("0") + "%";
    }

    public void ResetCountdown()
    {
        canCountdown = true;
        startTimeCountdown = resetCountdownValue;
    }

    public void StartCountdown()
    {
        if (canCountdown.Equals(false))
        {
            canCountdown = true;
            startTimeCountdown = resetCountdownValue;
        }
    }

    public void StopWatch()
    {
        // Stopwatch (+)
        if (startTimeStopWatch <= endTimeStopWatch && stopwatchCount.Equals(true))
        {
          
            startTimeStopWatch += Time.deltaTime;
            stopWatchTMP.text = Mathf.FloorToInt(startTimeStopWatch).ToString();
        }
        else
        {
            // Do Something
            stopWatchTMP.text = "End Stopwatch";
            stopwatchCount = false;
        }

        var stopwatchProgress = Mathf.Clamp01((startTimeStopWatch / endTimeStopWatch));
        stopWatchSlider.value = stopwatchProgress;
        stopwatchProgressTMP.text = (stopwatchProgress * 100).ToString("0") + "%";
    }

    public void ResetStopwatch()
    {
        stopwatchCount = true;
        startTimeStopWatch = resetStopWatchValue;
    }

    public void StartStopwatch()
    {
        if (stopwatchCount.Equals(false))
        {
            stopwatchCount = true;
            startTimeStopWatch = resetStopWatchValue;
        }
    }
}
