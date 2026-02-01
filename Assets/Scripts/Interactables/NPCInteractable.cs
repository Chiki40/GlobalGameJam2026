using Unity.Cinemachine;
using UnityEngine;

public class NPCInteractable : SmartObjectInteractable
{
	[SerializeField]
	private ItemData[] _requiredItems = null;
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

	protected override bool CanInteract()
	{
		bool canInteract = base.CanInteract();

		if (canInteract)
		{
			if (_requiredItems != null && _requiredItems.Length > 0)
			{
				for (int i = 0; i < _requiredItems.Length; ++i)
				{
					if (!_playerInventory.HasItem(_requiredItems[i]))
					{
						canInteract = false;
						break;
					}
				}
			}
		}

		return canInteract;
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
