using UnityEngine;

public class NPCGood : NPC
{
	protected override void Awake()
	{
		base.Awake();
		_npcNavigation.SetFleeDetectionDistance(_detectionDistance);
	}

	protected override void Update()
	{
		base.Update();

		if (_playerMaskController != null)
		{
			if (_playerMaskController.IsMaskOn && !_npcNavigation.IsFleeing(_playerMaskController.transform))
			{
				float distance = Vector3.Distance(_playerMaskController.transform.position, transform.position);
				if (distance <= _detectionDistance)
				{
					_npcNavigation.FleeTarget(_playerMaskController.transform);
				}
			}
			else if (!_playerMaskController.IsMaskOn && _npcNavigation.IsFleeing(_playerMaskController.transform))
			{
				_npcNavigation.CancelFlee();
			}
		}
	}
}
