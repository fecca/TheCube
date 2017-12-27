using System;
using System.Collections;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
	[SerializeField]
	private bool _spawnObject;
	[SerializeField]
	private bool _auto;
	[SerializeField]
	private float _autoTimer = 1.0f;
	[SerializeField]
	private Renderer[] _wallRenderers;
	[SerializeField]
	private float _rotationSpeed = 1.0f;
	[SerializeField]
	private float _dissolveSpeed = 1.0f;

	private bool _isRotating;
	private float _timer;
	private int _callbacks = 2;

	private void Start()
	{
		InputManager.Instance.Bind(KeyCode.Keypad1, RotateLeft);
		InputManager.Instance.Bind(KeyCode.Keypad3, RotateRight);
		InputManager.Instance.Bind(KeyCode.Keypad4, RotateDown);
		InputManager.Instance.Bind(KeyCode.Keypad6, RotateUp);
		InputManager.Instance.Bind(KeyCode.Keypad7, RotateBack);
		InputManager.Instance.Bind(KeyCode.Keypad9, RotateForward);

		StartRotating(Vector3.zero);
	}

	private void Update()
	{
		if (!_auto)
		{
			return;
		}

		if (_isRotating)
		{
			return;
		}

		if (_timer >= _autoTimer)
		{
			_timer = 0f;

			var random = UnityEngine.Random.Range(0, 5);
			var direction = random == 0 ? Vector3.right : random == 1 ? -Vector3.right : random == 2 ? Vector3.up : random == 3 ? -Vector3.up : random == 4 ? Vector3.forward : -Vector3.forward;

			StartRotating(direction);

			if (_spawnObject)
			{
				var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.SetParent(transform);
				var rigidbody = sphere.AddComponent<Rigidbody>();
				rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				sphere.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
			}
		}
		_timer += Time.deltaTime;
	}

	private void RotateLeft()
	{
		if (_isRotating)
		{
			return;
		}

		StartRotating(Vector3.left);
	}

	private void RotateDown()
	{
		if (_isRotating)
		{
			return;
		}

		StartRotating(Vector3.down);
	}

	private void RotateBack()
	{
		if (_isRotating)
		{
			return;
		}

		StartRotating(Vector3.back);
	}

	private void RotateRight()
	{
		if (_isRotating)
		{
			return;
		}

		StartRotating(Vector3.right);
	}

	private void RotateUp()
	{
		if (_isRotating)
		{
			return;
		}

		StartRotating(Vector3.up);
	}

	private void RotateForward()
	{
		if (_isRotating)
		{
			return;
		}

		StartRotating(Vector3.forward);
	}

	private void StartRotating(Vector3 direction)
	{
		_isRotating = true;

		StartCoroutine(Rotate(direction, Callback));
	}

	private IEnumerator Rotate(Vector3 axis, Action callback)
	{
		var finalRotation = Quaternion.Euler(axis * 90) * transform.rotation;
		var t = 0f;
		while (transform.rotation != finalRotation)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, t * _rotationSpeed);

			t += Time.deltaTime;

			yield return null;
		}
		transform.rotation = finalRotation;

		foreach (var renderer in _wallRenderers)
		{
			StartCoroutine(FadeWall(renderer, Callback));
		}
	}

	private IEnumerator FadeWall(Renderer renderer, Action callback)
	{
		var current = renderer.material.GetFloat("_SliceAmount");
		var from = 0f;
		var to = 1f;
		if (renderer.transform.forward == -Vector3.right || renderer.transform.forward == -Vector3.forward || renderer.transform.forward == Vector3.up)
		{
			if (current == 1.0f)
			{
				yield break;
			}

			from = 0.0f;
			to = 1.0f;
		}
		else
		{
			if (current == 0.0f)
			{
				yield break;
			}

			from = 1.0f;
			to = 0.0f;
		}

		var t = 0f;
		var sliceAmount = from;
		while (sliceAmount != to)
		{
			sliceAmount = Mathf.Lerp(from, to, t * _dissolveSpeed);
			renderer.material.SetFloat("_SliceAmount", sliceAmount);
			t += Time.deltaTime;

			yield return null;
		}

		callback();
	}

	private void Callback()
	{
		_callbacks--;

		if (_callbacks == 0)
		{
			_isRotating = false;
			_callbacks = 2;
		}
	}
}
