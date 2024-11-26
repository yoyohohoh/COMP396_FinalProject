using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float bounceAmplitude = 0.03f; // Height of the bounce
    [SerializeField] private float bounceFrequency = 10f;    // Speed of the bounce

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position of the vehicle
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new Y position with a sine wave
        float bounceOffset = Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;

        // Apply the new position while maintaining the original X and Z
        transform.localPosition = new Vector3(
            originalPosition.x,
            originalPosition.y + bounceOffset,
            originalPosition.z
        );
    }
}
