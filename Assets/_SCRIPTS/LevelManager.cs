using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private int totalLevels;
    [SerializeField] private string roadmapSceneName = "LevelSelect";
    [SerializeField] private string transitionSceneName = "TransitionScene";

    private int currentLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    public void RestartLevel()
    {
        LoadSceneWithTransition(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel > totalLevels)
        {
            currentLevel = 1;
        }
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        LoadLevel(currentLevel);
    }

    public void GoToRoadmap()
    {
        LoadSceneWithTransition(roadmapSceneName);
    }

    public void LoadLevel(int levelNumber)
    {
        if (levelNumber > 0 && levelNumber <= totalLevels)
        {
            LoadSceneWithTransition("Level" + levelNumber);
        }
        else
        {
            Debug.LogError("Invalid level number: " + levelNumber);
        }
    }

    private void LoadSceneWithTransition(string sceneName)
    {
        PlayerPrefs.SetString("TargetSceneName", sceneName);
        SceneManager.LoadScene(transitionSceneName, LoadSceneMode.Additive);
    }
}

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private int levelNumber;

    public void OnClick()
    {
        LevelManager.Instance.LoadLevel(levelNumber);
    }
}
