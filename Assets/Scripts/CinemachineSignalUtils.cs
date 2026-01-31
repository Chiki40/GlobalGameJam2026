using UnityEngine;

public class CinemachineSignalUtils : MonoBehaviour
{
    public void StartGameplay()
    {
        GameManager.Instance.StartGameplay();
        gameObject.SetActive(false);
    }
}
