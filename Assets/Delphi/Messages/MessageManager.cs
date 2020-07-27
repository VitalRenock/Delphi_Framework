using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Sirenix.OdinInspector;


[RequireComponent(typeof(VerticalLayoutGroup), typeof(ContentSizeFitter))]
public class MessageManager : Singleton<MessageManager>
{
	public float TimeDisplayed;
	public float TimeBeforePost;
	public GameObject PrefabMessage;
	public UnityEvent onMessagePosted;

	[ShowInInspector]
	Queue<GameObject> messagesDisplayed = new Queue<GameObject>();


	public void Post(string title, string text)
	{
		MessageData message = ScriptableObject.CreateInstance(typeof(MessageData)) as MessageData;
		message.Title = title;
		message.ColorTitle = Color.red;
		message.Text = text;
		message.ColorText = Color.white;
		message.PrefabMessage = PrefabMessage;

		Post(message);
	}
	public void Post(string title, string text, float timeDisplayed)
	{
		MessageData message = ScriptableObject.CreateInstance(typeof(MessageData)) as MessageData;
		message.Title = title;
		message.ColorTitle = Color.red;
		message.Text = text;
		message.ColorText = Color.white;
		message.PrefabMessage = PrefabMessage;

		Post(message, timeDisplayed);
	}
	public void Post(MessageData message)
	{
		InstantiateMessage(message);
	}
	public void Post(List<MessageData> messages)
	{
		foreach (MessageData message in messages)
			InstantiateMessage(message);
	}
	public void Post(MessageData message, float timeDisplayed)
	{
		InstantiateMessage(message);
		StartCoroutine(RemoveAfterSecondes(timeDisplayed));
	}
	public void Post(List<MessageData> messages, float timeDisplayed)
	{
		foreach (MessageData message in messages)
		{
			InstantiateMessage(message);
			StartCoroutine(RemoveAfterSecondes(timeDisplayed));
		}
	}
	public void Post(MessageData message, float timeDisplayed, float timeBeforePost)
	{
		StartCoroutine(InstantiateDelayedMessage(message, timeDisplayed, timeBeforePost));
	}
	public void Post(List<MessageData> messages, float timeDisplayed, float timeBeforePost)
	{
		StartCoroutine(InstantiateDelayedMessage(messages, timeDisplayed, timeBeforePost));
	}
	public void RemoveAllMessagesDisplayed()
	{
		StopAllCoroutines();

		while (messagesDisplayed.Count > 0)
		{
			GameObject messageToDelete = messagesDisplayed.Dequeue();
			Destroy(messageToDelete);
		}
	}


	void InstantiateMessage(MessageData message)
	{
		GameObject gameObjectToInstaniate = Instantiate(message.PrefabMessage, transform);
		SetMessage(gameObjectToInstaniate, message);
		messagesDisplayed.Enqueue(gameObjectToInstaniate);

		onMessagePosted?.Invoke();
	}
	void SetMessage(GameObject gameObjectToInstantiate, MessageData message)
	{
		Text textComponent = gameObjectToInstantiate.transform.GetChild(0).GetComponent<Text>();
		textComponent.text = message.Title;
		textComponent.color = message.ColorTitle;
		textComponent = gameObjectToInstantiate.transform.GetChild(1).GetComponent<Text>();
		textComponent.text = message.Text;
		textComponent.color = message.ColorText;
		gameObjectToInstantiate.transform.GetChild(2).GetComponent<Image>().sprite = message.SpriteSide;
		gameObjectToInstantiate.transform.GetComponent<Image>().sprite = message.SpriteBackground;
	}
	IEnumerator InstantiateDelayedMessage(MessageData message, float timeDisplayed, float timeBeforePost)
	{
		yield return new WaitForSeconds(timeBeforePost);
		InstantiateMessage(message);
		StartCoroutine(RemoveAfterSecondes(timeDisplayed));
	}
	IEnumerator InstantiateDelayedMessage(List<MessageData> messages, float timeDisplayed, float timeBeforePost)
	{
		foreach (MessageData message in messages)
		{
			yield return new WaitForSeconds(timeBeforePost);
			InstantiateMessage(message);
			StartCoroutine(RemoveAfterSecondes(timeDisplayed));
		}
	}
	IEnumerator RemoveAfterSecondes(float time)
	{
		yield return new WaitForSeconds(time);
		GameObject messageToDelete = messagesDisplayed.Dequeue();
		Destroy(messageToDelete);
	}
}