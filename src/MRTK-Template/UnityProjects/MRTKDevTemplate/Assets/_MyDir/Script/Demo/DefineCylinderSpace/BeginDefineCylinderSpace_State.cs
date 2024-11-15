using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginDefineCylinderSpace_State : State_BaseClass
{
    private SceneInstances m_sceneInstances;
    private MenuController m_menu;

    private void Awake()
    {
        m_sceneInstances = GameObject.FindGameObjectWithTag("SceneInstances").GetComponent<SceneInstances>();
        m_menu = m_sceneInstances.Menu;
    }

    protected override void OnStateEnter()
    {
        m_menu.SetHeaderText("This is iewaghohgwapghewo");
        m_menu.SetBodyText(this.GetType().ToString());
    }
}
