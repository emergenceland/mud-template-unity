using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IWorld.ContractDefinition;
using mud;
using UniRx;
using DefaultNamespace;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;

public class CounterManager : MonoBehaviour
{
    private IDisposable _counterSub;
    public GameObject prefab;
    private NetworkManager net;
    int lastVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        net = NetworkManager.Instance;
        net.OnNetworkInitialized += SubscribeToCounter;
    }

    private void SubscribeToCounter(NetworkManager _)
    {
        var incrementQuery = new Query().In(CounterTable.CounterRxTable);
        _counterSub = ObservableExtensions.Subscribe(net.ds.RxQuery(incrementQuery).ObserveOnMainThread(), OnIncrement);
    }

    private void OnIncrement((List<RxRecord> SetRecords, List<RxRecord> RemovedRecords) update)
    {
        // first element of tuple is set records, second is deleted records
        foreach (var record in update.SetRecords)
        {
            if(record.value == null) return;

            var currentValue = (ulong)record.value["value"];
            Debug.Log("Counter is now: " + currentValue);

            int change = (int)currentValue - lastVal;
            for (int i = 0; i < change; i++) {
                SpawnPrefab();
            }

            lastVal = (int)currentValue;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendIncrementTxAsync().Forget();
        }
    }

    private async UniTaskVoid SendIncrementTxAsync()
    {
        try
        {
            await net.world.Write<IncrementFunction>();
        }
        catch (Exception ex)
        {
            // Handle your exception here
            Debug.LogException(ex);
        }
    }

    private void SpawnPrefab()
    {
        var randomX = UnityEngine.Random.Range(-1, 1);
        var randomZ = UnityEngine.Random.Range(-1, 1);
        Instantiate(prefab, new Vector3(randomX, 5, randomZ), Quaternion.identity);
    }
}