using UnityEngine;

public class NPCBad : NPC
{
	[SerializeField]
	private float _purseTime = 5.0f;

	[SerializeField]
	private float _attackDistanceOffset = 0.05f;

	protected override void Awake()
	{
		base.Awake();
		_npcNavigation.SetPursueTime(_purseTime);
	}

	protected override void Update()
	{
		base.Update();

		if (_playerMaskController != null)
		{
			if (!_playerMaskController.IsMaskOn)
			{
				float distance = Vector3.Distance(_playerMaskController.transform.position, transform.position);
				if (!_npcNavigation.IsPursuing(_playerMaskController.transform) && !_npcNavigation.IsReturningToBase)
				{
					if (distance <= _detectionDistance)
					{
						_npcNavigation.PursueTarget(_playerMaskController.transform);
					}
				}
				if (distance <= _npcNavigation.Agent.stoppingDistance + _attackDistanceOffset)
				{
					_playerMaskController.GetComponent<DamageComponent>().TakeDamage();
				}
			}
			else if (_playerMaskController.IsMaskOn && _npcNavigation.IsPursuing(_playerMaskController.transform))
			{
				_npcNavigation.CancelPursue();
			}
		}
	}
}
