using UnityEngine;
using UnityEngine.UI;

public class FullScreenItemUI : MonoBehaviour
{
    public Image ItemImage;

    public void ShowItem(ItemData item)
    {
        gameObject.SetActive(true);
        ItemImage.sprite = item.Image;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
