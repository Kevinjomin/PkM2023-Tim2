using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    // List all scenes here
    public enum scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    public static scene targetScene;

    public static void Load(scene targetScene)
    {
        // store the target scene
        Loader.targetScene = targetScene;

        // go to loading scene
        SceneManager.LoadScene(scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        // go to target scene after loading
        SceneManager.LoadScene(targetScene.ToString());
    }
}
