using UnityEngine;

public class NPCInteractable : SmartObjectInteractable
{
    public ConversationData Conversation;
    protected override void Interact()
    {
		ConversationManager.Instance.StartConversation(Conversation);

		base.Interact();
    }
}
