using UnityEngine;

public class CinemachineSignalUtils : MonoBehaviour
{
    public void StartGameplay()
    {
        NPCBad[] npcBads = FindObjectsByType<NPCBad>(FindObjectsSortMode.None);
        for (int i = 0; i < npcBads.Length; i++)
        {
            npcBads[i].StartIdleAudio();
        }

        GameManager.Instance.StartGameplay();
        gameObject.SetActive(false);
    }
}
