using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class InputsManager : Singleton<InputsManager>
{
	[TabGroup("Axis Events")] [DisableInPlayMode]
	public List<AxisEvent> AxisEvents;

	[TabGroup("Keyboard Events")] [DisableInPlayMode]
	public List<InputKeyEvent> KeyDownEvents;
	[TabGroup("Keyboard Events")] [DisableInPlayMode]
	public List<InputKeyEvent> KeyEvents;
	[TabGroup("Keyboard Events")] [DisableInPlayMode]
	public List<InputKeyEvent> KeyUpEvents;

	[TabGroup("Keyboard Events")] [DisableInPlayMode]
	public MouseClickTerrainEvent onMouseClickOnTerrain = new MouseClickTerrainEvent();


	void Update()
	{
		CheckAxis();
		CheckKeyDown();
		CheckKey();
		CheckKeyUp();
	}

	void CheckAxis()
	{
		foreach (AxisEvent axisEvent in AxisEvents)
			if (Input.GetAxis(axisEvent.AxisName) != 0)
				axisEvent.onAxis?.Invoke(Input.GetAxis(axisEvent.AxisName));
	}
	void CheckKeyDown()
	{
		foreach (InputKeyEvent inputKeyEvent in KeyDownEvents)
			if (Input.GetKeyDown(inputKeyEvent.KeyCode))
				inputKeyEvent.KeyEvent?.Invoke();
	}
	void CheckKey()
	{
		foreach (InputKeyEvent inputKeyEvent in KeyEvents)
			if (Input.GetKey(inputKeyEvent.KeyCode))
				inputKeyEvent.KeyEvent?.Invoke();
	}
	void CheckKeyUp()
	{
		foreach (InputKeyEvent inputKeyEvent in KeyUpEvents)
			if (Input.GetKey(inputKeyEvent.KeyCode))
				inputKeyEvent.KeyEvent?.Invoke();
	}

	public bool ReadUndoInput()
	{
		if (Input.GetKey(KeyCode.KeypadEnter))
			return true;
		else
			return false;
	}


	// Temporary For Move Enemy on Terrain
	public void SendClickPosition()
	{
		Vector3? destination = GetClickPosition();

		if (destination != null)
			onMouseClickOnTerrain?.Invoke((Vector3)destination);
	}
	public Vector3? GetClickPosition()
	{
		RaycastHit raycastHit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out raycastHit))
			return raycastHit.point;
		else
			return null;
	}
}

[System.Serializable] public class AxisEvent
{
	public string AxisName;
	public MouseAxisEvent onAxis;
}
[System.Serializable] public class MouseAxisEvent : UnityEvent<float> { }
[System.Serializable] public class InputKeyEvent
{
	public string Name;
	public KeyCode KeyCode;
	public UnityEvent KeyEvent = new UnityEvent();
}

// Temporary For Move Enemy on Terrain
[System.Serializable]
public class MouseClickTerrainEvent : UnityEvent<Vector3> { }