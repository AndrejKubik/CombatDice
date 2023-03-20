using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RouletteActivator : MonoBehaviour
{
    private BoxCollider activator;
    private Roulette roulette;

    private void Start()
    {
        activator = GetComponent<BoxCollider>();
        roulette = transform.parent.GetComponent<Roulette>();
    }

    private void OnMouseDown()
    {
        roulette.StartSpinning();
        activator.enabled = false;
    }
}
