using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Path : MonoBehaviour
{
    [SerializeField] public bool isLoop = true;
    [SerializeField] int randomPointAmount = 4;
    [SerializeField] public Transform[] selectedWaypoints;
    GameObject[] allWaypoints;
    void Start()
    {
        allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        allWaypoints = allWaypoints.OrderBy(x => Random.Range(0, int.MaxValue)).ToArray();
        selectedWaypoints = new Transform[randomPointAmount];
        for (int i = 0; i < randomPointAmount; i++)
        {
            selectedWaypoints[i] = allWaypoints[i].transform;
        }
    }
    public int Length
    {
        get
        {
            return selectedWaypoints.Length;
        }
    }
    public Vector3 GetPoint(int index)
    {
        return selectedWaypoints[index].position;
    }
    void OnDrawGizmos()
    {
        for (int i = 1; i < selectedWaypoints.Length; i++)
        {
            Debug.DrawLine(selectedWaypoints[i - 1].position, selectedWaypoints[i].position, Color.red);
        }
        if (isLoop && selectedWaypoints.Length > 1)
        {
            // Ensure there are at least two waypoints before drawing a line
            Debug.DrawLine(selectedWaypoints[selectedWaypoints.Length - 1].position, selectedWaypoints[0].position, Color.red);
        }
    }
}