using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCStateBase : MonoBehaviour
{
    protected NPCStateMachine fsm;

    public NPCStateBase(NPCStateMachine fsm)
    {
        this.fsm = fsm;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
