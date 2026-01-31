using Unity.VisualScripting;
using UnityEngine;

public abstract class SmartObjectInteractable : MonoBehaviour
{
    protected GameObject PlayerObject;

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual bool CanInteract()
    {
        return !PlayerObject.GetComponent<MaskController>().MaskAnimationInProgress;
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
