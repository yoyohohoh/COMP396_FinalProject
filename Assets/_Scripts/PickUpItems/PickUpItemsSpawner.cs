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
    [SerializeField] float startXPoint = -5.0f;
    [SerializeField] float endXPoint = 5.0f;
    [SerializeField] float startZPoint = 20.0f;
    [SerializeField] float endZPoint = 160.0f;
    [SerializeField] float spawnYPoint = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
       for(int i = 0; i < totalPowerUp; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(startXPoint, endXPoint), spawnYPoint, Random.Range(startZPoint, endZPoint));
            Instantiate(powerUpPrefab, randomPosition, Quaternion.identity);
        }
        for (int i = 0; i < totalReward; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(startXPoint, endXPoint), 0f, Random.Range(startZPoint, endZPoint));
            Instantiate(rewardPrefab, randomPosition, Quaternion.identity);
        }
    }

}
