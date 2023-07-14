using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfileData 
{
    public string folderPath;
    public List<bool> items = new List<bool>();
    public List<LevelData> levelDatas = new List<LevelData>();

    public ProfileData(string folderPath)
    {
        this.folderPath = folderPath;
        items = new List<bool>();
        levelDatas = new List<LevelData>();
    }
}
