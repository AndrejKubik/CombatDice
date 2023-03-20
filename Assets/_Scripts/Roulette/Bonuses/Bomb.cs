using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, RouletteBonus
{
    [SerializeField] private GameObject bombCannon;

    public void Activate()
    {
        RouletteSelector.instance.gameObject.SetActive(false);
        bombCannon.SetActive(true);
    }
}
