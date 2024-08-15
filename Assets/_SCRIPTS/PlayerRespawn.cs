using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform levelStartRespawnPoint;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip healingSound;
    [SerializeField] private AudioClip looseHeartSound;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float playerInWaterDuration = 1f;
    [SerializeField] private float deathAnimationDuration = 2f;
    [SerializeField] private float brockenHeartSoundDelayDuration = 0.7f;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject healingParticles;
    [SerializeField] private float respawnAnimationDuration = 2f;
    [SerializeField] private int startingLives = 3;
    [SerializeField] private MonoBehaviour[] componentsToDisableOnWin;

    private Transform respawnPoint;
    private AudioSource audioSource;
    private bool isRespawning = false;
    private int lives;

    [Header("Winning")]
    [SerializeField] private AudioClip winningSound;
    [SerializeField] private AudioClip winningMusic;
    [SerializeField] private GameObject winningParticles;
    [SerializeField] private float winningAnimationDuration = 3f;
    [SerializeField] private TextMeshProUGUI winText;

    private bool isLevelComplete = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (deathText != null)
        {
            deathText.gameObject.SetActive(false);
        }

        if (deathParticles != null)
        {
            deathParticles.SetActive(false);
        }

        if (healingParticles != null)
        {
            healingParticles.SetActive(false);
        }

        lives = startingLives;
        UpdateLivesDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathCollider") && !isRespawning)
        {
            StartCoroutine(DeathSequence());
        }
        else if (other.CompareTag("LifePickup"))
        {
            GainLife();
            Destroy(other.gameObject);
        }
    }

    public void UpdateRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }

    private IEnumerator DeathSequence()
    {
        isRespawning = true;

        // Ensure respawn point is set
        if (respawnPoint == null)
        {
            Debug.LogWarning("No respawn point set. Using levelStartRespawnPoint position.");
            respawnPoint = levelStartRespawnPoint;
        }

        // Decrease life
        lives--;
        UpdateLivesDisplay();

        // Check if game over
        if (lives <= 0)
        {
            GameOver();
            yield break;
        }

        // 1. Play death sound
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Show death text
        if (deathText != null)
        {
            deathText.gameObject.SetActive(true);
        }

        // Respawn at the spawn point
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        // Wait for the playerInWater duration
        yield return new WaitForSeconds(playerInWaterDuration);

        // Hide death text
        if (deathText != null)
        {
            deathText.gameObject.SetActive(false);
        }

        // 3. Play death animation and particles
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Death");
        }

        if (deathParticles != null)
        {
            deathParticles.SetActive(true);
        }

        // Wait for the playerInWater duration
        yield return new WaitForSeconds(brockenHeartSoundDelayDuration);

        // Play looseHeartSound sound
        if (looseHeartSound != null)
        {
            audioSource.PlayOneShot(looseHeartSound);
        }

        // Wait for the death animation duration
        yield return new WaitForSeconds(deathAnimationDuration);

        // Stop death particles
        if (deathParticles != null)
        {
            deathParticles.SetActive(false);
        }

        // Play respawn animation and healing particles
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Respawn");
        }

        if (healingParticles != null)
        {
            healingParticles.SetActive(true);
        }

        // Play healing sound
        if (healingSound != null)
        {
            audioSource.PlayOneShot(healingSound);
        }

        // Wait for respawn animation
        yield return new WaitForSeconds(respawnAnimationDuration);

        // Stop healing particles
        if (healingParticles != null)
        {
            healingParticles.SetActive(false);
        }

        isRespawning = false;
    }

    private void UpdateLivesDisplay()
    {
        if (livesText != null)
        {
            livesText.text = lives.ToString();
        }
    }

    private void GainLife()
    {
        lives++;
        UpdateLivesDisplay();
        // You could add a sound effect or particle effect here for gaining a life
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        // Implement game over logic here (e.g., show game over screen, restart level, etc.)
    }

    // Disable components when winning (e.g. movement...)
    private void DisableComponents()
    {
        foreach (MonoBehaviour component in componentsToDisableOnWin)
        {
            if (component != null)
            {
                component.enabled = false;
            }
        }
    }

    // WINNING Conditions
    public void LevelComplete()
    {
        if (!isLevelComplete)
        {
            isLevelComplete = true;
            DisableComponents();
            StartCoroutine(WinningSequence());
        }
    }

    private IEnumerator WinningSequence()
    {
        // Play winning sound
        if (winningSound != null)
        {
            audioSource.PlayOneShot(winningSound);
        }

        // Show winning text
        if (winText != null)
        {
            winText.gameObject.SetActive(true);
        }

        // Play winning animation
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Win");
        }

        // Play winning particles
        if (winningParticles != null)
        {
            winningParticles.SetActive(true);
        }

        // Wait for the winning animation duration
        yield return new WaitForSeconds(winningAnimationDuration);

        // Play winning music
        if (winningMusic != null)
        {
            audioSource.Stop(); // Stop any currently playing music
            audioSource.clip = winningMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Here you would typically show the "Next Level" button or end game screen
        Debug.Log("Level Complete! Show 'Next Level' button or end game screen here.");
    }
}