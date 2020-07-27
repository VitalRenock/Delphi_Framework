using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Manager<T, DataType> : MonoBehaviour where T : MonoBehaviour where DataType : Data
{
	private static T _I = null;
	public static T I
	{
		get
		{
			//if (_I == null)
			//    Debug.Assert(false, "No Instance of " + typeof(T));

			return _I;
		}
	}

	[ReadOnly] public DataType DataLoaded;
	[ReadOnly] public GameObject GameObjectLoaded;

	private void Awake()
	{
		if (_I != this && _I != null)
		{
			Debug.Log("Manager has destroyed: " + gameObject.name);
			DestroyImmediate(gameObject);
			return;
		}
		_I = this as T;

	}

	public virtual void Load(DataType dataToLoad)
	{
		DataLoaded = dataToLoad;
		GameObjectLoaded = Instantiate(dataToLoad.Prefab);
	}
	public virtual void Unload() 
	{
		DataLoaded = null;
		Destroy(GameObjectLoaded);
	}
}
