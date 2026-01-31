using Unity.VisualScripting;
using UnityEngine;

public abstract class SmartObjectInteractable : MonoBehaviour
{
    protected GameObject PlayerObject;
    private void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual bool CanInteract()
    {
        return GameManager.Instance.ControlsEnabled && !PlayerObject.GetComponent<MaskController>().MaskAnimationInProgress;
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
