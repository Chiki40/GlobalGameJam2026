using UnityEngine;

public class NPCInteractable : SmartObjectInteractable
{
    public ConversationData Conversation;
    protected override void Interact()
    {
        base.Interact();
        ConversationManager.Instance.StartConversation(Conversation);
    }
}
