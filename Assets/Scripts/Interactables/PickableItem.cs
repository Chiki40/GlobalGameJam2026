using UnityEngine;

public class PickableItem : SmartObjectInteractable
{
	[SerializeField]
    private ItemData[] _itemsToGive = null;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Interact()
    {
		base.Interact();

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
}
