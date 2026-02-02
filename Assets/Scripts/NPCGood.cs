using UnityEngine;

public class NPCGood : NPC
{
    [Header("Audio")]
    [SerializeField] private AudioSource _fleeAudio;

    private bool _wasFleeingLastFrame = false;

    protected override void Awake()
    {
        base.Awake();
        _npcNavigation.SetFleeDetectionDistance(_detectionDistance);

        if (_fleeAudio != null)
        {
            _fleeAudio.loop = true;
            if (_fleeAudio.isPlaying) _fleeAudio.Stop();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (_playerMaskController == null) return;

        bool isMaskOn = _playerMaskController.IsMaskOn;
        bool isFleeingPlayer = _npcNavigation.IsFleeing(_playerMaskController.transform);
        
        //Cuando el player tiene la máscara y se asusta
        if (isMaskOn && !isFleeingPlayer)
        {
            float distance = Vector3.Distance(_playerMaskController.transform.position, transform.position);
            if (distance <= _detectionDistance)
            {
                _npcNavigation.FleeTarget(_playerMaskController.transform);
            }
        }
        //Cuando se pone el player la máscara
        else if (!isMaskOn && isFleeingPlayer)
        {
            _npcNavigation.CancelFlee();
        }
        //Huye en el anterior frame, así que chilla
        bool isFleeingNow = _npcNavigation.IsFleeing(_playerMaskController.transform);

        if (!_wasFleeingLastFrame && isFleeingNow)
        {
            StartFleeAudio();
        }
        //No Huye en el anterior frame, ásí que para
        else if (_wasFleeingLastFrame && !isFleeingNow)
        {
            StopFleeAudio();
        }

        _wasFleeingLastFrame = isFleeingNow;
    }

    private void StartFleeAudio()
    {
        Debug.Log("audio flee started");
        if (_fleeAudio == null) return;
        if (!_fleeAudio.isPlaying) _fleeAudio.Play();
    }

    private void StopFleeAudio()
    {
        Debug.Log("audio flee stopped");
        if (_fleeAudio == null) return;
        if (_fleeAudio.isPlaying) _fleeAudio.Stop();
    }
}