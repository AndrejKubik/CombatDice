using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FighterCombat fighter = other.GetComponent<FighterCombat>();

        if(fighter)
        {
            Debug.Log(gameObject.name + "Wall hit!");
            Destroy(other.gameObject);
        }
    }
}
