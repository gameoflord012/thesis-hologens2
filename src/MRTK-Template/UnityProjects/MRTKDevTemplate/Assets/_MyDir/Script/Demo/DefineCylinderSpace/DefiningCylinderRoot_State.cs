using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DefiningCylinderRoot_State : State_BaseClass
{
    [SerializeField] GameObject m_returnButton_prefab;

    private const string BUTTON_GROUP_NAME = "DefiningCylinderRoot_State";

    private SceneInstances m_sceneInstances;
    private MenuController m_menu;

    private void Awake()
    {
        // validate serialize 
        Assert.IsNotNull(m_returnButton_prefab.GetComponent<PressableButton>());

        m_sceneInstances = GameObject.FindGameObjectWithTag("SceneInstances").GetComponent<SceneInstances>();
        m_menu = m_sceneInstances.Menu;
    }
    private void Start()
    {
        // initialize button group
        m_menu.InitializeButton(m_returnButton_prefab, BUTTON_GROUP_NAME);
    }

    protected override void OnStateEnter()
    {
        m_menu.SetHeaderText("This is DefiningCylinderRoot_State");
        m_menu.SetBodyText(this.GetType().ToString());
    }
}
