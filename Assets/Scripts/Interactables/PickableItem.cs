using UnityEngine;

public class PickableItem : SmartObjectInteractable
{
	[SerializeField]
	private ItemData[] _requiredItems = null;
	[SerializeField]
    private ItemData[] _itemsToGive = null;

	private Inventory _playerInventory = null;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
		_playerInventory = _playerObject.GetComponent<Inventory>();
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
		base.Interact();

		if (_requiredItems != null && _requiredItems.Length > 0)
		{
			for (int i = 0; i < _requiredItems.Length; ++i)
			{
				if (_playerInventory.HasItem(_requiredItems[i]))
				{
					_playerInventory.RemoveItem(_requiredItems[i]);
				}
			}
		}

		if (_itemsToGive != null && _itemsToGive.Length > 0)
		{
			for (int i = 0; i < _itemsToGive.Length; ++i)
			{
				_playerInventory.AddItem(_itemsToGive[i]);
			}
		}
	}

	protected override void CantInteractFeedback()
	{
		base.CantInteractFeedback();
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
