using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : NPCStateBase
{
    public IdleState(NPCStateMachine fsm) : base(fsm) { }
    public override void Enter() => Debug.Log("Entering Idle State");
    public override void Execute() => Debug.Log("Executing Idle State");
    public override void Exit() => Debug.Log("Exiting Idle State");
}
public class PatrolState : NPCStateBase
{
    public PatrolState(NPCStateMachine fsm) : base(fsm) { }

    public override void Enter() => Debug.Log("Entering Patrol State");
    public override void Execute() => Debug.Log("Updating Patrol State");
    public override void Exit() => Debug.Log("Exiting Patrol State");
}

public class AttackState : NPCStateBase
{
    public AttackState(NPCStateMachine fsm) : base(fsm) { }

    public override void Enter() => Debug.Log("Entering Attack State");
    public override void Execute() => Debug.Log("Updating Attack State");
    public override void Exit() => Debug.Log("Exiting Attack State");
}