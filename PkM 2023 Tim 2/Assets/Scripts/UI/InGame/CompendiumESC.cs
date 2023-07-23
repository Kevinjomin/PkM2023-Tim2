using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompendiumESC : MonoBehaviour
{
    //this script is just to close compendium when esc is pressed
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
