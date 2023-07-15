using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [SerializeField] private ProfileData profileData;
    private string ownId;

    public void InitializeSaveSlot(ProfileData profileData)
    {
        this.profileData = profileData;
        profileData.folderPath = ownId;
    }
    public ProfileData GetProfile()
    {
        return profileData;
    }
    public string GetID()
    {
        return ownId;
    }
}
