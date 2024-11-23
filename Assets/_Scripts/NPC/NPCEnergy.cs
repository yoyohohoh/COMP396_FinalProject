using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnergy : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] AnimationCurve idleCurve;
    [SerializeField] AnimationCurve patrolCurve;
    [SerializeField] AnimationCurve attackCurve;
    [SerializeField] public float energyLevel;

    float patrolStrength;
    float attackStrength;
    float idleStrength;

    float lowThreshold = 0.3f;
    float highThreshold = 0.7f;

    [Header("Mode")]
    List<NPCStatus> NPCStatusList = new List<NPCStatus>();
    [SerializeField] string NPCMode;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeCurve(idleCurve);
        RandomizeCurve(patrolCurve);
        RandomizeCurve(attackCurve);

        NPCStatusList = new List<NPCStatus>();

        NPCStatusList.Add(new NPCStatus("Low", false, false, true));
        NPCStatusList.Add(new NPCStatus("Medium", false, true, false));
        NPCStatusList.Add(new NPCStatus("High", true, true, false));
    }

    void RandomizeCurve(AnimationCurve curve)
    {
        curve.keys = new Keyframe[0];

        int numKeyframes = Random.Range(2, 5);

        for (int i = 0; i < numKeyframes; i++)
        {
            float time = Random.Range(0f, 1f);
            float value = Random.Range(0f, 1f);

            curve.AddKey(time, value);
        }

        curve.SmoothTangents(0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        // Energy Level
        patrolStrength = patrolCurve.Evaluate(1);
        attackStrength = attackCurve.Evaluate(1);
        idleStrength = idleCurve.Evaluate(1);
        energyLevel = FuzzyLogic(patrolStrength, attackStrength, idleStrength);

        //NPC Mode Decision
        NPCMode = NPCModeDecision(energyLevel).mode;

    }
    float FuzzyLogic(float patrolStrength, float attackStrength, float idleStrength)
    {
        float patrolLow = Mathf.Clamp01(1 - patrolStrength);
        float attackLow = Mathf.Clamp01(1 - attackStrength);
        float idleLow = Mathf.Clamp01(1 - idleStrength);


        float ruleHigh = Mathf.Min(patrolStrength, attackStrength) * highThreshold;
        float ruleLow = Mathf.Min(idleStrength, Mathf.Min(patrolLow, attackLow)) * lowThreshold;
        float ruleMedium = Mathf.Min(attackStrength, idleLow) * lowThreshold;

        float finalEnergy = ruleHigh + ruleMedium + ruleLow;

        return Mathf.Clamp(finalEnergy, 0f, 1f);
    }

    public NPCStatus NPCModeDecision(float energyLevel)
    {
        NPCStatus decision = null;
        if (energyLevel >= 0.70f)
        {
            decision = NPCStatusList[2];
        }
        else if (energyLevel < 0.70f && energyLevel > 0.30f)
        {
            decision = NPCStatusList[1];
        }
        else
        if (energyLevel <= 0.30f)
        {
            decision = NPCStatusList[0];
        }

        return decision;
    }

}
