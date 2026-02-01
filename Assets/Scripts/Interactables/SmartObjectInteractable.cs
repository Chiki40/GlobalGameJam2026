using UnityEngine;
using UnityEngine.Events;

public abstract class SmartObjectInteractable : MonoBehaviour
{
	[SerializeField]
	private string _interactSFX = null;
	[SerializeField]
	private UnityEvent _onInteracted = null;
	[SerializeField]
	private ItemData[] _requiredItems = null;
	[SerializeField]
	private UnityEvent _onCantInteractFeedback = null;

	protected GameObject _playerObject;
	protected MaskController _maskController = null;
	protected Inventory _playerInventory = null;

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("Player");
		_maskController = _playerObject.GetComponent<MaskController>();
		_playerInventory = _playerObject.GetComponent<Inventory>();
	}

	protected virtual bool CanInteractNoFeedback()
	{
		return GameManager.Instance.ControlsEnabled && GameManager.Instance.NumEnemiesPursuing == 0 && !_maskController.MaskAnimationInProgress;
	}

	protected virtual bool CanInteract()
    {
		bool canInteract = true;

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

    protected virtual void Interact()
    {
		Debug.Log(name + "Interacted");
		if (!string.IsNullOrEmpty(_interactSFX))
		{
			UtilSound.Instance.PlaySound(_interactSFX);
		}
		_onInteracted?.Invoke();
	}

	protected virtual void CantInteractFeedback()
	{
		Debug.Log("Can't interact with " + name);
		_onCantInteractFeedback?.Invoke();
	}

	public void TryToInteract()
	{
		// Can't interact for game's conditions or inputs disabled
		if (!CanInteractNoFeedback())
		{
			return;
		}

		if (CanInteract())
		{
			Interact();
		}
		else
		{
			CantInteractFeedback();
		}
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}

	public void AddItem(ItemData item)
	{
		_playerInventory.AddItem(item);
	}

	public void StartConversation(ConversationData conversationData)
	{
		ConversationManager.Instance.StartConversation(conversationData);
	}

	public void LoadLogicMap()
	{
		GameManager.Instance.LoadLogicMap();
	}
}
