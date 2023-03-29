using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHighlighter : MonoBehaviour
{
    #region Singleton
    public static BonusHighlighter instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private Animator animator;
    private List<GameObject> bonuses = new List<GameObject>();

    private void Start()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i < transform.childCount; i++)
        {
            bonuses.Add(transform.GetChild(i).gameObject);
        }
    }

    public void FlashChosenBonus()
    {
        animator.PlayInFixedTime("Flash", 0, 0f);
    }

    public void FlashWholeWheel()
    {
        ShowAllHighlights();
        animator.PlayInFixedTime("Flash", 0, 0f);
    }

    public void HideAllBonuses() //animation event
    {
        for (int i = 0; i < bonuses.Count; i++)
        {
            bonuses[i].SetActive(false);
        }
    }

    public void ShowAllHighlights()
    {
        for (int i = 0; i < bonuses.Count; i++)
        {
            bonuses[i].SetActive(true);
        }
    }
}
