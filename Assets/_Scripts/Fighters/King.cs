using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    public int MaxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void GetDamaged(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
            if(currentHealth == 0) Debug.Log("King is dead!");
            //else Debug.Log("King hp: " + currentHealth);
        }
    }
}
