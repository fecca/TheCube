using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class InputManager : MonoSingletone<InputManager>
{
	private Dictionary<KeyCode, Action> _bindings = new Dictionary<KeyCode, Action>();

	public void Bind(KeyCode key, Action action)
	{
		if (_bindings.ContainsKey(key))
		{
			_bindings[key] += action;
		}
		else
		{
			_bindings.Add(key, action);
		}
	}

	private void Update()
	{
		foreach (var binding in _bindings)
		{
			if (Input.GetKeyDown(binding.Key))
			{
				binding.Value.Invoke();
			}
		}
	}
}