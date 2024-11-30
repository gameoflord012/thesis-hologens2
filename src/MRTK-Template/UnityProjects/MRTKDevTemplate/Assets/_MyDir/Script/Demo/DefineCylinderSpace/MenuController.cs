using MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_headerTMP;
    [SerializeField] private TextMeshProUGUI m_bodyTMP;
    [SerializeField] private Transform m_buttonRoot;

    public void SetHeaderText(string theText)
    {
        m_headerTMP.text = theText;    
    }

    public void SetBodyText(string theText)
    {
        m_bodyTMP.text = theText;
    }

    public PressableButton InitializeButton(GameObject prefab_AddingButton, string buttonGroup)
    {
        Assert.IsNotNull(prefab_AddingButton.GetComponent<PressableButton>());

        var parent = GetButtonGroup(buttonGroup);
        parent.SetActive(false);

        var initializedBtn = Instantiate(prefab_AddingButton, parent.transform);

        return initializedBtn.GetComponent<PressableButton>();
    }

    public void ToggleButtonGroup(string buttonGroup, bool shouldActive)
    {
        var group = GetButtonGroup(buttonGroup);

        group.SetActive(shouldActive);

        //for (int i = 0; i < groupTransform.childCount; i++)
        //{
        //    groupTransform.GetChild(i).gameObject.SetActive(shouldActive);
        //}
    }

    private GameObject GetButtonGroup(string groupName)
    {
        for (int i = 0; i < m_buttonRoot.childCount; i++)
        {
            Transform child_i = m_buttonRoot.GetChild(i);
            if (child_i.name == groupName)
            {
                return child_i.gameObject;
            }
        }

        var creatingGroup = new GameObject(groupName);
        creatingGroup.transform.parent = m_buttonRoot;

        var groupLayout = creatingGroup.AddComponent<HorizontalLayoutGroup>();
        groupLayout.childControlHeight = true;
        groupLayout.childControlWidth = true;
        groupLayout.childForceExpandWidth = true;
        groupLayout.childControlHeight = true;
        groupLayout.transform.localScale = Vector3.one;

        return creatingGroup;
    }
}
