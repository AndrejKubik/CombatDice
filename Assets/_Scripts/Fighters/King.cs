using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    public int MaxHealth;
    private int currentHealth;
    private Animator animator;
    private Animator animator2;
    public bool PlayerKing;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator2 = transform.GetChild(0).GetComponent<Animator>();
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
                animator2.PlayInFixedTime("KingDeath", 0, 0f);

                if(PlayerKing)
                {
                    Debug.Log("You Lose");
                }
                else
                {
                    Debug.Log("You Win");
                }
            }
            else if (currentHealth > 0)
            {
                animator.PlayInFixedTime("KingDamage", 0, 0f);
                Debug.Log("King hp: " + currentHealth);
            }
        }
    }
}
