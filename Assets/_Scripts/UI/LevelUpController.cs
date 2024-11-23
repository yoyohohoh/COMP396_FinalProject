using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    int countPlayer;


    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        countPlayer = players.Length;
    }

    void FixedUpdate()
    {
        if(countPlayer <= 0)
        {
            Debug.Log("All player pass");
            
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerController>().enabled = false;
            }
            // add player winning animation
            Invoke("NextLevel", 2f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            countPlayer--;
        }
    }

    private void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
