using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public CinemachineVirtualCameraBase StartCinematic;
    private CinemachineVirtualCameraBase MainMenuVirtualCamera;

    private void OnEnable()
	{
		StartCoroutine(InitCoroutine());

        MainMenuVirtualCamera = GameObject.Instantiate(StartCinematic);
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
    }

	public void StartGame()
    {
        UtilSound.Instance.PlaySound("StartGame");
		GameManager.Instance.SetControlsEnabled(true);
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
