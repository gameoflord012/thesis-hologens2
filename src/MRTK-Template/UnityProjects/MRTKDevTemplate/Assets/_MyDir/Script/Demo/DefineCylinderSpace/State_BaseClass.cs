using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_BaseClass : MonoBehaviour
{
    protected virtual void OnStateEnter() { }

    protected virtual void OnStateExit() { }

    protected void ChangeToState()
    {

    }
}
