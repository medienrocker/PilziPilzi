using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public void CollectItem(Collectible collectible)
    {
        // Notify the inventory/score manager
        InventoryManager.Instance.AddCollectible(collectible.type, collectible.value);

        // Check for power-ups
        if (collectible.type == CollectibleType.Star)
        {
            PowerUpManager.Instance.ActivatePowerUp(PowerUpType.DoubleJump);
        }
    }
}