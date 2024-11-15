using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SceneInstances : MonoBehaviour
{
    [SerializeField] private MenuController m_menu;

    public MenuController Menu
    {
        get
        {
            Assert.IsNotNull(m_menu);
            return m_menu;
        }
    }
}
