using UnityEngine;
using TMPro;
using System;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    private float elapsedTime;
    private bool isRunning;

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        timerText.text = string.Format("{0:D2}:{1:D2}", 
            timeSpan.Minutes, 
            timeSpan.Seconds, 
            timeSpan.Milliseconds / 10);
    }

    public void StartStopwatch() {
        isRunning = true;
        elapsedTime = 0f;
    }

    public void StopStopwatch() {
        isRunning = false;
        SaveTime();
        CompareWithBestTime();
    }

    private void SaveTime() {
        PlayerPrefs.SetFloat("LastTime", elapsedTime);
        PlayerPrefs.Save();
    }

    private void CompareWithBestTime() {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        
        if (elapsedTime < bestTime) {
            PlayerPrefs.SetFloat("BestTime", elapsedTime);
            PlayerPrefs.Save();
           // Debug.Log("New best time: " + FormatTime(elapsedTime));
        }
        else {
            //Debug.Log("Current best time: " + FormatTime(bestTime));
        }
    }

    private string FormatTime(float timeInSeconds) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}.{2:D2}", 
            timeSpan.Minutes, 
            timeSpan.Seconds, 
            timeSpan.Milliseconds / 10);
    }

    // Shows the current Time and the Best Time that was arrived.
    public void DisplayFinalTimes() {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        currentTimeText.text = "Meine Zeit: " + FormatTime(elapsedTime);
        bestTimeText.text = "Letzte Bestzeit: " + FormatTime(bestTime);
    }
}
