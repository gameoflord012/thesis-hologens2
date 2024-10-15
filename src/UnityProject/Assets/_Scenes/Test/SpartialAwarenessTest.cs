using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;
using Microsoft.MixedReality.Toolkit.SpatialObjectMeshObserver;

public class SpartialAwarenessTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitMeshObserver();
        //StartStopMeshObserverTest();
        EnumMeshes();
    }



    private void EnumMeshes()
    {
        int count = 0;
        foreach(SpatialAwarenessMeshObject meshObject in meshObserver.Meshes.Values)
        {
            Mesh mesh = meshObject.Filter.mesh;
            count++;
        }

        Assert.IsTrue(count > 0);
    }

    private void StartStopMeshObserverTest()
    {
        meshObserver.Suspend();
        meshObserver.Resume();
    }

    private void InitMeshObserver()
    {
        // Use CoreServices to quickly get access to the IMixedRealitySpatialAwarenessSystem
        var spatialAwarenessService = CoreServices.SpatialAwarenessSystem;
        var dataProviderAccess = spatialAwarenessService as IMixedRealityDataProviderAccess;

        // Get the SpatialObjectMeshObserver specifically
        var meshObserverName = "Spatial Object Mesh Observer";
        meshObserver = dataProviderAccess.GetDataProvider<SpatialObjectMeshObserver>(meshObserverName);

        Assert.IsNotNull(meshObserver);
    }

    private SpatialObjectMeshObserver meshObserver;
}
