using UnityEngine;
using UnityEngine.Events;

public class AnimationSmartObject : SmartObjectInteractable
{
	[SerializeField]
	private string _animatiorStateName = null;

	protected Animator _animator = null;

	protected override void Awake()
	{
		base.Awake();
		_animator = GetComponent<Animator>();
	}

	protected override void Interact()
	{
		base.Interact();
		_animator.CrossFadeInFixedTime(_animatiorStateName, 0.0f);
	}
}
