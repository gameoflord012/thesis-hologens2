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

    protected T GetStateData<T>() where T : new()
    {
        if(StateManager.StateData == null)
        {
            StateManager.StateData = new T();
        }

        Assert.IsTrue(StateManager.StateData.GetType() == typeof(T), "inconsistent type between state data");
        return (T)StateManager.StateData;
    }

    protected virtual void OnStateEnter() { }

    protected virtual void OnStateExit() { }

    protected virtual void OnStateUpdate() { }

    protected void SetNextState(Type theChangingState)
    {
        StateManager.SetNextState(theChangingState);
    }

    #region INTERNAL METHODS
    internal void OnAssigningToAStateManager(StateManager stateManager)
    {
        StateManager = stateManager;
    }

    internal void OnStateEnter_Internal()
    {
        OnStateEnter();
    }

    internal void OnStateExit_Internal()
    {
        OnStateExit();
    }

    internal void OnStateUpdate_Internal()
    {
        OnStateUpdate();
    }
    #endregion
}
