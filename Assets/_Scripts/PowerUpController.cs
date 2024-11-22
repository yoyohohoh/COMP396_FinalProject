using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] int totalPowerUp = 10;
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] float startZPoint = 20.0f;
    [SerializeField] float endZPoint = 160.0f;
    float startXPoint = -5.0f;
    float endXPoint = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
       for(int i = 0; i < totalPowerUp; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(startXPoint, endXPoint), 0f, Random.Range(startZPoint, endZPoint));
            Instantiate(powerUpPrefab, randomPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
