using UnityEngine;

public class StateManager : MonoBehaviour
{
    private State_BaseClass[] m_states;

    private void Awake()
    {
        m_states = GetComponentsInChildren<State_BaseClass>();
        Debug.Log(m_states.Length);
    }
}
