using System;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_headerTMP;
    [SerializeField] private TextMeshProUGUI m_bodyTMP;

    public void SetHeaderText(string theText)
    {
        m_headerTMP.text = theText;    
    }

    public void SetBodyText(string theText)
    {
        m_bodyTMP.text = theText;
    }


}
