using System;
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
    private IDisposable _counterSub;
    public GameObject prefab;
    private NetworkManager net;

    Dictionary<string, BallTest> dictionary = new Dictionary<string, BallTest>();
    List<BallTest> balls = new List<BallTest>();
    int spheres;

    // Start is called before the first frame update
    void Start()
    {
        net = NetworkManager.Instance;
        net.OnNetworkInitialized += StartTest;
    }

    private void StartTest(NetworkManager _)
    {
        var query = new Query().In(StateTable.ID);
        _counterSub = ObservableExtensions.Subscribe(net.ds.RxQuery(query).ObserveOnMainThread(), OnState);

        Test();

    }


    private void OnTester((List<Record> SetRecords, List<Record> RemovedRecords) update)
    {
        // first element of tuple is set records, second is deleted records
        foreach (var record in update.SetRecords)
        {

            var currentValue = record.value;
            if (currentValue == null) return;

            Debug.Log("Adding : " + record.key);
            BallTest ball = (Instantiate(prefab, Vector3.up + Vector3.right * spheres, Quaternion.identity) as GameObject).GetComponent<BallTest>();

            dictionary.Add(record.key, ball);
            balls.Add(ball);
            spheres++;
        }
    }

    private void OnState((List<Record> SetRecords, List<Record> RemovedRecords) update)
    {
        // first element of tuple is set records, second is deleted records
        foreach (var record in update.SetRecords)
        {
            var currentValue = record.value;
            if (currentValue == null) return;

            if (!dictionary.ContainsKey(record.key))
            {
                Debug.Log("Adding : " + record.key);
                BallTest ball = (Instantiate(prefab, Vector3.up + Vector3.right * spheres, Quaternion.identity) as GameObject).GetComponent<BallTest>();
                ball.name = "Ball " + record.key;
                ball.key = record.key;
                dictionary.Add(record.key, ball);
                balls.Add(ball);
                spheres++;
            }

            BallTest updatedBall = dictionary[record.key];

            StateType newState = (StateType)((UInt64)currentValue["value"]);
            Debug.Log("Updated: " + newState.ToString(), updatedBall);
            updatedBall.UpdateState(newState);

        }
    }

    async UniTask Test()
    {

        Debug.Log("Test LOADED");

        // while (balls.Count < 5) { await UniTask.Delay(100); }

        Debug.Log("Test STARTING");

        for (int i = 0; i < 5; i++)
        {
            while (await SpawnBall(i) == false)
            {
                await UniTask.Delay(2500);
            }

            await UniTask.Delay(2500);
        }

        while (true)
        {

            Debug.Log("Test Loop");

            for (int i = 0; i < balls.Count; i++)
            {

                StateType lastType = balls[i].state;

                while (await IncrementState(i) == false)
                {
                    await UniTask.Delay(2500);
                }

                await UniTask.Delay(2500);

                Debug.Assert(balls[i].state != lastType, balls[i]);
            }
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


    private async UniTask<bool> IncrementState(int index)
    {
        try
        {
            await net.worldSend.TxExecute<NextStateFunction>(index);
            return true;
        }
        catch (Exception ex)
        {
            // Handle your exception here
            Debug.LogException(ex);
            return false;
        }
    }

    private void OnDestroy()
    {
        _counterSub?.Dispose();
    }
}
