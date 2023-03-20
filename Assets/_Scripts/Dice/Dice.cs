using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody body;
    public float minForce;
    public float maxForce;
    public float maxTorque;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Throw()
    {
        float randomForce = Random.Range(minForce, maxForce);
        body.AddForce(Vector3.up * randomForce);
    }

    public void Roll()
    {
        float randomX = Random.Range(-maxTorque, maxTorque);
        float randomY = Random.Range(-maxTorque, maxTorque);
        float randomZ = Random.Range(-maxTorque, maxTorque);
        Vector3 randomTorque = new Vector3(randomX, randomY, randomZ);

        body.AddTorque(randomTorque);
    }
}
