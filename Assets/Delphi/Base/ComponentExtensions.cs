using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class ComponentExtensions
{
	#region Vectors Extensions

	public static Vector2 Round(this Vector2 vector)
	{
		return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
	}
	public static Vector3 Round(this Vector3 vector)
	{
		return new Vector3(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));
	}

	public static Vector2 RandomRange(this Vector2 vector, float minrange, float maxRange)
	{
		return new Vector2(Random.Range(minrange, maxRange), Random.Range(minrange, maxRange));
	}
	public static Vector3 RandomRange(this Vector3 vector, float minrange, float maxRange)
	{
		return new Vector3(Random.Range(minrange, maxRange), Random.Range(minrange, maxRange), Random.Range(minrange, maxRange));
	}

	#endregion

	#region List<T> Extensions

	public static void Swap<T>(this List<T> list, int indexA, int indexB)
	{
		if (indexA > list.Count || indexB > list.Count)
			Debug.LogWarning("Switch items impossible, an item(s) is out of range of the list");
		else
		{
			T tmp = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = tmp;
		}
	}

	#endregion

	#region GameObject Extensions

	public static T[] AddComponents<T>(this GameObject gameObject, int number = 1) where T : Component
	{
		T[] components = new T[number];
		for (int i = 0; i < number; i++)
			components[i] = gameObject.AddComponent<T>();

		return components;
	}

	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T component;

		if (!gameObject.TryGetComponent(out component))
			component = gameObject.AddComponent<T>();

		return component;
	}
	public static List<T> GetOrAddComponents<T>(this GameObject gameObject, int number = 1) where T : Component
	{
		List<T> components = gameObject.GetComponents<T>().ToList();

		int count = number - components.Count;
		for (int i = 0; i < count; i++)
			components.Add(gameObject.AddComponent<T>());

		return components;
	}

	public static GameObject AddChild(this GameObject gameObject, string name = "New GameObject")
	{
		GameObject child = new GameObject(name);
		child.transform.SetParent(gameObject.transform);
		return child;
	}
	public static List<GameObject> AddChilds(this GameObject gameObject, int count, string name = "New GameObject")
	{
		List<GameObject> childs = new List<GameObject>();

		for (int i = 0; i < count; i++)
			childs.Add(AddChild(gameObject, name + "_" + i.ToString()));

		return childs;
	}

	#endregion
}