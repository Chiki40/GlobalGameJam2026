using Unity.Cinemachine;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public CinemachineVirtualCameraBase StartCinematic;
    private CinemachineVirtualCameraBase MainMenuVirtualCamera;

    private void Awake()
    {
        MainMenuVirtualCamera = GameObject.Instantiate(StartCinematic);
        MainMenuVirtualCamera.transform.position = StartCinematic.transform.position;
        MainMenuVirtualCamera.transform.rotation = StartCinematic.transform.rotation;
        MainMenuVirtualCamera.gameObject.SetActive(true);
        //MainMenuVirtualCamera.transform.parent = gameObject.transform;
        MainMenuVirtualCamera.Priority = 10000;
        Cursor.visible = true;
    }

    private void Start()
    {
        GameManager.Instance.ChangeInputMapping(true);
    }
    public void GoToCredits()
    {

    }

    public void ExitCredits()
    {

    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        MainMenuVirtualCamera.Priority = 0;
        GameManager.Instance.StartGame();
    }

    public void StartGameplay()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        MainMenuVirtualCamera.Priority = 0;
        GameManager.Instance.StartGameplay();
    }
}
