using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BonusHighlight : MonoBehaviour
{
    public MeshRenderer highlight;
    public float FlashDuration;
    public float TargetAlpha;

    public void FlashBonus()
    {
        highlight.material.DOFade(TargetAlpha, FlashDuration * 0.3f).
            OnComplete(() => highlight.material.DOFade(0f, FlashDuration * 0.7f * Time.unscaledDeltaTime));
    }
}
