using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierGate : MonoBehaviour
{
    [SerializeField] private int multiplierValue;

    private bool placed;

    [SerializeField] private float dragHeightOffset = 1f;
    private float originalHeight;
    private Vector3 startPosition;
    [SerializeField] private Transform placementIndicator;

    private void Start()
    {
        originalHeight = transform.position.y;
        startPosition = transform.position;
        placementIndicator.position = transform.position + Vector3.down * 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(placed)
        {
            FighterMovement fighter = other.GetComponent<FighterMovement>();

            if (fighter && !fighter.Multiplied)
            {
                fighter.Multiply(multiplierValue);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (placed)
        {
            FighterMovement fighter = other.GetComponent<FighterMovement>();

            if (fighter) fighter.UnblockMultiplication();
        }
    }

    private void OnMouseDown()
    {
        if (!placed) placementIndicator.gameObject.SetActive(true);
    }

    private void OnMouseDrag()
    {
        if (!placed)
        {
            transform.position = new Vector3(MouseWorldPosition().x, originalHeight + dragHeightOffset, MouseWorldPosition().z);
        }
    }

    private void OnMouseUp()
    {
        if(!placed)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    //transform.position = hit.point + Vector3.up;
                    transform.position = new Vector3(hit.point.x, hit.point.y + transform.localScale.y * 0.55f, hit.point.z);
                    Roulette.instance.ReactivateSpinOption();
                    placed = true;
                }
            }
            else transform.position = startPosition;

            placementIndicator.gameObject.SetActive(false);
        }
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 rawMousePosition = Input.mousePosition;
        rawMousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(rawMousePosition);
    }
}
