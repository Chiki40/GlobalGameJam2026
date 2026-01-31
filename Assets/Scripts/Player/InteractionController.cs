using StarterAssets;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
	[SerializeField]
	private float _interactionDistance = 1.0f;
	[SerializeField]
	private LayerMask _interactionMask = default;

	private StarterAssetsInputs _input = null;

	private void Awake()
	{
		_input = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
	{
#if DEBUG
		Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * _interactionDistance, Color.red);
#endif

		if (!_input.interact)
		{
			return;
		}
		_input.interact = false;

		if (!GameManager.Instance.ControlsEnabled)
		{
			return;
		}

		TryToInteract();
	}

	private void TryToInteract()
	{
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _interactionDistance, _interactionMask, QueryTriggerInteraction.Collide))
		{
			if (hit.collider.TryGetComponent(out SmartObjectInteractable smartObject))
			{
				smartObject.TryToInteract();
			}
		}
	}
}
