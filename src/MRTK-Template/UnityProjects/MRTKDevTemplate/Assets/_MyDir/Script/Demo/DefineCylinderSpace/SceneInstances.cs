using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SceneInstances : MonoBehaviour
{
    [SerializeField] private Transform m_menu;

    public Transform Menu
    {
        get
        {
            Assert.IsNotNull(m_menu);
            return m_menu;
        }
    }
}
