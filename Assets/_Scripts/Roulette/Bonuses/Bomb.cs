using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, RouletteBonus
{
    [SerializeField] private BonusCannon bonusCannon;
    [SerializeField] private GameObject projectilePrefab;
    public bool SuperBomb;

    public void Activate()
    {
        CameraControl.instance.ToggleSpinCamera(false);
        Time.timeScale = 0.2f;
        //RouletteSelector.instance.gameObject.SetActive(false);
        bonusCannon.gameObject.SetActive(true);
        
        if(SuperBomb) bonusCannon.LoadCannon(projectilePrefab, bonusCannon.SuperBombTarget);
        else bonusCannon.LoadCannon(projectilePrefab, bonusCannon.BombTarget);
    }
}
