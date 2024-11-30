using MixedReality.Toolkit.UX;
using System;
using TMPro;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.Assertions;

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

    public void AddButton(GameObject prefab_AddingButton, string buttonGroup)
    {
        Assert.IsNotNull(prefab_AddingButton.GetComponent<PressableButton>());

        var parent = GetButtonGroupTransform(buttonGroup);
        Instantiate(prefab_AddingButton, parent);
    }

    public void ToggleButtonGroup(string buttonGroup, bool shouldActive)
    {
        var groupTransform = GetButtonGroupTransform(buttonGroup);

        for (int i = 0; i < groupTransform.childCount; i++)
        {
            groupTransform.GetChild(i).gameObject.SetActive(shouldActive);
        }
    }

    private Transform GetButtonGroupTransform(string groupName)
    {
        for (int i = 0; i < m_buttonRoot.childCount; i++)
        {
            Transform child_i = m_buttonRoot.GetChild(i);
            if (child_i.name == groupName)
            {
                return child_i;
            }
        }

        var creatingGroup = Instantiate(m_buttonRoot, m_buttonRoot);
        return creatingGroup;
    }
}
