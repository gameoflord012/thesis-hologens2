using MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlacementScriptDemo : MonoBehaviour
{
    private MRTKBaseInteractable m_interactable;

    private void Awake()
    {
        m_interactable = GetComponent<MRTKBaseInteractable>();
    }

    private void Start()
    {
        m_interactable.selectEntered.AddListener(OnSelectedEnter)
    }

    private void OnSelectedEnter(SelectEnterEventArgs args)
    {
        if(args.interactableObject is IRayInteractor
    }
}
