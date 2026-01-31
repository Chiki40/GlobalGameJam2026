using UnityEngine;

public class NPCInteractable : SmartObjectInteractable
{
    public ConversationData Conversation;

	protected override bool CanInteractNoFeedback()
	{
		return base.CanInteractNoFeedback() && !_maskController.IsMaskOn;
	}

	protected override void Interact()
    {
		base.Interact();
		ConversationManager.Instance.StartConversation(Conversation);
    }
}
