using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] Text p1Name;
    [SerializeField] Text p2Name;
    [SerializeField] Text p1EasyTimer;
    [SerializeField] Text p2EasyTimer;
    [SerializeField] Text p1MediumTimer;
    [SerializeField] Text p2MediumTimer;
    [SerializeField] Text p1HardTimer;
    [SerializeField] Text p2HardTimer;
    List<string> easyLevelRecord;
    List<string> mediumLevelRecord;
    List<string> hardLevelRecord;

    [SerializeField] Image P1Badge;
    [SerializeField] Image P2Badge;
    [SerializeField] Image P1EasyBadge;
    [SerializeField] Image P2EasyBadge;
    [SerializeField] Image P1MediumBadge;
    [SerializeField] Image P2MediumBadge;
    [SerializeField] Image P1HardBadge;
    [SerializeField] Image P2HardBadge;

    int p1CountWin = 0;
    int p2CountWin = 0;
    DataKeeper dataKeeper;
    // Start is called before the first frame update
    void Start()
    {
        dataKeeper = GameObject.Find("DataKeeper").GetComponent<DataKeeper>();
        p1Name.text = dataKeeper.player1Name;
        p2Name.text = dataKeeper.player2Name;
        easyLevelRecord = dataKeeper.easyLevelRecord;
        mediumLevelRecord = dataKeeper.mediumLevelRecord;
        hardLevelRecord = dataKeeper.hardLevelRecord;
        UpdateTimerTexts(dataKeeper.easyLevelRecord, p1EasyTimer, p2EasyTimer);
        UpdateTimerTexts(dataKeeper.mediumLevelRecord, p1MediumTimer, p2MediumTimer);
        UpdateTimerTexts(dataKeeper.hardLevelRecord, p1HardTimer, p2HardTimer);

        UpdateBadges(p1EasyTimer, p2EasyTimer, P1EasyBadge, P2EasyBadge);
        UpdateBadges(p1MediumTimer, p2MediumTimer, P1MediumBadge, P2MediumBadge);
        UpdateBadges(p1HardTimer, p2HardTimer, P1HardBadge, P2HardBadge);

        if (p1CountWin > p2CountWin)
        {
            P1Badge.enabled = true;
            P2Badge.enabled = false;
        }
        if (p1CountWin < p2CountWin)
        {
            P1Badge.enabled = false;
            P2Badge.enabled = true;
        }
        if (p1CountWin == p2CountWin)
        {
            P1Badge.enabled = true;
            P2Badge.enabled = true;
        }
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

    void UpdateBadges(Text p1Timer, Text p2Timer, Image p1Badge, Image p2Badge)
    {


        if (p1Timer.text == "Dead" && p2Timer.text == "Dead")
        {
            p1Badge.enabled = false;
            p2Badge.enabled = false;
        }
        else if (p1Timer.text != "Dead" && p2Timer.text != "Dead")
        {
            TimeSpan p1Time = TimeSpan.Parse(p1Timer.text);
            TimeSpan p2Time = TimeSpan.Parse(p2Timer.text);

            if (p1Time < p2Time)
            {
                p1Badge.enabled = true;
                p2Badge.enabled = false;
                p1CountWin++;
            }

            if (p1Time > p2Time)
            {
                p2Badge.enabled = true;
                p1Badge.enabled = false;
                p2CountWin++;
            }

            if (p1Time == p2Time)
            {
                p1Badge.enabled = true;
                p2Badge.enabled = true;
                p1CountWin++;
                p2CountWin++;
            }
        }
        else
        {
            if (p1Timer.text == "Dead" && p2Timer.text != "Dead")
            {
                p2Badge.enabled = true;
                p1Badge.enabled = false;
                p2CountWin++;
            }

            if (p1Timer.text != "Dead" && p2Timer.text == "Dead")
            {
                p1Badge.enabled = true;
                p2Badge.enabled = false;
                p1CountWin++;
            }
        }

    }
}
