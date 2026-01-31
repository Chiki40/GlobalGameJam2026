using System.Collections;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	private void OnEnable()
	{
		StartCoroutine(InitCoroutine());
	}

	private IEnumerator InitCoroutine()
	{
		yield return null;
		GameManager.Instance.SetControlsEnabled(false);
		GameManager.Instance.ChangeInputMapping(ui: true);
	}

	public void StartGame()
    {
		GameManager.Instance.SetControlsEnabled(true);
		GameManager.Instance.ChangeInputMapping(ui: false);
		gameObject.SetActive(false);
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
