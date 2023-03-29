using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBoost : MonoBehaviour, RouletteBonus
{
    [SerializeField] private FighterLauncher playerCannon;
    [SerializeField, Range(0.1f, 1f)] private float boostedReloadDuration;
    [SerializeField] private float powerUpDuration;

    public void Activate()
    {
        playerCannon.BoostFireRate(boostedReloadDuration, powerUpDuration);
    }

    
}
