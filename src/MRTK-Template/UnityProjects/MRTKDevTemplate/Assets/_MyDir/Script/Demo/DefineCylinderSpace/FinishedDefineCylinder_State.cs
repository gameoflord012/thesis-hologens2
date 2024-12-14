using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinishedDefineCylinder_State : State_BaseClass
{
    const string BUTTON_GROUP_NAME = "FinishedDefineCylinder_State";

    MenuController m_menu;

    DefineCylinder_StateData m_stateData;

    private void Awake()
    {
        var sceneInstances = GameObject.FindGameObjectWithTag("SceneInstances").GetComponent<SceneInstances>();
        m_menu = sceneInstances.Menu;
    }

    private void Start()
    {
        m_stateData = GetStateData<DefineCylinder_StateData>();
    }

    protected override void OnStateEnter()
    {
        m_menu.SetHeaderText("Defining Cylinder Completed");

        m_menu.SetBodyText($"Here is your cylinder information:\n cylinder position: {m_stateData.CylinderOrigin}\n cylinder size {m_stateData.CylinderSize}");
        m_menu.ToggleButtonGroup(BUTTON_GROUP_NAME, true);
    }

    protected override void OnStateExit()
    {
        m_menu.ToggleButtonGroup(BUTTON_GROUP_NAME, false);
    }
}
