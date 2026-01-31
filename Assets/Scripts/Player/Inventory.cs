using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly HashSet<ItemData> _inventory = new HashSet<ItemData>();

	public void AddItem(ItemData item)
    {
        if (!_inventory.Contains(item))
        {
            _inventory.Add(item);
            UIManager.Instance.AddItemUI(item.Image);
        }
        else
        {
            Debug.LogError("[Inventory.AddItem] ERROR. Item " + item.Name + " already present");
        }
    }

    public bool HasItem(ItemData item)
    {
        return _inventory.Contains(item);
	}

    public void RemoveItem(ItemData item)
    {
        if (_inventory.Contains(item))
        {
            _inventory.Remove(item);
			UIManager.Instance.RemoveItemUI(item.Image);
		}
		else
		{
			Debug.LogError("[Inventory.RemoveItem] ERROR. Item " + item.Name + " not present");
		}
	}
}
