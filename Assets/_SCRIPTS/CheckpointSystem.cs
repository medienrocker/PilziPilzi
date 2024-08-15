using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] private PlayerRespawn playerRespawn;

    private void Start()
    {
        if (playerRespawn == null)
        {
            playerRespawn = FindObjectOfType<PlayerRespawn>();
        }

        if (playerRespawn == null)
        {
            Debug.LogError("PlayerRespawn script not found. Please assign it in the inspector or ensure it exists in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateRespawnPoint(transform);
        }
    }

    private void UpdateRespawnPoint(Transform newRespawnPoint)
    {
        if (playerRespawn != null)
        {
            playerRespawn.UpdateRespawnPoint(newRespawnPoint);
        }
    }
}
