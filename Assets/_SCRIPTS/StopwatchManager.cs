using UnityEngine;
using System;

public class StopwatchManager : MonoBehaviour
{
    public static StopwatchManager Instance;

    private float elapsedTime;
    private bool isRunning;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UIManager.Instance.UpdateTimerDisplay(FormatTimeMinutesSeconds(elapsedTime));
        }
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

    public void CompareWithBestTime() {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (elapsedTime < bestTime) {
            PlayerPrefs.SetFloat("BestTime", elapsedTime);
            PlayerPrefs.Save();
            Debug.Log("New best time: " + FormatTimeWithMilliseconds(elapsedTime));
        }
        else {
            Debug.Log("Current best time: " + FormatTimeWithMilliseconds(bestTime));
        }

        UIManager.Instance.DisplayFinalTimes(elapsedTime, bestTime);
    }

    public string FormatTimeMinutesSeconds(float timeInSeconds) {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60F);
        int seconds = Mathf.FloorToInt(timeInSeconds - minutes * 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string FormatTimeWithMilliseconds(float timeInSeconds)
    {
        // Clamp the value to a reasonable range
        if (timeInSeconds > TimeSpan.MaxValue.TotalSeconds)
        {
            Debug.LogWarning("Elapsed time is too large, clamping to TimeSpan.MaxValue.");
            timeInSeconds = (float)TimeSpan.MaxValue.TotalSeconds;
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        return string.Format("{0:D2}:{1:D2}.{2:D2}",
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds / 10);
    }


    public float GetElapsedTime() {
        return elapsedTime;
    }

    public void ResetStopwatch() {
        isRunning = false;
        elapsedTime = 0f;
        UIManager.Instance.UpdateTimerDisplay(FormatTimeMinutesSeconds(elapsedTime));
    }
}