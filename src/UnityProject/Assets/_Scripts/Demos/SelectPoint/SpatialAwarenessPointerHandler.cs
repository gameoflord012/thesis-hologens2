using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpatialAwarenessPointerHandler : MonoBehaviour
{
    [SerializeField] private GameObject spawnRoot;

    void Start()
    {
        Assert.IsNotNull(spawnRoot);

        GameObject SARoot = CoreServices.SpatialAwarenessSystem.SpatialAwarenessObjectParent;
        var pHandler = SARoot.AddComponent<PointerHandler>();

        pHandler.OnPointerClicked.AddListener(OnPointerClicked);
    }

    private void OnPointerClicked(MixedRealityPointerEventData data)
    {
        var result = data.Pointer.Result;
        Debug.Log("A sphere spawn at: " + result.StartPoint);
        SpawnSphere(result.StartPoint);
    }

    private void SpawnSphere(Vector3 spawnPos)
    {
        var obj = new GameObject();
        obj.transform.parent = spawnRoot.transform;
        obj.transform.position = spawnPos;
    }
}
