using UnityEngine;

[CreateAssetMenu(menuName = "Data/Conversation/ConversationPieceData")]
public class ConversationPieceData : ScriptableObject
{
	[SerializeField]
	internal string Text;
	[SerializeField]
	internal float TextAppearTimeBetweenCharsMultiplier = 1.0f;
}
