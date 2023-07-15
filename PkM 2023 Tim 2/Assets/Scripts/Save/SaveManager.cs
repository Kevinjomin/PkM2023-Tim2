using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager : MonoBehaviour
{ 
    [SerializeField] private string fileName;

    [SerializeField] private string saveFolderPath;
    [SerializeField] private string saveName;

    [Header ("Testing Stuff")] 
    [SerializeField] private ProfileData selectedProfile;
    [SerializeField] private List<ProfileData> availableProfiles; // Find the same id on save menu manager

    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }

        InitializeProfiles();
    }

    private void InitializeProfiles()
    {
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(saveFolderPath).EnumerateDirectories();
        availableProfiles.Clear();

        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string selectedFolder = dirInfo.Name;
            string fullPath = Path.Combine(saveFolderPath, selectedFolder, saveName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Not the corect save path");
                continue;
            }

            ProfileData profileData = LoadData(fullPath);
            if (profileData != null)
            {
                availableProfiles.Add(profileData);
            }
        }
    }
    public ProfileData CreateProfile(SaveSlot saveSlot)
    {
        ProfileData newProfile = new ProfileData(saveSlot.GetID());
        CreateData(newProfile);
        InitializeProfiles();

        return newProfile;
    }
    public void CreateData(ProfileData profileData)
    {
        string fullPath = Path.Combine(saveFolderPath, profileData.folderPath, saveName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        string dataToStore = JsonUtility.ToJson(profileData, true);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
    }


    public void SaveGame()
    {
        CreateData(selectedProfile);
    }
    public void LoadGame(ProfileData selectedProfile)
    {
        if (selectedProfile == null)
        {
            Debug.Log("No data was found, Please create a new game");
            return;
        }

        this.selectedProfile = LoadData(selectedProfile.folderPath);
    }


    public void DeleteData(ProfileData selectedProfile)
    {
        if (selectedProfile.folderPath == null)
            return;

        string fullPath = Path.Combine(saveFolderPath, selectedProfile.folderPath, saveName);
        try
        {
            if (File.Exists(fullPath))
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else
            {
                Debug.LogWarning("Data doesn't exist");
            }
        }
        catch
        {
            Debug.LogError("Failed to delete the save file");
        }
    }
    public ProfileData LoadData(string selectedFolder)
    {
        string fullPath = Path.Combine(saveFolderPath, selectedFolder, saveName);
        ProfileData loadedData = null;

        if (File.Exists(fullPath))
        {
            string dataToLoad = ""; 

            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }
            loadedData = JsonUtility.FromJson<ProfileData>(dataToLoad);
        }
        else
        {
            Debug.LogError("There's no data in this path");
        }

        return loadedData;
    }


    public void ChangeSelectedProfileId(ProfileData newProfile)
    {
        selectedProfile = newProfile;
        LoadGame(selectedProfile);
    }
    public List<ProfileData> GetAllProfiles()
    {
        return availableProfiles;
    }
}
