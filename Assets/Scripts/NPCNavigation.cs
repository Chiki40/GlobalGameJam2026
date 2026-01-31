using System.Collections;
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
    private float _pursuingTime = 0.0f;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
	}

	public void SetAgentEnabled(bool enabled)
	{
        _agent.isStopped = !enabled;
	}

	private void Update()
	{
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
        }

		if (_pursuing)
        {
            _pursuingTime += Time.deltaTime;
            if (_pursuingTime >= _purseTime)
            {
                GoToTarget(_basePoint);
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
}
