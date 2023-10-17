using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 480, false);


    }


}
