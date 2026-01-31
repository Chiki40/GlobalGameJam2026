using StarterAssets;
using System.Collections;
using UnityEngine;

public class MaskController : MonoBehaviour
{
    private bool _maskOn = false;
    private bool _maskAnimationInProgress = false;
    public bool MaskAnimationInProgress => _maskAnimationInProgress;

    private Animator _animator = null;
	private StarterAssetsInputs _input = null;

	public bool IsMaskOn => _maskOn;

	private void Awake()
	{
        _animator = GetComponent<Animator>();
		_input = GetComponent<StarterAssetsInputs>();
	}

	private void Update()
    {
        if (!_input.mask)
        {
            return;
        }
		_input.mask = false;

		if (!GameManager.Instance.ControlsEnabled || _maskAnimationInProgress)
        {
            return;
        }

        if (!_maskOn)
        {
            PutMask();
        }
        else
        {
            RemoveMask();
        }
    }

    private void PutMask()
    {
        IEnumerator PutMaskCoroutine()
        {
            _maskAnimationInProgress = true;
            _animator.SetTrigger("MaskOn");
            yield return null;
            yield return new WaitUntil(() => !_animator.IsInTransition(0));
			yield return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
			_maskAnimationInProgress = false;
			_maskOn = true;
			Debug.Log("Mask on");
		}
        StartCoroutine(PutMaskCoroutine());
    }

    private void RemoveMask()
    {
		IEnumerator RemoveMaskCoroutine()
		{
			_maskAnimationInProgress = true;
			_animator.SetTrigger("MaskOff");
			yield return null;
			yield return new WaitUntil(() => !_animator.IsInTransition(0));
			yield return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
			_maskAnimationInProgress = false;
			_maskOn = false;
            Debug.Log("Mask off");
		}
		StartCoroutine(RemoveMaskCoroutine());
    }
}
