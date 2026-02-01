using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
	[SerializeField]
	private float _interactionDistance = 1.0f;
	[SerializeField]
	private LayerMask _interactionMask = default;
	[SerializeField]
	private Image _pointer = default;
	[SerializeField]
	private Color _aimNormalColor = Color.white;
	[SerializeField]
	private Color _aimDetectingColor = Color.red;

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
		bool raycast = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _interactionDistance, _interactionMask, QueryTriggerInteraction.Collide);
		_pointer.color = raycast ? _aimDetectingColor : _aimNormalColor;

		if (!_input.interact)
		{
			return;
		}
		_input.interact = false;

		if (!GameManager.Instance.ControlsEnabled)
		{
			return;
		}

		TryToInteract(raycast, hit);
	}

	private void TryToInteract(bool valid, RaycastHit hit)
	{
		if (valid)
		{
			_pointer.color = _aimDetectingColor;
			if (hit.collider.TryGetComponent(out SmartObjectInteractable smartObject))
			{
				smartObject.TryToInteract();
			}
		}
		else
		{
			_pointer.color = _aimNormalColor;
		}
	}
}
