using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    public Text countdownText;
    string level;
    void Start()
    {
        countdownText = this.GetComponent<Text>();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (currentSceneIndex)
        {
            case 3:
                level = "Easy";
                break;
            case 4:
                level = "Medium";
                break;
            case 5:
                level = "Hard";
                break;
        }

        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        countdownText.text = level;
        yield return new WaitForSeconds(1.0f);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        StartCounting();
        countdownText.text = "START";
        yield return new WaitForSeconds(0.5f);
        countdownText.enabled = false;
    }

    private void StartCounting()
    {
        GameObject.Find("Timer1").GetComponent<TimeDisplay>().isTime = true;
        GameObject.Find("Timer2").GetComponent<TimeDisplay>().isTime = true;
    }
}
