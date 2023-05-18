using DefaultNamespace;
using IWorld.ContractDefinition;
using mud.Client;
using mud.Unity;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;

public class PlayerManager : MonoBehaviour
{
	private CompositeDisposable _disposers = new();
	public GameObject playerPrefab;
	private NetworkManager net;

	// Start is called before the first frame update
	void Start()
	{
		net = NetworkManager.Instance;
		net.OnNetworkInitialized += Spawn;
	}

	async void Spawn(NetworkManager nm)
	{
		// TODO: Check if current player exists in PlayerTable 
		// TODO: If not, make the Spawn Tx 

		// TODO: Subscribe to PlayerTable
	}

	// TODO: Callback for PlayerTable update
	// private void OnUpdatePlayers(PlayerTableUpdate update)
	// {
	// }

	private void OnDestroy()
	{
		_disposers?.Dispose();
	}
}
