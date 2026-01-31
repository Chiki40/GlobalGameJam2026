using UnityEngine;

public class NPCInteractable : SmartObjectInteractable
{
    public ConversationData Conversation;
    public override void Interact()
    {
        base.Interact();

        PlayerObject.GetComponent<ConversationManager>().StartConversation(Conversation);
    }
}
