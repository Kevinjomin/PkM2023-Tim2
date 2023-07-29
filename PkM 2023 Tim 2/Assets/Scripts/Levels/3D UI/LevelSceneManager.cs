using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSceneManager : MonoBehaviour
{
    private LevelManager manager;
    [SerializeField] private List<GameObject> levels = new List<GameObject>();
    [SerializeField] private Image unlockedBar;

    private void Start()
    {
        if (manager == null)
            manager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

        InitializeLevels(manager.levels);
    }

    private void InitializeLevels(List<Levels> levels)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].GetLocked() == false)
                unlockedBar.fillAmount += 0.20f;

            this.levels[i].GetComponent<Level3DDisplayer>().Initialize(levels[i]);
        }
    }
}
