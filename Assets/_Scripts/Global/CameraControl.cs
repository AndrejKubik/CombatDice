using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    #region Singleton
    public static CameraControl instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public CinemachineVirtualCamera SpinCamera;
    public GameObject CameraEffect;

    public void ToggleSpinCamera(bool state)
    {
        if (state == true) SpinCamera.Priority = 2;
        else if (state == false) SpinCamera.Priority = 0;

        Debug.Log("switch");
    }

    public void ToggleSlowMotion(bool state)
    {
        CameraEffect.SetActive(state);
    }
}
