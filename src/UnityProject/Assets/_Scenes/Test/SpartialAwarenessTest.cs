using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpartialAwarenessTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Use CoreServices to quickly get access to the IMixedRealitySpatialAwarenessSystem
        var spatialAwarenessService = CoreServices.SpatialAwarenessSystem;
        var dataProviderAccess = spatialAwarenessService as IMixedRealityDataProviderAccess;

        // Get the SpatialObjectMeshObserver specifically
        var meshObserverName = "Spatial Object Mesh Observer";
        var spatialObjectMeshObserver = dataProviderAccess.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>(meshObserverName);

        Assert.IsNotNull(spatialObjectMeshObserver);
    }
}
