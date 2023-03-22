using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour, RouletteBonus
{
    [SerializeField] private GameObject multiplierPrefab;
    private Transform spawnPoint;

    public void Activate()
    {
        spawnPoint = RouletteSelector.instance.MultiplierSpawnPoint;
        RouletteSelector.instance.gameObject.SetActive(false);
        Instantiate(multiplierPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
