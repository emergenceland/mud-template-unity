using System;
using Cysharp.Threading.Tasks;
using IWorld.ContractDefinition;
using mud.Unity;
using UniRx;
using DefaultNamespace;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;

public class CounterManager : MonoBehaviour
{
	private IDisposable _counterSub;
	public GameObject prefab;
	private NetworkManager net;

	// Start is called before the first frame update
	void Start()
	{
		net = NetworkManager.Instance;
		net.OnNetworkInitialized += SubscribeToCounter;
	}

	private void SubscribeToCounter(NetworkManager _)
	{
		_counterSub = ObservableExtensions.Subscribe(CounterTable.OnRecordUpdate().ObserveOnMainThread(), OnIncrement);
	}


	private void OnIncrement(CounterTableUpdate update)
	{
		var currentValue = update.TypedValue.Item1;
		if (currentValue == null) return;

		Debug.Log("Counter is now: " + currentValue.value);
		SpawnPrefab();
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
			await net.worldSend.TxExecute<IncrementFunction>();
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

	private void OnDestroy()
	{
		_counterSub?.Dispose();
	}
}
