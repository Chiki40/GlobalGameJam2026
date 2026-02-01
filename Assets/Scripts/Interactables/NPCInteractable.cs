using Unity.Cinemachine;
using UnityEngine;

public class NPCInteractable : SmartObjectInteractable
{
	[SerializeField]
	private ConversationData _conversation = null;

	private Animator _animator = null;
	private Inventory _playerInventory = null;
	private CinemachineCamera _dialogueCamera = null;

	protected override void Start()
	{
		base.Start();
		_animator = GetComponentInChildren<Animator>();
		_playerInventory = _playerObject.GetComponent<Inventory>();
		_dialogueCamera = transform.parent.GetComponentInChildren<CinemachineCamera>();
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
			_dialogueCamera.Priority = 0;
		}

		base.Interact();
		_dialogueCamera.Priority = 1000;
		_animator.CrossFadeInFixedTime("Talk", 0.0f);
		ConversationManager.Instance.StartConversation(_conversation, OnConversationEnd);
    }

	public void GiveItem(ItemData item)
	{
		_playerInventory.AddItem(item);
	}
}
