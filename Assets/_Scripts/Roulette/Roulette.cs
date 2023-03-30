using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

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
    public bool Rolling;
    public bool Accelerating;
    [SerializeField] private float maxSpinFullSpeed;
    [SerializeField] private float minSpinFullSpeed;
    private float spinFullSpeed;

    [SerializeField] private float slowSpinMaxSpeed;
    [SerializeField] private float spinAccelerationStrength;
    [SerializeField, Min(0.05f)] private float minSlowStrength = 0.05f;
    [SerializeField, Min(0.05f)] private float maxSlowStrength = 3f;
    [SerializeField] private float rollOverallSpeed;
    [SerializeField] private float minSpinSpeed;

    public RouletteSelector Selector;
    private bool bonusChosen = true;
    private int coinCount = 0;

    [SerializeField] private Collider Activator;
    public TextMeshProUGUI CoinCounter;

    public int timer;
    public int MaxSpinTime;

    private void Start()
    {
        body = GetComponent<Rigidbody>();

        if (!Selector) Debug.LogError("Roulette selector not assigned!");
        if (!Activator) Debug.LogError("Activator not assigned!");

        //CoinCounter.text = coinCount.ToString();
    }

    private void Update()
    {
        if(Rolling) //if the roulette is still spinning
        {
            SlowTheSpin(); //constantly slow it down

            if (body.angularVelocity == Vector3.zero) //when the max slowing speed is reached
            {
                Rolling = false; //pretend that the rolling is stopped 
            }
        }
    }

    IEnumerator SpinTimer()
    {
        while (timer <= MaxSpinTime)
        {
            yield return new WaitForSeconds(1f);
            timer++;
        }

        timer = 0;
    }

    public void SpinNormally()
    {
        if (!Rolling && !Accelerating) //if the roulette is not currently spinning
        {
            //if (coinCount > 0) //if the player has a coin for a spin
            //{
            //    SpendACoin();
            //    float targetSpeed = RandomFullSpeed();
            //    StartCoroutine(SpinAcceleration(targetSpeed)); //spin the wheel of fortune
            //}
            //else
            //{
            //    Debug.Log("No coins left!");
            //    ReactivateSpinOption();
            //}

            //SpendACoin();

            //DisableSpinning();
            float targetSpeed = RandomFullSpeed();
            StartCoroutine(SpinAcceleration(targetSpeed)); //spin the wheel of fortune
        }
        else Debug.Log("Hold on! What's the rush?");
    }

    public void SpinSlowly()
    {
        Activator.enabled = false;
        Rolling = false; //pretend that the rolling is stopped 
        StartCoroutine(SpinAcceleration(slowSpinMaxSpeed));
    }

    IEnumerator SpinAcceleration(float maxSpeed)
    {
        body.angularDrag = minSlowStrength; //reset the spin slowing strength
        Vector3 targetVelocity = Vector3.up * maxSpeed;
        Accelerating = true;

        while (body.angularVelocity != targetVelocity)
        {
            body.angularVelocity = Vector3.MoveTowards(body.angularVelocity, targetVelocity, spinAccelerationStrength * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Rolling = true; //let the update method know to start slowing the spin
        Accelerating = false;
        bonusChosen = false;
        StartCoroutine(SpinTimer());
    }

    private float RandomFullSpeed()
    {
        return Random.Range(minSpinFullSpeed, maxSpinFullSpeed);
    }

    private void SlowTheSpin()
    {
        if (body.angularVelocity.y <= minSpinSpeed || timer >= MaxSpinTime) //if the spin gets real slow 
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
        //Selector.gameObject.SetActive(true);
        Activator.gameObject.SetActive(true);
        Activator.enabled = true;
    }

    public void DisableSpinning()
    {
        //Selector.gameObject.SetActive(false);
        Activator.gameObject.SetActive(false);
        //Activator.enabled = false;
    }

    public void GetACoin()
    {
        coinCount++;
        CoinCounter.text = coinCount.ToString();
    }

    public void SpendACoin()
    {
        coinCount--;
        CoinCounter.text = coinCount.ToString();
    }
}
