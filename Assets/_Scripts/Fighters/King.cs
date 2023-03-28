using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    public int MaxHealth;
    private int currentHealth;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = MaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        FighterCombat fighter = other.GetComponent<FighterCombat>();

        if(fighter)
        {
            Destroy(fighter.gameObject);
            GetDamaged(1);
        }
    }

    public void GetDamaged(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;

            if (currentHealth == 0)
            {
                Debug.Log("King is dead!");

            }
            else if (currentHealth > 0) animator.PlayInFixedTime("KingDamage", 0, 0f);
            //else Debug.Log("King hp: " + currentHealth);
        }
    }
}
