using UnityEngine;

public class FullScreenUIItem : SmartObjectInteractable
{
    public ItemData ItemData;
    protected override void Interact()
    {
        base.Interact();

        UIManager.Instance.ShowFullScreenItemUI(ItemData);
    }
}
