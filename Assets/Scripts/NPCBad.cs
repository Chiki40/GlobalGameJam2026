using UnityEngine;

public class NPCBad : NPC
{
    [SerializeField] private float _purseTime = 5.0f;
    [SerializeField] private float _attackDistanceOffset = 0.05f;

    [Header("Audio")]
    [SerializeField] private AudioSource _pursueAudio;
    [SerializeField] private AudioSource _idleAudio;
    [SerializeField] private bool _stopAudioWhenReturningToBase = true;

    private bool _wasPursuingLastFrame = false;
    private bool _idleAudioEnabled = false;

    protected override void Awake()
    {
        base.Awake();
        _npcNavigation.SetPursueTime(_purseTime);

        if (_pursueAudio != null)
        {
            _pursueAudio.loop = true;
            // Por si se buggea, lo para al awake
            if (_pursueAudio.isPlaying) _pursueAudio.Stop();
            _pursueAudio.playOnAwake = false;
        }

        if (_idleAudio != null)
        {
            _idleAudio.loop = true;
            if (_idleAudio.isPlaying) _idleAudio.Stop();
            _idleAudio.playOnAwake = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (_playerMaskController == null) return;

        bool isMaskOff = !_playerMaskController.IsMaskOn;
        bool isPursuingPlayer = _npcNavigation.IsPursuing(_playerMaskController.transform);

        if (isMaskOff)
        {
            float distance = Vector3.Distance(_playerMaskController.transform.position, transform.position);

            if (!isPursuingPlayer && !_npcNavigation.IsReturningToBase)
            {
                if (distance <= _detectionDistance)
                {
                    _npcNavigation.PursueTarget(_playerMaskController.transform);
                }
            }

            if (distance <= _npcNavigation.Agent.stoppingDistance + _attackDistanceOffset)
            {
                _playerMaskController.GetComponent<DamageComponent>().TakeDamage();
            }
        }
        else
        {
            // Si se pone la máscara y lo estaba persiguiendo, cancela
            if (isPursuingPlayer)
                _npcNavigation.CancelPursue();
        }

        //Cuando esté persiguiendo
        bool isPursuingNow = _npcNavigation.IsPursuing(_playerMaskController.transform);

        // Se corta audo al volver a base
        if (_stopAudioWhenReturningToBase && _npcNavigation.IsReturningToBase)
            isPursuingNow = false;

        //Persigue en el anterior frame, así que chilla

        if (!_wasPursuingLastFrame && isPursuingNow)
        {
            StartPursueAudio();

            //No perseguia en el anterior frame, ásí que para
        }
        else if (_wasPursuingLastFrame && !isPursuingNow)
        {
            StopPursueAudio();
        }

        if (_idleAudio != null && _idleAudioEnabled)
        {
            if (!isPursuingNow)
            {
                if (!_idleAudio.isPlaying) _idleAudio.Play();
            }
            else
            {
                if (_idleAudio.isPlaying) _idleAudio.Stop();
            }
        }

        _wasPursuingLastFrame = isPursuingNow;
    }
    //Audio de idle on
    public void StartIdleAudio()
    {
        _idleAudioEnabled = true;
        if (_idleAudio == null) return;
        if (!_idleAudio.isPlaying) _idleAudio.Play();
    }
    //audio de idle off
    public void StopIdleAudio()
    {
        _idleAudioEnabled = false;
        if (_idleAudio == null) return;
        if (_idleAudio.isPlaying) _idleAudio.Stop();
    }

    private void StartPursueAudio()
    {
        Debug.Log("audio pursue started");
        if (_pursueAudio == null) return;

        if (_idleAudio != null && _idleAudio.isPlaying) _idleAudio.Stop();
        if (!_pursueAudio.isPlaying) _pursueAudio.Play();
    }

    private void StopPursueAudio()
    {
        Debug.Log("audio pursue stopped");
        if (_pursueAudio == null) return;

        if (_pursueAudio.isPlaying) _pursueAudio.Stop();
        if (_idleAudio != null && _idleAudioEnabled && !_idleAudio.isPlaying) _idleAudio.Play();
    }
}
