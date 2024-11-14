using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SceneInstances : MonoBehaviour
{
    [SerializeField] private Transform m_buttonSlotsRoot;

    public Transform ButtonSlotsRoot
    {
        get
        {
            Assert.IsNotNull(m_buttonSlotsRoot);
            return m_buttonSlotsRoot;
        }
    }
}
