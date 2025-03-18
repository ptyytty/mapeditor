using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonHelper : MonoBehaviour
{
	public static T[] FromJson<T>(string json) {
		string newJson = "{\"items\":" + json + "}";
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
		return wrapper.items;
	}

	[System.Serializable]
	private class Wrapper<T>
	{
		public T[] items;
	}
}
