using UnityEngine;

public class NPCInteractable : SmartObjectInteractable
{
	private Animator _animator = null;

    public ConversationData Conversation;

	protected override void Start()
	{
		base.Start();
		_animator = GetComponentInChildren<Animator>();
	}

	protected override bool CanInteractNoFeedback()
	{
		return base.CanInteractNoFeedback() && !_maskController.IsMaskOn;
	}

	protected override void Interact()
    {
		void OnConversationEnd()
		{
			_animator.CrossFadeInFixedTime("Idle", 0.0f);
		}

		base.Interact();
		_animator.CrossFadeInFixedTime("Talk", 0.0f);
		ConversationManager.Instance.StartConversation(Conversation, OnConversationEnd);
    }
}
