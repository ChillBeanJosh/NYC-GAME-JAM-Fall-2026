using UnityEngine;

public class RoundTrigger : MonoBehaviour
{
    [SerializeField] private PositionLogger logger;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            logger.CompleteTestRound();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasTriggered = false;
        }
    }
}
