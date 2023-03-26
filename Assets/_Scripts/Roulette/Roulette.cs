using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class Roulette : MonoBehaviour
{
    #region Singleton
    public static Roulette instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private Rigidbody body;
    private bool rolling;
    private bool accelerating;
    [SerializeField] private float maxSpinSpeed;
    [SerializeField] private float slowSpinMaxSpeed;
    [SerializeField] private float spinAccelerationStrength;
    [SerializeField, Min(0.05f)] private float minSlowStrength = 0.05f;
    [SerializeField, Min(0.05f)] private float maxSlowStrength = 3f;
    [SerializeField] private float rollOverallSpeed;
    [SerializeField] private float minSpinSpeed;

    public RouletteSelector Selector;
    private bool bonusChosen = true;
    private int coinCount;

    [SerializeField] private Collider Activator;

    private void Start()
    {
        body = GetComponent<Rigidbody>();

        if (!Selector) Debug.LogError("Roulette selector not assigned!");
        if (!Activator) Debug.LogError("Activator not assigned!");
    }

    private void Update()
    {
        if(rolling) //if the roulette is still spinning
        {
            SlowTheSpin(); //constantly slow it down

            if (body.angularVelocity == Vector3.zero) //when the max slowing speed is reached
            {
                rolling = false; //pretend that the rolling is stopped 
            }
        }
    }

    public void SpinNormally()
    {
        if (!rolling && !accelerating) //if the roulette is not currently spinning
        {
            if (coinCount > 0) //if the player has a coin for a spin
            {
                coinCount--; //spend a coin
                StartCoroutine(SpinAcceleration(maxSpinSpeed)); //spin the wheel of fortune
            }
            else
            {
                Debug.Log("No coins left!");
                ReactivateSpinOption();
            }
        }
        else Debug.Log("Hold on! What's the rush?");
    }

    public void SpinSlowly()
    {
        Activator.enabled = false;
        rolling = false; //pretend that the rolling is stopped 
        StartCoroutine(SpinAcceleration(slowSpinMaxSpeed));
    }

    IEnumerator SpinAcceleration(float maxSpeed)
    {
        body.angularDrag = minSlowStrength; //reset the spin slowing strength
        Vector3 targetVelocity = Vector3.up * maxSpeed;
        accelerating = true;

        while (body.angularVelocity != targetVelocity)
        {
            body.angularVelocity = Vector3.MoveTowards(body.angularVelocity, targetVelocity, spinAccelerationStrength * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        rolling = true; //let the update method know to start slowing the spin
        accelerating = false;
        bonusChosen = false;
    }

    private void SlowTheSpin()
    {
        if (body.angularVelocity.y <= minSpinSpeed) //if the spin gets real slow 
        {
            body.angularVelocity = Vector3.zero; //stop the spin

            if(!bonusChosen)
            {
                Selector.ActivateTargetedBonus(); //activate the bonus below the selector
                bonusChosen = true;
            }
        }
        else body.angularDrag = Mathf.MoveTowards(body.angularDrag, maxSlowStrength, rollOverallSpeed * Time.deltaTime);
        //if the roulette is still spinning nice and fast, increase the slowing strength gradually  
    }

    public void ReactivateSpinOption()
    {
        Selector.gameObject.SetActive(true);
        Activator.enabled = true;
    }

    public void GetACoin() => coinCount++;
}
