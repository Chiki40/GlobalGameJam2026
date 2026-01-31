using UnityEngine;

public class NPCBad : NPC
{
	[SerializeField]
	private float _purseTime = 5.0f;

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
			if (!_playerMaskController.IsMaskOn && !_npcNavigation.IsPursuing(_playerMaskController.transform) && !_npcNavigation.IsReturningToBase)
			{
				float distance = Vector3.Distance(_playerMaskController.transform.position, transform.position);
				if (distance <= _detectionDistance)
				{
					_npcNavigation.PursueTarget(_playerMaskController.transform);
				}
			}
			else if (_playerMaskController.IsMaskOn && _npcNavigation.IsPursuing(_playerMaskController.transform))
			{
				_npcNavigation.CancelPursue();
			}
		}
	}
}
