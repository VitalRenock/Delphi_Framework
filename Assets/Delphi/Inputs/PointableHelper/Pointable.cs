using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointable : MonoBehaviour, IPointable
{
	[TabGroup("Events")] public UnityEvent onPointerEnter = new UnityEvent();
	[TabGroup("Events")] public UnityEvent onPointerClick = new UnityEvent();
	[TabGroup("Events")] public UnityEvent onPointerExit = new UnityEvent();
	[TabGroup("Events")] public UnityEvent onPointerDown = new UnityEvent();
	[TabGroup("Events")] public UnityEvent onPointerUp = new UnityEvent();

	[TabGroup("Events")] public UnityEvent<PointerEventData> onPointerEnterEventData;
	[TabGroup("Events")] public UnityEvent<PointerEventData> onPointerClickEventData;
	[TabGroup("Events")] public UnityEvent<PointerEventData> onPointerExitEventData;
	[TabGroup("Events")] public UnityEvent<PointerEventData> onPointerDownEventData;
	[TabGroup("Events")] public UnityEvent<PointerEventData> onPointerUpEventData;

	[TabGroup("Coloration")] public bool ColorObject;
	[TabGroup("Coloration")][ShowIf("ColorObject")] public ColorBlock ColorBlock;


	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		onPointerEnter?.Invoke();
		onPointerEnterEventData?.Invoke(eventData);

		if (ColorObject)
			GetComponent<MeshRenderer>().material.color = ColorBlock.highlightedColor;
	}
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		onPointerClick?.Invoke();
		onPointerClickEventData?.Invoke(eventData);

		if (ColorObject)
			GetComponent<MeshRenderer>().material.color = ColorBlock.pressedColor;
	}
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		onPointerExit?.Invoke();
		onPointerExitEventData?.Invoke(eventData);

		if (ColorObject)
			GetComponent<MeshRenderer>().material.color = ColorBlock.normalColor;
	}
	public virtual void OnPointerDown(PointerEventData eventData)
	{
		onPointerDown?.Invoke();
		onPointerDownEventData?.Invoke(eventData);

		if (ColorObject)
			GetComponent<MeshRenderer>().material.color = ColorBlock.pressedColor;
	}
	public virtual void OnPointerUp(PointerEventData eventData)
	{
		onPointerUp?.Invoke();
		onPointerUpEventData?.Invoke(eventData);

		if (ColorObject)
			GetComponent<MeshRenderer>().material.color = ColorBlock.normalColor;
	}
}