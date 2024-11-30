using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class State_BaseClass : MonoBehaviour
{
    private StateManager m_stateManager;

    public StateManager StateManager
    {
        get
        {
            Assert.IsNotNull(m_stateManager);
            return m_stateManager;
        }

        set => m_stateManager = value;
    }

    protected virtual void OnStateEnter() { }

    protected virtual void OnStateExit() { }

    protected void ChangeToState(Type theChangingState)
    {
        StateManager.SetNextState(theChangingState);
    }

    #region INTERNAL METHODS
    internal void OnAssigningToAStateManager(StateManager stateManager)
    {
        StateManager = stateManager;
    }

    internal virtual void OnStateEnter_Internal()
    {
        OnStateEnter();
    }

    internal virtual void OnStateExit_Internal()
    {
        OnStateExit();
    }
    #endregion
}
