using System;
using UnityEngine;
using UnityEngine.Assertions;

public class StateManager : MonoBehaviour
{
    private State_BaseClass[] m_states;

    private State_BaseClass m_currentState;

    private void Awake()
    {
        m_states = GetComponentsInChildren<State_BaseClass>();

        Assert.IsTrue(m_states.Length > 0);
        m_currentState = m_states[0];
    }

    private void Start()
    {
        foreach (var state in m_states)
        {
            state.OnAssigningToAStateManager(this);
        }

        m_currentState.OnStateEnter_Internal();
    }

    internal void ChangeToState(Type typeOfTheChangingState)
    {
        Assert.IsTrue(m_currentState.GetType() != typeOfTheChangingState, "Can't transition to the same state");

        State_BaseClass nextState = null;
        foreach (var state_i in m_states)
        {
            if(nextState.GetType() == typeOfTheChangingState)
            {
                nextState = state_i;
            }
        }

        Assert.IsNotNull(nextState, $"Can't find the next state having '{typeOfTheChangingState}' type");

        //
        // Begin State Transition
        //

        m_currentState.OnStateExit_Internal();
        nextState.OnStateEnter_Internal();

        m_currentState = nextState;
        nextState = null;
    }
}
