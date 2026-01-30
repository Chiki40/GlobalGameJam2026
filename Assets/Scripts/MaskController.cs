using System.Collections;
using UnityEngine;

public class MaskController : MonoBehaviour
{
    [SerializeField]
    private float _maskProcessDuration = 1.0f;

    private bool _maskOn = false;
    private bool _maskAnimationInProgress = false;

    private Animator _animator = null;

    public bool IsMaskOn => _maskOn;

	private void Awake()
	{
        _animator = GetComponent<Animator>();
	}

	public void ToggleMask()
    {
        if (_maskAnimationInProgress)
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
            yield return _animator.GetCurrentAnimatorClipInfo(0).Length;
			_maskAnimationInProgress = false;
		}
        StartCoroutine(PutMaskCoroutine());
        _maskOn = true;
    }

    private void RemoveMask()
    {
		IEnumerator RemoveMaskCoroutine()
		{
			_maskAnimationInProgress = true;
			_animator.SetTrigger("MaskOff");
			yield return null;
			yield return _animator.GetCurrentAnimatorClipInfo(0).Length;
			_maskAnimationInProgress = false;
		}
		StartCoroutine(RemoveMaskCoroutine());
		_maskOn = false;
    }
}
