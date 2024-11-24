using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class NPCStateFactory
{
    public NPCStateBase CreateState(NPCStateMachine fsm, string stateType)
    {
        switch (stateType)
        {
            case "Idle":
                return new IdleState(fsm);
            case "Patrol":
                return new PatrolState(fsm);
            case "Attack":
                return new AttackState(fsm);
            default:
                throw new System.ArgumentException($"State '{stateType}' is not recognized.");
        }
    }
}
