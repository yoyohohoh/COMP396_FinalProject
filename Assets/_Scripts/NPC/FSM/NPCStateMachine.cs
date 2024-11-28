using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.XR.Haptics;

public class NPCStateMachine : MonoBehaviour
{
    public NPCController _controller;
    public NPCStateBase _currentState;
    NPCStateFactory _stateFactory;

    [SerializeField] string currentState;
    void Start()
    {
        _controller = this.gameObject.GetComponent<NPCController>();    
        _stateFactory = new NPCStateFactory();
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
        if(_currentState != null)
        { 
            currentState = _currentState.ToString();
            Debug.Log($"NPC: {_currentState.ToString()}");
        }
    }
}
