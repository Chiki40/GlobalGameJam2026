using System;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    public int InitialHealth = 1;

    private int CurrentHealt = 0;

    private void Start()
    {
        CurrentHealt = InitialHealth;
    }

    public void TakeDamage()
    {
        --CurrentHealt;
        if (CurrentHealt <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        GameManager.Instance.Respawn();
        CurrentHealt = InitialHealth;
    }
}
