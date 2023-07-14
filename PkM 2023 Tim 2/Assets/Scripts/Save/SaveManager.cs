using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager : MonoBehaviour
{ 
    [SerializeField] private string fileName;

    [SerializeField] private ProfileData selectedProfile;
    [SerializeField] private List<ProfileData> availableProfiles; // Find the same id on save menu manager

    [SerializeField] private string saveFolderPath;
    [SerializeField] private string saveName;

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
    }
    private void Start() 
    {
        InitializeProfiles();
    }
    private void InitializeProfiles()
    {
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(saveFolderPath).EnumerateDirectories();

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
            else
            {
                Debug.LogError("Tried to load profile, but failed");
            }
        }
    }


    public void NewGame(string selectedFolder)
    {
        this.selectedProfile = new ProfileData(selectedFolder);
    }
    public void SaveGame()
    {
        string fullPath = Path.Combine(saveFolderPath, selectedProfile.folderPath, saveName); 

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        string dataToStore = JsonUtility.ToJson(selectedProfile, true);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
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


    public void DeleteData(string selectedFolder)
    {
        if (selectedFolder == null)
            return;

        string fullPath = Path.Combine(saveFolderPath, selectedFolder, saveName);
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
            string dataToLoad = ""; // Load the Json data

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
}
