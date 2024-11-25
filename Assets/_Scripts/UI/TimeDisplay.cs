using UnityEngine;
using UnityEngine.UI;


public class TimeDisplay : MonoBehaviour
{
    //public Text timeText;  // Reference to the TextMeshPro component
    private float elapsedTime = 0f;

    void Update()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Convert elapsed time to minutes and seconds format (mm:ss)
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Display the time in the format mm:ss
        this.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
