using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager _instance = null;
	public static UIManager Instance => _instance;

	[SerializeField]
	private Transform _inventoryParent = null;
	[SerializeField]
	private Image _itemUIPrefab = null;
	[SerializeField]
	private GameObject _conversationParent = null;
	[SerializeField]
	private TextMeshProUGUI _conversationText = null;

	private int _currentNumItems = 0;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
			Init();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Init()
	{
		_conversationParent.SetActive(false);
	}

	public void AddItemUI(Sprite sprite)
    {
		Image image = Instantiate(_itemUIPrefab, _inventoryParent);
		image.sprite = sprite;
		++_currentNumItems;
    }

	public void RemoveItemUI(Sprite sprite)
	{
		for (int i = 0; i < _currentNumItems; ++i)
		{
			if (_inventoryParent.GetChild(i).GetComponent<Image>().sprite == sprite)
			{
				Destroy(_inventoryParent.GetChild(i).gameObject);
				--_currentNumItems;
				break;
			}
		}
	}

	public void ShowConversationUI()
	{
		_conversationParent.SetActive(true);
	}
	public void HideConversationUI()
	{
		_conversationParent.SetActive(false);
	}
	public void SetConversationText(string text)
	{
		_conversationText.text = text;
	}
}
