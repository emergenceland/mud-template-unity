using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IWorld.ContractDefinition;
using mud.Unity;
using UniRx;
using DefaultNamespace;
using mud.Client;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;


public class TestManager : MonoBehaviour
{
    public static bool TestInitialized = false; 
    private IDisposable _counterSub;
    public GameObject prefab;
    private NetworkManager net;

    Dictionary<string, BallTest> dictionary;
    int spheres;

    // Start is called before the first frame update
    void Start()
    {
        dictionary = new Dictionary<string, BallTest>();
        TestInitialized = false;
        net = NetworkManager.Instance;
        net.OnNetworkInitialized += StartTest;
    }

    void OnDestroy() {
        TestInitialized = false;
        _counterSub?.Dispose();
        dictionary = null;
    }

    private void StartTest(NetworkManager _)
    {
        var query = new Query().In(StateTable.ID);
        _counterSub = ObservableExtensions.Subscribe(net.ds.RxQuery(query).ObserveOnMainThread(), OnState);
        
        StartCoroutine(StartTest());
    }   

    private IEnumerator StartTest() {

        yield return new WaitForSeconds(2.5f);

        TestInitialized = true;
        Debug.Log("Initialized");
    }


    private void OnState((List<Record> SetRecords, List<Record> RemovedRecords) update)
    {
        UpdateBall(update.SetRecords, false);
        UpdateBall(update.RemovedRecords, true);
    }

    void UpdateBall(List<Record> records, bool isDelete = false) {

         // first element of tuple is set records, second is deleted records
        foreach (var record in records)
        {
            var currentValue = record.value;

            if (currentValue == null) Debug.Log("Empty record", this);

            BallTest ball = null;

            bool spawning = !dictionary.ContainsKey(record.key);
            if (spawning)
            {
                Debug.Log("Adding : " + record.key);
                ball = (Instantiate(prefab, Vector3.up + Vector3.right * spheres * 2f + Vector3.right, Quaternion.identity) as GameObject).GetComponent<BallTest>();
                ball.name = "Ball " + record.key;
                ball.key = record.key;
                ball.ballIndex = spheres;
                ball.keyBytes32 = "0x" + ball.key.Replace("0x", "").PadLeft(64, '0').ToLower();

                dictionary.Add(record.key, ball);
                spheres++;
            }

            ball = dictionary[record.key];

            StateType newState = (StateType)((UInt64)currentValue["value"]);

            if (spawning) { 
                ball.transactionState = newState; 
            } else {

            }

            ball.UpdateState(newState, isDelete ? UpdateType.DeleteRecord : UpdateType.SetRecord);

        }

    }



        private async UniTask<bool> SpawnBall(int index)
    {
        try
        {
            await net.worldSend.TxExecute<SpawnBallFunction>(index);
            return true;
        }
        catch (Exception ex)
        {
            // Handle your exception here
            Debug.LogException(ex);
            return false;
        }
    }


}
