using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MAKE SURE THIS SCRIPT ONLY EXISTS ON LOADING SCENE
public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
