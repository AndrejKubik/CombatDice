using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FighterMovement : MonoBehaviour
{
    public bool InEnemyBase;
    public Transform AttackTarget;
    public float AttackMoveSpeed;

    private float currentSpeed;
    public float RunSpeed = 10f;
    public float LaunchSpeed = 13f;
    public float SlowDownStrength = 1f;

    public bool Multiplied;
    [SerializeField] private GameObject multiplyPrefab;
    [SerializeField, Range(0f, 1f)] private float multiplyDistance;
    [SerializeField, Range(0f, 1f)] private float multiplyRadius;

    private void Start()
    {
        currentSpeed = LaunchSpeed;
        StartCoroutine(SpeedReducing());
    }


    private void Update()
    {
        if (!InEnemyBase) RunForward();
        else LockOnTarget();
    }

    IEnumerator SpeedReducing()
    {
        while(currentSpeed > RunSpeed)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, RunSpeed, SlowDownStrength * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void RunForward()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void LockOnTarget()
    {
        transform.LookAt(AttackTarget, Vector3.up);
    }

    public void AttackTheTarget()
    {
        StartCoroutine(MoveTowardsTarget(AttackTarget.transform, AttackMoveSpeed));
    }

    public void Multiply(int multiplierNumber)
    {
        if (!Multiplied)
        {
            for (int i = 0; i < multiplierNumber; i++)
            {
                float xPosition = multiplyDistance * Mathf.Sqrt(i) * Mathf.Cos(i * multiplyRadius);
                float yPosition = multiplyDistance * Mathf.Sqrt(i) * Mathf.Sin(i * multiplyRadius);

                Vector3 newPosition = transform.position + new Vector3(xPosition, 0f, yPosition);

                GameObject clone = Instantiate(multiplyPrefab, transform.position, transform.rotation);
                clone.transform.DOMove(newPosition, 0.15f).SetEase(Ease.OutBack).OnComplete(() => transform.DOKill());
                clone.GetComponent<FighterMovement>().Multiplied = true;
            }
            
            Multiplied = true;
        }
    }

    public void UnblockMultiplication()
    {
        StartCoroutine(MultiplierCooldown());
    }

    IEnumerator MultiplierCooldown()
    {
        yield return new WaitForSeconds(1f);
        Multiplied = false;
    }

    IEnumerator MoveTowardsTarget(Transform target, float speed)
    {
        while(transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
