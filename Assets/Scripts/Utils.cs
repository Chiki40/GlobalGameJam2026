using UnityEngine;

public class Utils : MonoBehaviour
{
    public void EndGame()
    {
        GameManager.Instance.StartEndCinematic();
    }
}
