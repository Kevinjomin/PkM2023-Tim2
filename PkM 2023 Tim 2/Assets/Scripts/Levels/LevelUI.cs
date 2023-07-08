using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour // May make superclass for this and compendium
{
    private LevelManager manager;

    private void Start()
    {
        if (manager == null)
            manager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }
}
