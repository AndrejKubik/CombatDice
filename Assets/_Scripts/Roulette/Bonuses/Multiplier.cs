using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour, RouletteBonus
{
    [SerializeField] private BonusCannon bonusCannon;
    [SerializeField] private GameObject projectilePrefab;
    private enum MultiplierType { Multiplier2, Multiplier5 }
    [SerializeField] private MultiplierType multiplierType;

    public void Activate()
    {
        RouletteSelector.instance.gameObject.SetActive(false);

        #region << Multiplying Gate Projectile System >>
        //bonusCannon.gameObject.SetActive(true);
        //Time.timeScale = 0.2f;

        //if (multiplierType == MultiplierType.Multiplier2) bonusCannon.LoadCannon(projectilePrefab, bonusCannon.Multiplier2Target);
        //else if (multiplierType == MultiplierType.Multiplier5) bonusCannon.LoadCannon(projectilePrefab, bonusCannon.Multiplier5Target);
        #endregion

        if (multiplierType == MultiplierType.Multiplier2)
        {
            bonusCannon.FighterLauncher.ToggleSpawnPoints(3);
        }
        else if (multiplierType == MultiplierType.Multiplier5)
        {
            bonusCannon.FighterLauncher.ToggleSpawnPoints(5);
        }
    }
}
