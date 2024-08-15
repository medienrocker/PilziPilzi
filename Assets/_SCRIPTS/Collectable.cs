using UnityEngine;

public enum CollectibleType
{
    Coin,
    Star
}

public class Collectible : MonoBehaviour
{
    public CollectibleType type;
    public int value = 1;
    public GameObject collectEffect;
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        // Trigger sound
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        // Spawn particle effect
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        // Notify player of collection
        player.GetComponent<PlayerCollector>().CollectItem(this);

        // Destroy the collectible
        Destroy(gameObject);
    }
}
