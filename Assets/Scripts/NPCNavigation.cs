using UnityEngine;
using UnityEngine.AI;

public class NPCNavigation : MonoBehaviour
{
    [SerializeField]
    private Transform _basePoint = null;
	[SerializeField]
    private float _purseTime = 5.0f;

    private NavMeshAgent _agent = null;
    private Transform _target = null;
    private bool _pursuing = false;
    private bool _returningToBase = false;
    private float _pursuingTime = 0.0f;

    public bool IsPursuing(Transform target) => _pursuing && _target == target;
    public bool IsReturningToBase => _returningToBase;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
            if (_target == _basePoint && Vector3.Distance(_target.position, transform.position) <= _agent.stoppingDistance)
            {
                _returningToBase = false;
            }
        }

		if (_pursuing)
        {
            _pursuingTime += Time.deltaTime;
            if (_pursuingTime >= _purseTime)
            {
                CancelPursue();
            }
        }
	}

	public void GoToTarget(Transform target)
    {
        _target = target;
        _pursuing = false;
		_pursuingTime = 0.0f;
	}

    public void PursueTarget(Transform target)
    {
        _target = target;
        _pursuing = true;
		_pursuingTime = 0.0f;
    }

    public void CancelPursue()
    {
		GoToTarget(_basePoint);
        _returningToBase = true;
	}

	public void SetAgentEnabled(bool enabled)
	{
		_agent.isStopped = !enabled;
	}
}
