using UnityEngine;

public class PickableItem : SmartObjectInteractable
{
    [SerializeField]
    private ItemData _itemToGive = null;

	protected override void Awake()
	{
		base.Awake();
        GetComponent<SpriteRenderer>().sprite = _itemToGive.Image;
	}

	protected override void Interact()
    {
        base.Interact();

        Inventory InventoryComponent = PlayerObject.GetComponent<Inventory>();
        InventoryComponent.AddItem(_itemToGive);

        Destroy(gameObject);
    }
}
