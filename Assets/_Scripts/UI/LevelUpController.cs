using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    int countPlayer;
    DataKeeper dataKeeper;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        countPlayer = players.Length;
        dataKeeper = GameObject.Find("DataKeeper").GetComponent<DataKeeper>();
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
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            dataKeeper.easyLevelRecord.Add(other.gameObject.name);
            dataKeeper.easyLevelRecord.Add(playerController.timerTxt.text);
            playerController.timerTxt.GetComponent<TimeDisplay>().enabled = false;
        }
    }

    private void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
