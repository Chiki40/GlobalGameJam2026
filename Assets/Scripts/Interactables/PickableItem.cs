using UnityEngine;

public class PickableItem : SmartObjectInteractable
{
    public ItemData ItemToGive;
	protected override void Interact()
    {
        base.Interact();

        Inventory InventoryComponent = PlayerObject.GetComponent<Inventory>();
        InventoryComponent.AddItem(ItemToGive);

        Destroy(gameObject);
    }
}
