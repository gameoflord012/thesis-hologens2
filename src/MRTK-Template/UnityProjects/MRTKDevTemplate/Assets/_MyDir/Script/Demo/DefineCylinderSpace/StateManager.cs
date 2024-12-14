using System;
using UnityEngine;
using UnityEngine.Assertions;

public class StateManager : MonoBehaviour
{
    private State_BaseClass[] m_states;

    private State_BaseClass m_currentState;
    private State_BaseClass m_nextState;

    private object m_stateData;

    public object StateData
    {
        get
        {
            return m_stateData;
        }
        set
        {
            Assert.IsNotNull(value, "Can't null");
            Assert.IsNull(m_stateData, "State data is initialized");

            m_stateData = value;
        }
    }

    private void Awake()
    {
        m_states = GetComponentsInChildren<State_BaseClass>();

        foreach (var state in m_states)
        {
            state.OnAssigningToAStateManager(this);
        }

        Assert.IsTrue(m_states.Length > 0);
        m_nextState = m_states[0];
    }

    private void Update()
    {
        if(m_nextState != null)
        {
            MoveToNextStateImmediately();
        }

        m_currentState?.OnStateUpdate_Internal();
    }

    public void SetNextState(Type typeOfTheChangingState)
    {
        Assert.IsTrue(m_currentState.GetType() != typeOfTheChangingState, "Can't transition to the same state");
        Assert.IsNull(m_nextState, "Warning! Next state is set twice, before current state is updating to next state");

        foreach (var state_i in m_states)
        {
            if (state_i.GetType() == typeOfTheChangingState)
            {
                m_nextState = state_i;
                break;
            }
        }

        Assert.IsNotNull(m_nextState, $"Can't find the next state having '{typeOfTheChangingState}' type");
    }

    private void MoveToNextStateImmediately()
    {
        if (m_nextState == null) return;
        
        m_currentState?.OnStateExit_Internal();
        m_nextState.OnStateEnter_Internal();

        m_currentState = m_nextState;
        m_nextState = null;
    }
}
