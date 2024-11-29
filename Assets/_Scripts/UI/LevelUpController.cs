using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    [SerializeField] int countPlayer;
    [SerializeField] DataKeeper dataKeeper;
    [SerializeField] bool hasCollided = false;

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
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerController>().health <= 0)
            {
                player.GetComponent<PlayerController>().lifeTxt.text = "Dead";
                player.GetComponent<PlayerController>().ResetPlayerPosition();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCollided) return;
        if (other.CompareTag("Player"))
        {
            hasCollided = true;
            countPlayer--;
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ResetPlayerPosition();
            playerController.moveSpeed = 0f;
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "EasyLevel")
            {
                dataKeeper.easyLevelRecord.Add(other.gameObject.name);
                if(playerController.health > 0)
                {
                    dataKeeper.easyLevelRecord.Add(playerController.timerTxt.text);
                }
                else
                {
                    dataKeeper.easyLevelRecord.Add("Dead");
                }

            }
            if (currentScene.name == "MediumLevel")
            {
                dataKeeper.mediumLevelRecord.Add(other.gameObject.name);
                dataKeeper.mediumLevelRecord.Add(playerController.timerTxt.text);
            }
            if (currentScene.name == "HardLevel")
            {
                dataKeeper.hardLevelRecord.Add(other.gameObject.name);
                dataKeeper.hardLevelRecord.Add(playerController.timerTxt.text);
            }
            playerController.timerTxt.GetComponent<TimeDisplay>().enabled = false;
            playerController.ResetPlayerPosition();
            StartCoroutine(ResetCollisionFlag());
        }
    }
    IEnumerator ResetCollisionFlag()
    {
        yield return new WaitForSeconds(1.0f); // Adjust the delay as needed
        hasCollided = false;
    }
    private void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
