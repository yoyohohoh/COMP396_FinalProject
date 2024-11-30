using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class StartTimer : MonoBehaviour
{
    [SerializeField] GameObject[] players;
    public Text countdownText;

    void Start()
    {
        countdownText = this.GetComponent<Text>();
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
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
