using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _defaultButtonGameObject = null;
	[SerializeField]
    private GameObject _goGoGameplayObject = null;

    public CinemachineVirtualCameraBase StartCinematic;
    private CinemachineVirtualCameraBase MainMenuVirtualCamera;

    private void OnEnable()
	{
        EventSystem.current.SetSelectedGameObject(_defaultButtonGameObject);
#if !DEBUG
        if (_goGoGameplayObject != null)
        {
            _goGoGameplayObject.SetActive(false);
        }
#endif

		StartCoroutine(InitCoroutine());

        MainMenuVirtualCamera = Instantiate(StartCinematic);
        MainMenuVirtualCamera.transform.position = StartCinematic.transform.position;
        MainMenuVirtualCamera.transform.rotation = StartCinematic.transform.rotation;
        MainMenuVirtualCamera.gameObject.SetActive(true);
        MainMenuVirtualCamera.Priority = 10000;
    }

	private IEnumerator InitCoroutine()
	{
		yield return null;
		GameManager.Instance.SetControlsEnabled(false);
		GameManager.Instance.ChangeInputMapping(ui: true);
        UtilSound.Instance.PlaySound("MainMenu");
    }

	public void StartGame()
    {
        UtilSound.Instance.StopSound("MainMenu");
        UtilSound.Instance.PlaySound("StartGame");
		GameManager.Instance.ChangeInputMapping(ui: false);
		gameObject.SetActive(false);
        MainMenuVirtualCamera.Priority = 0;
        GameManager.Instance.StartGame();
    }

    public void GoToCredits()
    {

    }

    public void ExitCredits()
    {

    }

    public void StartGameplay()
    {
        UtilSound.Instance.StopSound("MainMenu");
        GameManager.Instance.SetControlsEnabled(true);
        GameManager.Instance.ChangeInputMapping(ui: false);
        gameObject.SetActive(false);
        MainMenuVirtualCamera.Priority = 0;
        GameManager.Instance.StartGameplay();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}
