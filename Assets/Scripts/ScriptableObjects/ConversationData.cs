using UnityEngine;

[CreateAssetMenu(menuName = "Data/Conversation/ConversationData")]
public class ConversationData : ScriptableObject
{
	[SerializeField]
	internal ConversationPieceData[] Texts;
}
