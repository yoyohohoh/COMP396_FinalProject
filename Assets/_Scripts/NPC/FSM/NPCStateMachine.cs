using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class NPCStateMachine : MonoBehaviour
{
    NPCStateBase _currentState;
    NPCStateFactory _stateFactory;

    [SerializeField] string currentState;
    void Start()
    {
        _stateFactory = new NPCStateFactory();
        ChangeState("Idle");
    }
    public NPCStateMachine(NPCStateFactory factory)
    {
        _stateFactory = factory;
    }

    public void ChangeState(string newState)
    {
        _currentState?.Exit();
        _currentState = _stateFactory.CreateState(this, newState);
        _currentState.Enter();
    }

    void Update()
    {
        _currentState?.Execute();
        currentState = _currentState.ToString();
    }
}
