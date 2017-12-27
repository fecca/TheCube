using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util
{
	public static class ExtensionMethods
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable == null || !enumerable.Any();
		}

		public static void SetXPosition(this Transform transform, float x)
		{
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
		}

		public static void SetYPosition(this Transform transform, float y)
		{
			transform.position = new Vector3(transform.position.x, y, transform.position.z);
		}

		public static void SetZPosition(this Transform transform, float z)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, z);
		}

		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			var component = gameObject.GetComponent<T>();
			if (component == null)
			{
				component = gameObject.AddComponent<T>();
			}

			return component;
		}
	}
}