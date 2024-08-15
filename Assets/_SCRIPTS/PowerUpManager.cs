using UnityEngine;
using System.Collections;

public enum PowerUpType
{
    DoubleJump
}

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

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

    public void ActivatePowerUp(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.DoubleJump:
                StartCoroutine(ActivateDoubleJump());
                break;
        }
    }

    private IEnumerator ActivateDoubleJump()
    {
        Debug.Log("Double Jump is activated!");
        // // Enable double jump
        // PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        // if (playerMovement != null)
        // {
        //     playerMovement.EnableDoubleJump();
        // }

        // // Wait for 10 seconds
        yield return new WaitForSeconds(10f);

        // // Disable double jump
        // if (playerMovement != null)
        // {
        //     playerMovement.DisableDoubleJump();
        // }
    }
}