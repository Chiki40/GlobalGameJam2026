using Unity.VisualScripting;
using UnityEngine;

public abstract class SmartObjectInteractable : MonoBehaviour
{
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
	}

	public void TryToInteract()
	{
		if (CanInteract())
		{
			Interact();
		}
	}
}
