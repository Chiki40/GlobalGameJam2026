using UnityEngine;

public abstract class NPC : MonoBehaviour
{
	protected NPCNavigation _npcNavigation = null;
	protected MaskController _playerMaskController = null;

	protected virtual void Awake()
	{
		_npcNavigation = GetComponent<NPCNavigation>();
	}

	protected virtual void Start()
	{
		_playerMaskController = GameObject.FindGameObjectWithTag("Player").GetComponent<MaskController>();
	}

	protected virtual void OnEnable()
	{

	}

	protected virtual void OnDisable()
	{

	}

	protected virtual void OnDestroy()
	{

	}

	protected virtual void Update()
	{

	}
}
