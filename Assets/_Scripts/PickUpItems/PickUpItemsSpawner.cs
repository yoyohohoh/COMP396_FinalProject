using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PickUpItemsSpawner : MonoBehaviour
{
    [SerializeField] int totalPowerUp = 5;
    [SerializeField] int totalReward = 5;
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] GameObject rewardPrefab;
    [SerializeField] float spawnYPoint = 1.0f;

    [SerializeField] Vector3 centerPoint;

    [SerializeField] float innerRadius;
    [SerializeField] float outerRadius;


    // Start is called before the first frame update
    void Start()
    {
       for(int i = 0; i < totalPowerUp; i++)
        {
            SpawnPowerUp(powerUpPrefab);

        }
        for (int i = 0; i < totalReward; i++)
        {
            SpawnPowerUp(rewardPrefab);
        }
    }

    void SpawnPowerUp(GameObject prefab)
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
