using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    private int counter;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //counter++;
            //ScreenCapture.CaptureScreenshot("Screenshot " + counter + ".png");

            Roulette.instance.StopTheSpin();
        }
    }
}
