using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class IdleState : NPCStateBase
{
    public IdleState(NPCStateMachine fsm) : base(fsm) { }
    BTNode _behaviorTree;
    public override void Enter()
    {
        BTNode idleSelector = new Selector(
        new List<BTNode>
        {
            new Sequence(new List<BTNode>
            {
                new CheckStrengthNode("Idle", -fsm._controller.idleStrength, -0.5f),
                new StayAlertNode(fsm._controller)
            }),
        }
        );

        _behaviorTree = idleSelector;
    }
    public override void Execute()
    {
        _behaviorTree.Execute();
    }
    public override void Exit()
    {
    }
}
public class PatrolState : NPCStateBase
{
    public PatrolState(NPCStateMachine fsm) : base(fsm) { }
    BTNode _behaviorTree;
    public override void Enter()
    {
        BTNode patrolSelector = new Selector(
        new List<BTNode>
        {
            new Sequence(new List<BTNode>
            {
                new StayAlertNode(fsm._controller),
                new CheckStrengthNode("Patrol", fsm._controller.patrolStrength, 0.5f),
                new FollowWaypointsNode(fsm._controller)
            }),

        }
        );

        _behaviorTree = patrolSelector;
    }
    public override void Execute()
    {
        _behaviorTree.Execute();
    }
    public override void Exit()
    {
    }
}

public class AttackState : NPCStateBase
{
    public AttackState(NPCStateMachine fsm) : base(fsm) { }
    BTNode _behaviorTree;
    public override void Enter()
    {
        BTNode attackSelector = new Selector(
        new List<BTNode>
        {
            new Sequence(new List<BTNode>
            {
                new CheckStrengthNode("Attack", fsm._controller.attackStrength, 0.5f),
                new GoPlayerNode(fsm._controller)
            }),
        }
        );

        _behaviorTree = attackSelector;
    }
    public override void Execute()
    {
        _behaviorTree.Execute();
    }
    public override void Exit()
    { 
       
    }
}