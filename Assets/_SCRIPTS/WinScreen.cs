using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button nextLevelButton;

    private void Start()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        nextLevelButton.onClick.AddListener(OnNextLevelClicked);
    }

    private void OnPlayAgainClicked()
    {
        LevelManager.Instance.RestartLevel();
    }

    private void OnNextLevelClicked()
    {
        LevelManager.Instance.NextLevel();
    }
}
