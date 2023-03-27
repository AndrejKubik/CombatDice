using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSelectionEffects : MonoBehaviour
{
    public GameObject WheelPart;

    public void FlashBonus()
    {
        WheelPart.SetActive(true);
        BonusHighlighter.instance.FlashChosenBonus();
    }
}
