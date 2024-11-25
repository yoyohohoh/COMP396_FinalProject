using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("NPC"))
        {
            Debug.Log($"Reward is generated on {other.tag} at {this.transform.position}");
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().health += 10f; ;

            Destroy(gameObject);

        }


    }
}
