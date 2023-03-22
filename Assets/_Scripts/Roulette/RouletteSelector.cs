using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteSelector : MonoBehaviour
{
    public static RouletteSelector instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Transform rayOrigin;
    public Transform MultiplierSpawnPoint;

    public void ActivateTargetedBonus()
    {
        if (Physics.Raycast(rayOrigin.position, Vector3.down, out RaycastHit hit))
        {
            Debug.Log(hit.collider.name);
            hit.collider.GetComponent<RouletteBonus>().Activate();
        }
    }
}
