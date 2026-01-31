using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private string _conversationTextCharSFX = null;

	private static GameManager _instance = null;
	public static GameManager Instance => _instance;

	private bool _controlsEnabled = true;
	public bool ControlsEnabled => _controlsEnabled;
	private int _numEnemiesPursuing = 0;
	public int NumEnemies => _numEnemiesPursuing;

	private GameObject _player = null;
	private Transform _respawnTransform = null;
	private PlayerInput _playerInput = null;
	private InputAction _cancelInput = null;
	private bool _paused = false;

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
		_playerInput = _player.GetComponent<PlayerInput>();
		_cancelInput = _playerInput.actions.FindAction("Cancel");
		_respawnTransform = GameObject.FindGameObjectWithTag("Respawn").transform;
	}

	private void Update()
	{
		if (_cancelInput.IsPressed())
		{
			if (_paused)
			{
				PauseGame(false);
			}
			else if (UIManager.Instance.IsFullScreenItemIDActive())
			{
				UIManager.Instance.HideFullScreenItemID();
			}
			else
			{
				PauseGame(true);
			}
		}
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

	public void LoadLogicMap()
	{
		SceneManager.LoadSceneAsync("LogicMap");
	}

	public void PauseGame(bool pause)
	{
		Time.timeScale = pause ? 0.0f : 1.0f;
		_paused = pause;
	}

	public void AddEnemyPursuing()
	{
		++_numEnemiesPursuing;
	}

	public void ChangeInputMapping(bool ui)
	{
		_playerInput.SwitchCurrentActionMap(ui ? "UI" : "Player");
	}

	public void RemoveEnemyPursuing()
	{
		--_numEnemiesPursuing;
	}
}
