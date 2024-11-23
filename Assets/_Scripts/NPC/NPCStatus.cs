using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class NPCStatus
{
    public string mode;
    public bool isAggressive;
    public bool isAlert;
    public bool isResting;

    public NPCStatus(string mode, bool isAggressive, bool isAlert, bool isResting)
    {
        this.mode = mode;
        this.isAggressive = isAggressive;
        this.isAlert = isAlert;
        this.isResting = isResting;
    }
}

