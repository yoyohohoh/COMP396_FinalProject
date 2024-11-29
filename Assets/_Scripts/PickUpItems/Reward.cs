using System.Collections;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public float destroyDelay = 0.2f; // Duration of the animation
    public float upwardDistance = 1.0f; // Distance the object moves upwards

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("NPC"))
        {
            Debug.Log($"Reward is generated on {other.tag} at {this.transform.position}");
            TriggerBlipAnimation();
        }
        else if (other.CompareTag("Player"))
        {
            // Increment the player's health
            other.gameObject.GetComponent<PlayerController>().health += 10f;
            Debug.Log("Player health increased by 10.");
            TriggerBlipAnimation();
        }
    }

    private void TriggerBlipAnimation()
    {
        // Start the combined upward and scaling animation
        StartCoroutine(ShootUpAndScaleDown());
    }

    private IEnumerator ShootUpAndScaleDown()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition + Vector3.up * upwardDistance;
        float elapsedTime = 0f;

        // Animate both upward movement and scaling down
        while (elapsedTime < destroyDelay)
        {
            float progress = elapsedTime / destroyDelay;
            // Lerp position to move upwards
            transform.position = Vector3.Lerp(originalPosition, targetPosition, progress);
            // Lerp scale to shrink
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final state
        transform.position = targetPosition;
        transform.localScale = Vector3.zero;

        Destroy(this.gameObject); // Destroy the object after the animation
    }
}
