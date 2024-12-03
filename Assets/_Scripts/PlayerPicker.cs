using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicker : MonoBehaviour
{
    GameObject[] allWaypoints;
    Transform selectedPoint;
    bool isPicking = true;
    private void Start()
    {
        allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isPicking)
        {
            isPicking = false;
            Debug.Log($"{other.gameObject.tag} Enter");
            int randomIndex = Random.Range(0, allWaypoints.Length - 1);
            selectedPoint = allWaypoints[randomIndex].gameObject.transform;
            Vector3 rebornPoint = new Vector3(selectedPoint.position.x, 10f, selectedPoint.position.z);
            Debug.Log($"Reborn at {rebornPoint}");
            other.gameObject.GetComponent<PlayerController>().PlayerPicker(rebornPoint);
            Debug.Log($"reach");
            Invoke("DelayPicker", 0.1f);
        }
    }

    void DelayPicker()
    {
        isPicking = true;
    }
}
