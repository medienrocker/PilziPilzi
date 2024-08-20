using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMap : MonoBehaviour
{
    [System.Serializable]
    public class LevelButton {
        public Button button;
        public Image lockIcon;
    }

    [SerializeField] private LevelButton[] levelButtons;

    private void Start() {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        for (int i = 0; i < levelButtons.Length; i++) {
            int levelNumber = i + 1;
            levelButtons[i].button.onClick.AddListener(() => OnLevelButtonClicked(levelNumber));

            if (levelNumber <= currentLevel + 1) {
                levelButtons[i].button.interactable = true;
                levelButtons[i].lockIcon.gameObject.SetActive(false);
            }
            else {
                levelButtons[i].button.interactable = false;
                levelButtons[i].lockIcon.gameObject.SetActive(true);
            }
        }
    }

    private void OnLevelButtonClicked(int levelNumber) {
        LevelManager.Instance.LoadLevel(levelNumber);
    }
}