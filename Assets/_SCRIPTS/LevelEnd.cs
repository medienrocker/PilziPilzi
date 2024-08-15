using UnityEngine;
using UnityEngine.Events;

public class LevelEnd : MonoBehaviour
{
    public UnityEvent onLevelComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onLevelComplete.Invoke();
        }
    }
}
