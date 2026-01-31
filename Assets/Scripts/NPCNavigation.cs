using UnityEngine;
using UnityEngine.AI;

public class NPCNavigation : MonoBehaviour
{
    [SerializeField]
    private Transform[] _possibleBasePoints = null;

	private float _purseTime = 0.0f;
    private float _fleeingDetectionDistance = 0.0f;

	private NavMeshAgent _agent = null;
    private Transform _target = null;
	private int _basePointIndex = 0;
	private bool _pursuing = false;
	private float _currentPursuingTime = 0.0f;
	private bool _returningToBase = false;
	private bool _fleeing = false;
	private Transform _fleeingObject = null;

	public bool IsPursuing(Transform target) => _pursuing && _target == target;
    public bool IsReturningToBase => _returningToBase;
    public bool IsFleeing(Transform target) => _fleeing && _fleeingObject == target;

    public void SetPursueTime(float time)
    {
        _purseTime = time;
    }

	public void SetFleeDetectionDistance(float distance)
	{
        _fleeingDetectionDistance = distance;
	}

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
        if (_target != null)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);

            // Special case. Reaching base after pursuing should clear _returningToBase flag
            if (!_fleeing && !_pursuing &&
                _target == _possibleBasePoints[_basePointIndex] &&
                Vector3.Distance(_target.position, transform.position) <= _agent.stoppingDistance)
            {
                _returningToBase = false;
            }

            if (_fleeing && _fleeingObject != null &&
				Vector3.Distance(_target.position, transform.position) <= _agent.stoppingDistance &&
                Vector3.Distance(_fleeingObject.position, transform.position) <= _fleeingDetectionDistance)
			{
                ChangeFleeingPoint();
            }
        }
        else
        {
            _agent.isStopped = true;
		}

        if (_pursuing)
        {
            _currentPursuingTime += Time.deltaTime;
            if (_currentPursuingTime >= _purseTime)
            {
                CancelPursue();
            }
        }
	}

	public void GoToTarget(Transform target)
    {
        _target = target;
        _pursuing = false;
		_currentPursuingTime = 0.0f;
		_fleeing = false;
        _fleeingObject = null;
	}

    public void PursueTarget(Transform target)
    {
        _target = target;
        _pursuing = true;
		_currentPursuingTime = 0.0f;
		_fleeing = false;
		_fleeingObject = null;
	}

	public void CancelPursue()
    {
        _basePointIndex = Random.Range(0, _possibleBasePoints.Length);
        GoToTarget(_possibleBasePoints[_basePointIndex]);
		_returningToBase = true;
	}

	public void FleeTarget(Transform fleeingObject)
	{
        _pursuing = false;
		_currentPursuingTime = 0.0f;
		_fleeing = true;
		_fleeingObject = fleeingObject;
		ChangeFleeingPoint();
	}

    private void ChangeFleeingPoint()
    {
        int newBasePoint = 0;
        float greatestAngle = 0.0f;
        for (int i = 0; i < _possibleBasePoints.Length; ++i)
        {
            if (_possibleBasePoints[i] == _target)
            {
                continue;
            }

            float angle = Vector3.Angle(_fleeingObject.position - transform.position, _possibleBasePoints[i].position - transform.position);
			Debug.Log(angle);
			if (angle > greatestAngle)
            {
                newBasePoint = i;
                greatestAngle = angle;
            }
        }
        _target = _possibleBasePoints[newBasePoint];
	}

    public void CancelFlee()
    {
        _target = null;
		_fleeing = false;
        _fleeingObject = null;
	}
}
