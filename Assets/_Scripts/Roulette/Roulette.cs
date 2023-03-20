using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Roulette : MonoBehaviour
{
    private Rigidbody body;
    private bool rolling;
    private bool accelerating;
    [SerializeField] private float maxSpinSpeed;
    [SerializeField] private float spinAccelerationStrength;
    [SerializeField, Min(0.05f)] private float minSlowStrength = 0.05f;
    [SerializeField, Min(0.05f)] private float maxSlowStrength = 3f;
    [SerializeField] private float rollOverallSpeed;
    [SerializeField] private float minSpinSpeed;

    [SerializeField] private RouletteSelector selector;
    private bool bonusChosen = true;

    [SerializeField] private Collider Activator;

    private void Start()
    {
        body = GetComponent<Rigidbody>();

        if (!selector) Debug.LogError("Roulette selector not assigned!");
        if (!Activator) Debug.LogError("Activator not assigned!");
    }

    private void Update()
    {
        if(rolling) //if the roulette is still spinning
        {
            SlowTheSpin(); //constantly slow it down

            if (body.angularVelocity == Vector3.zero) //when the max slowing speed is reached
            {
                Activator.enabled = true;
                rolling = false; //pretend that the rolling is stopped 
            }
        }
    }

    public void StartSpinning()
    {
        if (!rolling && !accelerating) //if the roulette is not currently spinning
        {
            body.angularDrag = minSlowStrength; //reset the spin slowing strength
            StartCoroutine(SpinAcceleration());
            bonusChosen = false;
        }
    }

    IEnumerator SpinAcceleration()
    {
        Vector3 targetVelocity = Vector3.up * maxSpinSpeed;
        accelerating = true;

        while (body.angularVelocity != targetVelocity)
        {
            body.angularVelocity = Vector3.MoveTowards(body.angularVelocity, targetVelocity, spinAccelerationStrength * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        rolling = true; //let the update method know to start slowing the spin
        accelerating = false;
    }

    private void SlowTheSpin()
    {
        if (body.angularVelocity.y <= minSpinSpeed) //if the spin gets real slow 
        {
            body.angularVelocity = Vector3.zero; //stop the spin

            if(!bonusChosen)
            {
                selector.ActivateTargetedBonus(); //activate the bonus below the selector
                bonusChosen = true;
            }
        }
        else body.angularDrag = Mathf.MoveTowards(body.angularDrag, maxSlowStrength, rollOverallSpeed * Time.deltaTime);
        //if the roulette is still spinning nice and fast, increase the slowing strength gradually  
    }
}
