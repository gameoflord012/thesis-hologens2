using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.Subsystems;
using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DefiningCylinderRoot_State : State_BaseClass
{
    [SerializeField] GameObject m_returnButton_prefab;
    [SerializeField] TrackedHandJoint m_originJoint;
    [SerializeField] UnityEngine.XR.XRNode m_handedness;

    private const string BUTTON_GROUP_NAME = "DefiningCylinderRoot_State";

    private SceneInstances m_sceneInstances;
    private MenuController m_menu;

    private LineRenderer m_lineRenderer;
    private HandsAggregatorSubsystem m_handTracking;

    private void Awake()
    {
        // validate serialize 
        Assert.IsNotNull(m_returnButton_prefab.GetComponent<PressableButton>());

        m_lineRenderer = GetComponent<LineRenderer>();

        m_sceneInstances = GameObject.FindGameObjectWithTag("SceneInstances").GetComponent<SceneInstances>();
        m_menu = m_sceneInstances.Menu;
    }
    private void Start()
    {
        m_handTracking = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();

        // initialize button group
        m_menu.InitializeButton(m_returnButton_prefab, BUTTON_GROUP_NAME).OnClicked.AddListener(() =>
        {
            SetNextState(typeof(BeginDefineCylinderSpace_State));
        });

        // initialize scene instances variable
        var sceneInstances = FindObjectOfType<SceneInstances>();
        Assert.IsNotNull(sceneInstances);
    }

    protected override void OnStateEnter()
    {
        m_menu.SetHeaderText("Defining Cylinder Position");
        m_menu.SetBodyText("Select a point on the virtual surface by using grabbing interaction: hover your hand to a near surface and start grabbing motion");
        m_menu.ToggleButtonGroup(BUTTON_GROUP_NAME, true);
    }

    protected override void OnStateExit()
    {
        m_menu.ToggleButtonGroup(BUTTON_GROUP_NAME, false);

        // remove visual position line
        m_lineRenderer.positionCount = 0;
    }

    protected override void OnStateUpdate()
    {
        m_lineRenderer.positionCount = 0;

        if (m_handTracking.TryGetJoint(m_originJoint, m_handedness, out HandJointPose joinPose))
        {
            var rayOrigin = joinPose.Position;

            if (Physics.Raycast(new Ray(rayOrigin, Vector3.down), out RaycastHit hitInfo, 2))
            {
                // Draw a visual ray from hand to Cylinder root
                m_lineRenderer.positionCount = 2;
                m_lineRenderer.SetPosition(0, rayOrigin);
                m_lineRenderer.SetPosition(1, hitInfo.point);

                // If the hand is Pinching, select the that root
                if(m_handTracking.TryGetPinchProgress(m_handedness, out _, out bool isPinching, out _) && isPinching)
                {
                    GetStateData<DefineCylinder_StateData>().CylinderOrigin = hitInfo.point;
                    SetNextState(typeof(DefiningCylinderFinal_State));
                }
            }
        }
    }
}
