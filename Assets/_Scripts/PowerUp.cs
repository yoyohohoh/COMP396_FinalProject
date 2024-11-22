using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            int randomProbability = Random.Range(0, 100);
            if(randomProbability < 30)
            {
                Debug.Log("PowerUp: Protect");
                other.gameObject.GetComponent<PlayerController>().powerUpItemTxt.text = "Protect";
            }
            else
            {
                Debug.Log("PowerUp: Speed");
                other.gameObject.GetComponent<PlayerController>().powerUpItemTxt.text = "Speed";
            }

            Destroy(gameObject);
            
        }
    }
}
