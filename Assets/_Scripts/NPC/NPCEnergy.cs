
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
    [HideInInspector] public float idleStrength;
    [HideInInspector] public float patrolStrength;
    [HideInInspector] public float attackStrength;

    float lowThreshold = 0.3f;
    float highThreshold = 0.7f;
    NPCStateMachine fsm;
    // Start is called before the first frame update
    void Start()
    {
        fsm = this.gameObject.GetComponent<NPCStateMachine>();
        InitiateEnergyLevel();      
    }

    void EvaluateEnergy(float energyLevel)
    {
        if (energyLevel > 0.3f)
        {
            fsm.ChangeState("Patrol");
        }
        else if (energyLevel <= 0.3f)
        {
            fsm.ChangeState("Idle");
        }
    }

    void InitiateEnergyLevel()
    {
        RandomizeCurve(idleCurve);
        RandomizeCurve(patrolCurve);
        RandomizeCurve(attackCurve);
        patrolStrength = patrolCurve.Evaluate(1);
        attackStrength = attackCurve.Evaluate(1);
        idleStrength = idleCurve.Evaluate(1);
        energyLevel = FuzzyLogic(patrolStrength, attackStrength, idleStrength);
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
        if(fsm._currentState == null)
        {
            EvaluateEnergy(energyLevel);
        }        
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

}
