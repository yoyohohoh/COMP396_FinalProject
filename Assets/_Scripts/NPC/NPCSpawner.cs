using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] int totalNPC = 10;
    [SerializeField] GameObject NPCPrefab;

    [SerializeField] float spawnYPoint = 2.0f;

    [SerializeField] Vector3 centerPoint;

    [SerializeField] float innerRadius;
    [SerializeField] float outerRadius;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateSpawner", 3.0f);
    }

    void ActivateSpawner()
    {
        for (int i = 0; i < totalNPC; i++)
        {
            SpawnNPC(NPCPrefab);
        }
    }

    void SpawnNPC(GameObject prefab)
    {
        float angle = Random.Range(0f, 360f);
        float radians = angle * Mathf.Deg2Rad;

        float radius = Random.Range(innerRadius, outerRadius);

        float x = centerPoint.x + radius * Mathf.Cos(radians);
        float z = centerPoint.z + radius * Mathf.Sin(radians);

        float y = spawnYPoint;

        Vector3 randomPosition = new Vector3(x, y, z);

        Instantiate(prefab, randomPosition, Quaternion.identity);
    }

}
