using MixedReality.Toolkit;
using MixedReality.Toolkit.Subsystems;
using MixedReality.Toolkit.UX;
using UnityEngine;
using UnityEngine.Assertions;

public class DefiningCylinderFinal_State : State_BaseClass
{
    [SerializeField] GameObject m_resetBtn_prefab;
    [SerializeField] GameObject m_confirmBtn_prefab;
    [SerializeField] GameObject m_cylinder_prefab;

    [SerializeField] TrackedHandJoint m_originJoint;
    [SerializeField] UnityEngine.XR.XRNode m_handedness;

    private const string BUTTON_GROUP_NAME = "DefiningCylinderFinal_State";

    private SceneInstances m_sceneInstances;
    private MenuController m_menu;

    private PressableButton m_confirmButton;

    private GameObject m_cylinder;

    private DefineCylinder_StateData m_stateData;

    private HandsAggregatorSubsystem m_handTracking;

    private bool m_stillDecidingSize;


    private void Awake()
    {
        m_sceneInstances = m_sceneInstances = GameObject.FindGameObjectWithTag("SceneInstances").GetComponent<SceneInstances>();
        m_menu = m_sceneInstances.Menu;
    }

    private void Start()
    {
        m_handTracking = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();

        m_menu.InitializeButton(m_resetBtn_prefab, BUTTON_GROUP_NAME).OnClicked.AddListener(() =>
        {
            SetNextState(typeof(BeginDefineCylinderSpace_State));
        });

        m_confirmButton = m_menu.InitializeButton(m_confirmBtn_prefab, BUTTON_GROUP_NAME);
        m_confirmButton.OnClicked.AddListener(() =>
        {
            SetNextState(typeof(FinishedDefineCylinder_State));
        });

        m_stateData = GetStateData<DefineCylinder_StateData>();

        m_stillDecidingSize = true; // place this after m_conform button initialized
    }

    protected override void OnStateEnter()
    {
        m_menu.SetHeaderText("Define Cylinder Final Step");
        m_menu.SetBodyText("Pinch one for selecting desire Cylinder size, pinch another one to modify current size. Press confirm for progressing next step!");
        m_menu.ToggleButtonGroup(BUTTON_GROUP_NAME, true);

        m_cylinder = Instantiate(m_cylinder_prefab, transform);
        m_cylinder.name = "CylinderModel";
    }

    protected override void OnStateUpdate()
    {
        m_cylinder.transform.position = m_stateData.CylinderOrigin;

        if(m_stillDecidingSize)
        {
            if (m_handTracking.TryGetJoint(m_originJoint, m_handedness, out HandJointPose joinPose))
            {
                m_cylinder.transform.localScale = joinPose.Position - m_stateData.CylinderOrigin;
            }
        }

        m_stillDecidingSize = false;

        if (m_handTracking.TryGetPinchProgress(m_handedness, out _, out bool isPinching, out _) && isPinching)
        {
            m_stillDecidingSize = true;
        }
    }

    protected override void OnStateExit()
    {
        Assert.IsFalse(m_stillDecidingSize, "Can not exit the state while size is still deciding");
        m_menu.ToggleButtonGroup(BUTTON_GROUP_NAME, false);

        m_stateData.CylinderSize = m_cylinder.transform.localScale;
        Destroy(m_cylinder); // remove cylinder model
    }
}
