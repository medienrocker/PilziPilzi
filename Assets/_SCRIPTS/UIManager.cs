using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI bestTimeText;
    public Image currentTimePokalImage;
    public Image bestTimePokalImage;
    public TextMeshProUGUI newHighscoreText;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeUI();
    }

    private void InitializeUI() {
        if (coinText != null) coinText.text = "0";
        if (starText != null) starText.text = "0";
        if (timerText != null) timerText.text = "00:00";
        if (currentTimePokalImage != null) currentTimePokalImage.gameObject.SetActive(false);
        if (bestTimePokalImage != null) bestTimePokalImage.gameObject.SetActive(false);
        if (newHighscoreText != null) newHighscoreText.gameObject.SetActive(false);
    }

    public void UpdateCollectibleDisplay(CollectibleType type, int amount)
    {
        switch (type) {
            case CollectibleType.Coin:
                coinText.SetText("{0}", amount);
                break;
            case CollectibleType.Star:
                starText.SetText("{0}", amount);
                break;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs reset");

        // Optionally, update UI elements that display best times
        UpdateBestTimeDisplay();
    }

    private void UpdateBestTimeDisplay()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        if (bestTimeText != null)
        {
            bestTimeText.text = StopwatchManager.Instance.FormatTimeWithMilliseconds(bestTime);
        }
    }

    public void UpdateTimerDisplay(string time) {
        if (timerText != null) {
            timerText.text = time;
        }
    }

    public void DisplayFinalTimes(float currentTime, float bestTime) {
        if (currentTimeText != null)
            currentTimeText.text = "Meine Zeit: " + StopwatchManager.Instance.FormatTimeWithMilliseconds(currentTime);

        if (bestTimeText != null) {
            if (StopwatchManager.Instance.FormatTimeWithMilliseconds(bestTime) != null) {
                bestTimeText.text = "Letzte Bestzeit: " + StopwatchManager.Instance.FormatTimeWithMilliseconds(bestTime);
            } else {
                bestTimeText.text = "Letzte Bestzeit: " + StopwatchManager.Instance.FormatTimeWithMilliseconds(currentTime);
            }
        }

        if (currentTime < bestTime) {
            ShowCurrentTimePokal();
            HideBestTimePokal();
            ShowNewHighscoreText();
        }
        else {
            HideCurrentTimePokal();
            ShowBestTimePokal();
            HideNewHighscoreText();
        }
    }

    private void ShowCurrentTimePokal() {
        if (currentTimePokalImage != null)
        {
            currentTimePokalImage.gameObject.SetActive(true);
        }
    }

    private void HideCurrentTimePokal() {
        if (currentTimePokalImage != null)
        {
            currentTimePokalImage.gameObject.SetActive(false);
        }
    }

    private void ShowBestTimePokal() {
        if (bestTimePokalImage != null)
        {
            bestTimePokalImage.gameObject.SetActive(true);
        }
    }

    private void HideBestTimePokal() {
        if (bestTimePokalImage != null)
        {
            bestTimePokalImage.gameObject.SetActive(false);
        }
    }

    private void ShowNewHighscoreText() {
        if (newHighscoreText != null)
        {
            newHighscoreText.gameObject.SetActive(true);
        }
    }

    private void HideNewHighscoreText() {
        if (newHighscoreText != null)
        {
            newHighscoreText.gameObject.SetActive(false);
        }
    }
}