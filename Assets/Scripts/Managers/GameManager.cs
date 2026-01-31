using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private string _conversationTextCharSFX = null;

	private static GameManager _instance = null;
	public static GameManager Instance => _instance;

	private bool _controlsEnabled = true;
	public bool ControlsEnabled => _controlsEnabled;

	private GameObject _player = null;
	private Transform _respawnTransform = null;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
			Init();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Init()
	{
    }

	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
		_respawnTransform = GameObject.FindGameObjectWithTag("Respawn").transform;
	}

	public void SetControlsEnabled(bool enabled)
	{
		_controlsEnabled = enabled;
	}

	public void PlayConversationTextCharSFX(string text)
	{
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(_conversationTextCharSFX))
		{
			UtilSound.Instance.PlaySound(_conversationTextCharSFX);
		}
	}

	public void Respawn()
	{
		if (_respawnTransform != null)
		{
			_player.transform.position = _respawnTransform.position;
			Physics.SyncTransforms();
		}
	}
}
