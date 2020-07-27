using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Message_X", menuName = "New Message")]
public class MessageData : ScriptableObject
{
	public string Title;
	public Color ColorTitle;
	public string Text;
	public Color ColorText;
	public Sprite SpriteSide;
	public Sprite SpriteBackground;
	[AssetsOnly] public GameObject PrefabMessage;
}