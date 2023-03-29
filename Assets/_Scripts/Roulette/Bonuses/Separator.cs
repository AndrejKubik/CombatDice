using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separator : MonoBehaviour, RouletteBonus
{
    public void Activate()
    {
        CameraControl.instance.ToggleSpinCamera(true);
        Roulette.instance.SpinSlowly();
    }
}
