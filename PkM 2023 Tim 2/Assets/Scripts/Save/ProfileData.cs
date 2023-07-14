using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfileData 
{
    public List<bool> items = new List<bool>();
    public List<LevelData> levelDatas = new List<LevelData>();

    public ProfileData()
    {
        items = new List<bool>();
        levelDatas = new List<LevelData>();
    }
}
