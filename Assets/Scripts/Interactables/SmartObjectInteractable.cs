using UnityEngine;
using UnityEngine.Events;

public abstract class SmartObjectInteractable : MonoBehaviour
{
	[SerializeField]
	private string _interactSFX = null;
	[SerializeField]
	private UnityEvent _onInteracted = null;

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

    protected virtual bool CanInteract()
    {
        return !_maskController.MaskAnimationInProgress;
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

	public void TryToInteract()
	{
		if (CanInteract())
		{
			Interact();
		}
	}
}
