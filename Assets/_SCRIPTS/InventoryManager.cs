using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<CollectibleType, int> inventory = new Dictionary<CollectibleType, int>();

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

        // Initialize inventory
        inventory[CollectibleType.Coin] = 0;
        inventory[CollectibleType.Star] = 0;
    }

    public void AddCollectible(CollectibleType type, int amount)
    {
        inventory[type] += amount;
        UIManager.Instance.UpdateCollectibleDisplay(type, inventory[type]);
    }

    public int GetCollectibleCount(CollectibleType type)
    {
        return inventory[type];
    }
}