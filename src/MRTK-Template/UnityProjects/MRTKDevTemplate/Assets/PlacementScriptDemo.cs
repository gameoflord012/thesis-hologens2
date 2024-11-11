using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
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
        m_interactable.selectEntered.AddListener(OnSelectedEnter);
    }

    private void OnSelectedEnter(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject;
        Debug.Log(
                "Transform: " + interactor.GetAttachTransform(args.interactableObject),
                interactor.GetAttachTransform(args.interactableObject).gameObject);
    }
}
