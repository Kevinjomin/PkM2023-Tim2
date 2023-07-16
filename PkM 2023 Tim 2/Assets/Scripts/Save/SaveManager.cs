using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{ 
    private string saveFolderPath;
    [SerializeField] private string saveName;

    [Header ("Testing Stuff")] 
    public ProfileData selectedProfile;
    private bool newProfile;
    // [SerializeField] private List<ProfileData> availableProfiles; // Find the same id on save menu manager

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

        saveFolderPath = Application.persistentDataPath;
        InitializeProfiles();
    }

    private void InitializeProfiles()
    {
        string fullPath = Path.Combine(saveFolderPath, "Profile Data", saveName);

        if (!File.Exists(fullPath))
        {
            Debug.LogWarning("Creating a new profile");
            ProfileData newProfile = new ProfileData("Profile Data");

            CreateData(newProfile);
            this.newProfile = true;
        }
        else
        {
            newProfile = false;
        }

        ChangeSelectedProfile(LoadData(fullPath));

        /*IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(saveFolderPath).EnumerateDirectories();
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
                selectedProfile = profileData;
            }
            else
            {
                ProfileData newProfile = new ProfileData("Profile Data");
                CreateData(newProfile);
            }
        }*/
    }
    /*public ProfileData CreateProfile(SaveSlot saveSlot)
    {
        ProfileData newProfile = new ProfileData(saveSlot.GetID());
        CreateData(newProfile);
        InitializeProfiles();

        return newProfile;
    }*/
    private void Start()
    {
        if (newProfile)
        {
            SaveGame();
            newProfile = false;
            InitializeProfiles();
        }

        LoadGame();
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
        List<bool> collected = CollectibleManager.instance.SendToProfile();
        List<LevelData> levelDatas = LevelManager.instance.SendToProfile();

        Debug.Log("Saving");

        selectedProfile.items = new List<bool>(collected);
        selectedProfile.levelDatas = new List<LevelData>(levelDatas);

        CreateData(selectedProfile);
    }
    public void LoadGame(/*ProfileData selectedProfile*/)
    {
        if (selectedProfile == null)
        {
            Debug.Log("No data was found, Please create a new game");
            return;
        }
        CollectibleManager.instance.LoadProfile(selectedProfile.items);
        LevelManager.instance.LoadProfile(selectedProfile.levelDatas);
    }


    public void ResetData(/*ProfileData selectedProfile*/)
    {
        if (selectedProfile.folderPath == null)
            return;

        Debug.Log("Resetting");

        string fullPath = Path.Combine(saveFolderPath, selectedProfile.folderPath, saveName);

        if (File.Exists(fullPath))
        {
            Directory.Delete(Path.GetDirectoryName(fullPath), true);
            ProfileData newProfile = new ProfileData("Profile Data");

            CreateData(newProfile);
            SaveGame();
            ChangeSelectedProfile(LoadData(fullPath));
        }
        else
        {
            Debug.LogWarning("Data doesn't exist");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public ProfileData LoadData(string selectedFolder)
    {
        string fullPath = Path.Combine(selectedFolder);
        Debug.Log(fullPath);

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

    public void ChangeSelectedProfile(ProfileData newProfile)
    {
        selectedProfile = newProfile;
    }

    /*public List<ProfileData> GetAllProfiles()
    {
        return availableProfiles;
    }*/
}
