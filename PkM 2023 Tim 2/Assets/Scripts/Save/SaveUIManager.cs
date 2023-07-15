using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveUIManager : MonoBehaviour
{
    [SerializeField] private List<SaveSlot> saveSlots = new List<SaveSlot>();
    [SerializeField] private TextMeshProUGUI status;

    private void Start()
    {
        LoadData();
    }
    public void OnSaveClicked(SaveSlot saveSlot)
    {
        SaveManager.instance.ChangeSelectedProfileId(saveSlot.GetProfile());
    }
    public void OnDeleteClicked(SaveSlot saveSlot)
    {
        SaveManager.instance.DeleteData(saveSlot.GetProfile());
        SaveManager.instance.CreateProfile(saveSlot);

        LoadData();

        status.text = saveSlot.gameObject.name + " are cleared";
    }
    public void LoadData()
    {
        List<ProfileData> profilesGameData = SaveManager.instance.GetAllProfiles();
        for (int i = 0; i < saveSlots.Count; i++)
        {
            ProfileData profileData = profilesGameData[i];

            if(profileData == null)
            {
                saveSlots[i].InitializeSaveSlot(SaveManager.instance.CreateProfile(saveSlots[i]));
                Debug.Log("Creating a new Profile for " + saveSlots[i].gameObject.name);
            }
            else
                saveSlots[i].InitializeSaveSlot(profileData);
        }
    }
}
