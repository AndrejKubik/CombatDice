using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, RouletteBonus
{
    [SerializeField] private BonusCannon bonusCannon;
    [SerializeField] private GameObject projectilePrefab;

    public void Activate()
    {
        RouletteSelector.instance.gameObject.SetActive(false);
        bonusCannon.LoadCannon(projectilePrefab, bonusCannon.BombTarget);
    }
}
