using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    public float RunSpeed = 10f;

    private void Update()
    {
        RunForward();
    }

    private void RunForward()
    {
        transform.Translate(Vector3.forward * RunSpeed * Time.deltaTime);
    }
}
