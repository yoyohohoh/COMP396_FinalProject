using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatus : MonoBehaviour
{
    [SerializeField] GameObject npc;
    [SerializeField] NPCEnergy nPCEnergy;
    [SerializeField] NPCController nPCController;
    [SerializeField] NPCStateMachine npcStateMachine;
    [SerializeField] string currentState;
    string behaviour;

    // Start is called before the first frame update
    void Start()
    {
        npc = GameObject.Find("NPC");
        nPCEnergy = npc.GetComponent<NPCEnergy>();
        nPCController = npc.GetComponent<NPCController>();
        npcStateMachine = npc.GetComponent<NPCStateMachine>();


    }

    // Update is called once per frame
    void Update()
    {
        currentState = npcStateMachine._currentState.ToString();
        if (currentState == "IdleState")
        {
            if (nPCController.isAlert)
            {
                behaviour = "Idle Alert";
            }
            else
            {
                behaviour = "Idle Dead";
            }
        }

        else if (currentState == "PatrolState")
        {
            if (nPCController.isPatrol)
            {
                behaviour = "Patrol Moving";
            }
            else
            {
                behaviour = "Patrol Dead";
            }
        }

        else if (currentState == "AttackState")
        {
            if (nPCController.isAttack)
            {
                behaviour = "Attack Chase";
            }
            else
            {
                behaviour = "Attack Error";
            }
        }

        else
        {
            behaviour = "Error";
        }

        Debug.Log($"NPC Status: {behaviour}");
    }
}
