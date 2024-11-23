using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] int totalNPC = 10;
    [SerializeField] GameObject NPCPrefab;
    [SerializeField] float startZPoint = 20.0f;
    [SerializeField] float endZPoint = 160.0f;
    float startXPoint = -5.0f;
    float endXPoint = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < totalNPC; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(startXPoint, endXPoint), 0f, Random.Range(startZPoint, endZPoint));
            Instantiate(NPCPrefab, randomPosition, Quaternion.identity);
        }
    }

}
