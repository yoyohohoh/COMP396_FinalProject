using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] float energyLevel;
    NPCStateMachine fsm;
    void Start()
    {
        //energyLevel = this.gameObject.GetComponent<NPCEnergy>().energyLevel;
        
    }

    void Update()
    {
        energyLevel = this.gameObject.GetComponent<NPCEnergy>().energyLevel;
        fsm = this.gameObject.GetComponent<NPCStateMachine>();
        if (energyLevel > 0.3f)
        {
            fsm.ChangeState("Patrol");
        }       
    }
}
