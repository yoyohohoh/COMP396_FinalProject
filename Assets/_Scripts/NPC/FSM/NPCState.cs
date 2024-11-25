using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class IdleState : NPCStateBase
{
    public IdleState(NPCStateMachine fsm) : base(fsm) { }
    Node _behaviorTree;
    public override void Enter()
    {
        Debug.Log("Entering Idle State");
        Node idleSelector = new Selector(
        new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckStrengthNode("Idle", -fsm._controller.idleStrength, -0.5f),
                new stayAlertNode(fsm._controller)
            }),
        }
        );

        _behaviorTree = idleSelector;
    }
    public override void Execute()
    {
        Debug.Log("Updating Idle State");
        _behaviorTree.Execute();
    }
    public override void Exit()
    {
        Debug.Log("Exiting Idle State");
        //fsm._controller.isAlert = false;
    }
}
public class PatrolState : NPCStateBase
{
    public PatrolState(NPCStateMachine fsm) : base(fsm) { }
    Node _behaviorTree;
    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
        Node patrolSelector = new Selector(
        new List<Node>
        {
            new Sequence(new List<Node>
            {
                new stayAlertNode(fsm._controller),
                new CheckStrengthNode("Patrol", fsm._controller.patrolStrength, 0.5f),
                new FollowWaypointsNode(fsm._controller)
            }),

        }
        );

        _behaviorTree = patrolSelector;
    }
    public override void Execute()
    {
        Debug.Log("Updating Patrol State");
        _behaviorTree.Execute();
    }
    public override void Exit()
    {
        Debug.Log("Exiting Patrol State");
        //fsm._controller.isPatrol = false;
    }
}

public class AttackState : NPCStateBase
{
    public AttackState(NPCStateMachine fsm) : base(fsm) { }
    Node _behaviorTree;
    public override void Enter()
    {
        Node attackSelector = new Selector(
        new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckStrengthNode("Attack", fsm._controller.attackStrength, 0.5f),
            }),
        }
        );

        _behaviorTree = attackSelector;
    }
    public override void Execute()
    {
        Debug.Log("Updating Attack State");
        _behaviorTree.Execute();
    }
    public override void Exit()
    { 
        Debug.Log("Exiting Attack State");
       
    }
}