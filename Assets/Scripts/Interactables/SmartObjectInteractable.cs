using UnityEngine;
using UnityEngine.Events;

public abstract class SmartObjectInteractable : MonoBehaviour
{
	[SerializeField]
	private string _interactSFX = null;
	[SerializeField]
	private UnityEvent _onInteracted = null;
	[SerializeField]
	private UnityEvent _onCantInteractFeedback = null;

	protected GameObject _playerObject;
	protected MaskController _maskController = null;

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("Player");
		_maskController = _playerObject.GetComponent<MaskController>();

	}

	protected virtual bool CanInteractNoFeedback()
	{
		return GameManager.Instance.ControlsEnabled && !_maskController.MaskAnimationInProgress;
	}

	protected virtual bool CanInteract()
    {
		return true;
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

	public void StartConversation(ConversationData conversationData)
	{
		ConversationManager.Instance.StartConversation(conversationData);
	}
}
