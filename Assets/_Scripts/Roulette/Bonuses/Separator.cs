using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separator : MonoBehaviour, RouletteBonus
{
    public void Activate()
    {
        Roulette.instance.SpinSlowly();
    }
}
