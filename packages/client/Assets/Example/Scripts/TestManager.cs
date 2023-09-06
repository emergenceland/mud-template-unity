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
using IWorld.ContractDefinition;
using System.Text;
using Nethereum.RPC.TransactionTypes;
using Nethereum.Contracts;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionReceipts;
using Nethereum.RPC.TransactionTypes;
using Nethereum.Web3;

public class TestManager : MonoBehaviour
{
    public static System.Action OnInitialized;
    public static bool TestInitialized = false; 
    public GameObject prefab;
    private NetworkManager net;

    Dictionary<string, BallTest> dictionary;
    int spheres;
    private IDisposable ballSub;

    // Start is called before the first frame update
    async void Start()
    {
        dictionary = new Dictionary<string, BallTest>();
        TestInitialized = false;
        net = NetworkManager.Instance;
        net.OnNetworkInitialized += StartTest;

    }

    void OnDestroy() {
        TestInitialized = false;
        ballSub?.Dispose();
        dictionary = null;
    }


    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SpawnBall();
        }
    }

    private async void StartTest(NetworkManager _)
    {

        var posQuery = new Query().In(PositionTable.ID);
        ballSub = ObservableExtensions.Subscribe(net.ds.RxQuery(posQuery).ObserveOnMainThread(), OnPosition);

        // var query = new Query().In(StateTable.ID);
        // ballSub = ObservableExtensions.Subscribe(net.ds.RxQuery(query).ObserveOnMainThread(), OnState);

        await UpdateAllBalls();

    }   

    async UniTask UpdateAllBalls() {
        while(true) {
            await SendAllUpdate<UpdateAllBallsFunction>();
            await UniTask.Delay(1000);
        }
    }

    private async UniTask SpawnBall() {

        await NetworkManager.Instance.worldSend.TxExecute<SpawnBallFunction>();

        TestInitialized = true;
        OnInitialized?.Invoke();

        Debug.Log("Initialized");
    }

    private void OnPosition((List<Record> SetRecords, List<Record> RemovedRecords) update) {
        // Debug.Log("--TABLE UPDATE-- (" + update.SetRecords.Count.ToString() + ") (" + update.RemovedRecords.Count.ToString() + ")");
        UpdatePosition(update.SetRecords, false);
        UpdatePosition(update.RemovedRecords, true);
    }



    void UpdatePosition(List<Record> records, bool isDelete = false) {

         // first element of tuple is set records, second is deleted records
        foreach (var record in records) {

            var currentValue = record.value;
            if (currentValue == null) Debug.Log("Empty record", this);

            BallTest ball = GetBall(record.key);

            Vector3 newPosition = new Vector3(System.Convert.ToInt32(currentValue["x"]), System.Convert.ToInt32(currentValue["y"]), 0f);
            bool hasWaited = (bool)currentValue["hasWaited"];
            ball.UpdatePosition(newPosition, hasWaited, isDelete ? UpdateType.DeleteRecord : UpdateType.SetRecord);
        }

    }

    private void OnState((List<Record> SetRecords, List<Record> RemovedRecords) update)
    {
        Debug.Log("--TABLE UPDATE--");
        UpdateBall(update.SetRecords, false);
        UpdateBall(update.RemovedRecords, true);
    }



    void UpdateBall(List<Record> records, bool isDelete = false) {

         // first element of tuple is set records, second is deleted records
        foreach (var record in records) {

            var currentValue = record.value;
            if (currentValue == null) Debug.Log("Empty record", this);

            BallTest ball = GetBall(record.key);
                    
            ball = dictionary[record.key];

            StateType newState = (StateType)((UInt64)currentValue["value"]);

            ball.UpdateState(newState, isDelete ? UpdateType.DeleteRecord : UpdateType.SetRecord);

        }

    }

    BallTest GetBall(string key) {

        if (dictionary.ContainsKey(key)) { return dictionary[key]; }

        // Debug.Log("Adding : " + key);
        BallTest ball = (Instantiate(prefab, Vector3.zero - Vector3.right + Vector3.up * spheres, Quaternion.identity) as GameObject).GetComponent<BallTest>();
        ball.name = "Ball " + key;
        ball.key = key;
        ball.keyBytes32 = "0x" + ball.key.Replace("0x", "").PadLeft(64, '0').ToLower();

        dictionary.Add(key, ball);
        spheres++;

        return ball;

    }

    
    
    public async UniTask<bool> SendAllUpdate<TFunction>() where TFunction : FunctionMessage, new() 
    {
        bool success = false;

        while(!success) {

            try {
                success = await NetworkManager.Instance.worldSend.TxExecute<TFunction>();
            }
            catch (System.Exception ex) {
                // Handle your exception here
                Debug.LogException(ex, this);
                success = false;
            }

            if(success == false)
                await UniTask.Delay(1000);

        }

        return success;

    }


}
