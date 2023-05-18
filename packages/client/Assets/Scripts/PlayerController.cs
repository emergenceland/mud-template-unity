#nullable enable
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using IWorld.ContractDefinition;
using mud.Client;
using mud.Unity;
using UniRx;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;

public class PlayerController : MonoBehaviour
{
	private Camera _camera;
	private Vector3? _destination;

	public GameObject destinationMarker;
	private GameObject _destinationMarker;

	private bool _hasDestination;
	private IDisposable? _disposer;
	private TankShooting _target;
	// TODO: Get PlayerSync component
	// TODO: Get NetworkManager 

	void Start()
	{
		_camera = Camera.main;
		var target = FindObjectOfType<TankShooting>();
		if (target == null) return;
		_target = target;
		
		// TODO: Get NetworkManager

		// TODO: Get player sync

		// TODO: Subscribe to Position table
	}

	// TODO: Callback for Position table update
	// private void OnChainPositionUpdate(PositionTableUpdate update)
	// {
	// 	if (_player.key == null || update.Key != _player.key) return;
	// 	if (_player.IsLocalPlayer()) return;
	// 	var currentValue = update.TypedValue.Item1;
	// 	if (currentValue == null) return;
	// 	var x = Convert.ToSingle(currentValue.x);
	// 	var y = Convert.ToSingle(currentValue.y);
	// 	_destination = new Vector3(x, 0, y);
	// }

	
	// TODO: Send tx
	private async UniTaskVoid SendIncrementTxAsync()
	{
		try
		{
			// TODO: Send tx from NetworkManager	
		}
		catch (Exception ex)
		{
			// Handle your exception here
			Debug.LogException(ex);
		}
	}

	void Update()
	{
		var pos = transform.position;
		if (_destination.HasValue && Vector3.Distance(pos, _destination.Value) < 0.5)
		{
			_destination = null;
			if (_destinationMarker != null)
			{
				Destroy(_destinationMarker);
			}
		}
		else
		{
			if (_destination != null)
			{
				var newPosition = Vector3.Lerp(transform.position, _destination.Value, Time.deltaTime);
				var currentTransform = transform;
				currentTransform.position = newPosition;

				// Determine the new rotation
				var lookRotation = Quaternion.LookRotation(_destination.Value - currentTransform.position);
				var newRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime);
				transform.rotation = newRotation;
			}
		}

		// TODO: Early return if not local player
		if (_target.RangeVisible) return;
		if (Input.GetMouseButtonDown(0))
		{
			var ray = _camera.ScreenPointToRay(Input.mousePosition);
			if (!Physics.Raycast(ray, out var hit)) return;
			if (hit.collider.name != "floor-large") return;

			var dest = hit.point;
			dest.x = Mathf.Floor(dest.x);
			dest.y = Mathf.Floor(dest.y);
			dest.z = Mathf.Floor(dest.z);
			_destination = dest;

			if (_destinationMarker != null)
			{
				Destroy(_destinationMarker);
			}

			_destinationMarker = Instantiate(destinationMarker, dest, Quaternion.identity);
			// TODO: Send Tx
		}
	}

	private void OnDestroy()
	{
		_disposer?.Dispose();
	}
}
