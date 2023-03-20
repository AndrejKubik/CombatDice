using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public Transform DiceParent;
    private List<Dice> dice = new List<Dice>();

    private void Start()
    {
        ActivateAllDice();
    }

    private void OnMouseDown() => RollDice();

    private void RollDice()
    {
        for (int i = 0; i < dice.Count; i++)
        {
            dice[i].Throw();
            dice[i].Roll();
        }
    }

    private void ActivateAllDice()
    {
        for (int i = 0; i < DiceParent.childCount; i++)
        {
            dice.Add(DiceParent.GetChild(i).GetComponent<Dice>());
        }
    }
}
