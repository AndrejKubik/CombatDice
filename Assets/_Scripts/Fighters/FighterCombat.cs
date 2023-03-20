using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCombat : MonoBehaviour
{
    public bool PlayerTeam = true;
    public bool Targeted;

    private void OnTriggerEnter(Collider other)
    {
        FighterCombat fighter = other.GetComponent<FighterCombat>();
        
        if(fighter != null && !fighter.PlayerTeam)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
