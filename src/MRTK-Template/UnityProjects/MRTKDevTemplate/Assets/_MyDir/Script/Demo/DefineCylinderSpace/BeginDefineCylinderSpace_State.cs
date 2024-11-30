using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BeginDefineCylinderSpace_State : State_BaseClass
{
    [SerializeField] GameObject m_playButtonPrefab;

    private const string BUTTON_GROUP_NAME = "BeginDefineCylinderSpace_State";

    private SceneInstances m_sceneInstances;
    private MenuController m_menu;

    private void Awake()
    {
        // validate serialize 
        Assert.IsNotNull(m_playButtonPrefab.GetComponent<PressableButton>());

        m_sceneInstances = GameObject.FindGameObjectWithTag("SceneInstances").GetComponent<SceneInstances>();
        m_menu = m_sceneInstances.Menu;
    }

    private void Start()
    {
        // initialize button group
        m_menu.AddButton(m_playButtonPrefab, BUTTON_GROUP_NAME);
    }

    protected override void OnStateEnter()
    {
        m_menu.SetHeaderText("Define Cylinder Space");
        m_menu.SetBodyText("Press play button to define a cylinder space for capturing your scanning object. The space should contain the scanning object completely.");
        m_menu.ToggleButtonGroup(BUTTON_GROUP_NAME, true);
    }

    protected override void OnStateExit()
    {
        m_menu.ToggleButtonGroup(BUTTON_GROUP_NAME, false);
    }
}
