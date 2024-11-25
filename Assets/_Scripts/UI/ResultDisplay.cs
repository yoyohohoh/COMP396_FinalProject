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

        if (easyLevelRecord[0] == "Player1")
        {
            p1EasyTimer.text = easyLevelRecord[1];
            p2EasyTimer.text = easyLevelRecord[3];
        }
        else
        {
            p2EasyTimer.text = easyLevelRecord[1];
            p1EasyTimer.text = easyLevelRecord[3];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
