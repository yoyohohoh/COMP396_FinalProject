using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{
    public float spinSpeed = 100.0f; // Speed of the spin in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
