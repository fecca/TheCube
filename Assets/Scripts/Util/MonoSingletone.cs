using UnityEngine;

namespace Util
{
	public class MonoSingletone<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();
					if (_instance == null)
					{
						var newGameObject = new GameObject(typeof(T).Name, typeof(T));
						_instance = newGameObject.GetComponent<T>();
					}
				}
				return _instance;
			}
		}
	}
}