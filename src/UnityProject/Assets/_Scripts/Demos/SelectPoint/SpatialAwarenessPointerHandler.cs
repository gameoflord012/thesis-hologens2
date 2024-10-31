using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Assertions;

public class SpatialAwarenessPointerHandler : MonoBehaviour
{
    [SerializeField] private GameObject spawnRoot;
    [SerializeField] private GameObject spawnPrefab;

    void Start()
    {
        Assert.IsNotNull(spawnRoot);

        GameObject SARoot = CoreServices.SpatialAwarenessSystem.SpatialAwarenessObjectParent;

        var pHandler = SARoot.AddComponent<PointerHandler>();
        pHandler.AddComponent<NearInteractionTouchable>();
        pHandler.OnPointerClicked.AddListener(OnPointerClicked);
    }

    private void OnPointerClicked(MixedRealityPointerEventData data)
    {
        var result = data.Pointer.Result;
        SpawnSphere(result.Details.Point);
    }

    private void SpawnSphere(Vector3 spawnPos)
    {
        var obj = Instantiate(spawnPrefab, spawnPos, Quaternion.identity, spawnRoot.transform);
        Debug.Log("A sphere spawn at: " + spawnPos, obj);
    }
}
