using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEndGAme : MonoBehaviour
{

    private int currentLevel;
    [SerializeField] private string transitionSceneName = "TransitionScene";
    [SerializeField] private string roadmapSceneName = "LevelSelect";

    private void Start()
    {
        currentLevel = 1;
    }

    public void GoToRoadmap()
    {
        LoadSceneWithTransition(roadmapSceneName);
    }

    public void LoadLevel(int levelNumber)
    {
        if (levelNumber > 0)
        {
            LoadSceneWithTransition("Level" + levelNumber);
        }
    }

    private void LoadSceneWithTransition(string sceneName)
    {
        PlayerPrefs.SetString("TargetSceneName", sceneName);
        SceneManager.LoadScene(transitionSceneName, LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            // Exit Playmode in the Unity Editor
            EditorApplication.isPlaying = false;
        #else
            // Quit the application
            Application.Quit();
        #endif
    }
}
