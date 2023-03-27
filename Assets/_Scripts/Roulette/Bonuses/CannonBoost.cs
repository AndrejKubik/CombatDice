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
        StartCoroutine(ReloadBoost());
        Roulette.instance.ReactivateSpinOption();
    }

    IEnumerator ReloadBoost()
    {
        float baseReload = playerCannon.ReloadTime;

        playerCannon.ReloadBoostActive = true;
        playerCannon.ReloadTime *= boostedReloadDuration;

        yield return new WaitForSeconds(powerUpDuration);

        playerCannon.ReloadBoostActive = false;
        playerCannon.ReloadTime = baseReload;
    }
}
