using UnityEngine;

public class PickableItem : SmartObjectInteractable
{
	[SerializeField]
	private ItemData _requiredItem = null;
	[SerializeField]
    private ItemData _itemToGive = null;

	private Inventory _playerInventory = null;

	protected override void Awake()
	{
		base.Awake();
        GetComponent<SpriteRenderer>().sprite = _itemToGive.Image;
	}

	protected override void Start()
	{
		base.Start();
		_playerInventory = _playerObject.GetComponent<Inventory>();
	}

	protected override bool CanInteract()
	{
		return base.CanInteract() &&
			(_requiredItem == null ||
				(_playerInventory != null && _playerInventory.HasItem(_requiredItem)));
	}

	protected override void Interact()
    {
        base.Interact();

		if (_requiredItem != null)
		{
			_playerInventory.RemoveItem(_requiredItem);
		}

		if (_itemToGive != null)
		{
			_playerInventory.AddItem(_itemToGive);
		}

        Destroy(gameObject);
    }
}
