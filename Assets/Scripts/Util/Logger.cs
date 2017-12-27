using UnityEngine;

namespace Util
{
	public class Logging
	{
		public static void Log(object message, LogType logType = LogType.Log, GameObject gameObject = null)
		{
			switch (logType)
			{
				case LogType.Error:
					Debug.LogError(message, gameObject);
					break;
				case LogType.Warning:
					Debug.LogWarning(message, gameObject);
					break;
				case LogType.Log:
					Debug.Log(message, gameObject);
					break;
				default:
					break;
			}
		}
	}
}