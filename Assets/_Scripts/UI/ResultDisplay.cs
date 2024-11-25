using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] Text p1EasyTimer;
    [SerializeField] Text p2EasyTimer;
    [SerializeField] Text p1MediumTimer;
    [SerializeField] Text p2MediumTimer;
    [SerializeField] Text p1HardTimer;
    [SerializeField] Text p2HardTimer;
    List<string> easyLevelRecord;
    List<string> mediumLevelRecord;
    List<string> hardLevelRecord;

    DataKeeper dataKeeper;
    // Start is called before the first frame update
    void Start()
    {
        dataKeeper = GameObject.Find("DataKeeper").GetComponent<DataKeeper>();

        easyLevelRecord = dataKeeper.easyLevelRecord;
        mediumLevelRecord = dataKeeper.mediumLevelRecord;
        hardLevelRecord = dataKeeper.hardLevelRecord;
        UpdateTimerTexts(dataKeeper.easyLevelRecord, p1EasyTimer, p2EasyTimer);
        UpdateTimerTexts(dataKeeper.mediumLevelRecord, p1MediumTimer, p2MediumTimer);
        UpdateTimerTexts(dataKeeper.hardLevelRecord, p1HardTimer, p2HardTimer);
    }

    void UpdateTimerTexts(List<string> levelRecord, Text p1Timer, Text p2Timer)
    {
        if (levelRecord[0] == "Player1")
        {
            p1Timer.text = levelRecord[1];
            p2Timer.text = levelRecord[3];
        }
        else
        {
            p2Timer.text = levelRecord[1];
            p1Timer.text = levelRecord[3];
        }
    }
}
