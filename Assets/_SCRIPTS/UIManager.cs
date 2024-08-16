using UnityEngine;
using TMPro; // Add this for TextMeshPro

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI coinText; // Change to TextMeshProUGUI
    public TextMeshProUGUI starText; // Change to TextMeshProUGUI

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        if (coinText != null) {
            coinText.text = "0";
        }

        if (starText != null) {
            starText.text = "0";
        }
    }

    public void UpdateCollectibleDisplay(CollectibleType type, int amount) {
        switch (type) {
            case CollectibleType.Coin:
                coinText.SetText("{0}", amount); // Use SetText for better performance
                break;
            case CollectibleType.Star:
                starText.SetText("{0}", amount); // Use SetText for better performance
                break;
        }
    }
}