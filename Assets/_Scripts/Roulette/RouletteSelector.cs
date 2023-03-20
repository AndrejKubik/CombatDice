using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteSelector : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    public void ActivateTargetedBonus()
    {
        if (Physics.Raycast(rayOrigin.position, Vector3.down, out RaycastHit hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
