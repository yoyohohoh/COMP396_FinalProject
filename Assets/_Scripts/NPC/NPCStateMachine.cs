using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : MonoBehaviour
{
    private NPCStateBase _currentState;
    private NPCStateFactory _stateFactory;

    public NPCStateMachine(NPCStateFactory factory)
    {
        _stateFactory = factory;
    }

    public void ChangeState(NPCStateMachine fsm,string newState)
    {
        _currentState?.Exit();
        _currentState = _stateFactory.CreateState(fsm, newState);
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Execute();
    }
}
