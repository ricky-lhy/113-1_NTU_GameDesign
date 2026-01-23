using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [Header("Main Menu Objects")]
    [SerializeField] private GameObject loadingBar;
    [SerializeField] private Image loadingBarImage;
    [SerializeField] private GameObject[] menuItems;

    [Header("Scenes to Load")]
    [SerializeField] private string levelScene_Upper;
    [SerializeField] private string levelScene_Lower;

    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    private void Awake()
    {
        loadingBar.SetActive(false);
    }

    public void StartGame()
    {
        HideMenu();

        ShowLoadingBar();

        scenesToLoad.Add(SceneManager.LoadSceneAsync(levelScene_Upper));

        StartCoroutine(LoadingScreen());
    }

    public void StartTutorial()
    {
        HideMenu();

        ShowLoadingBar();

        // scenesToLoad.Add(SceneManager.LoadSceneAsync("Gameplay"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync(levelScene_Lower));

        StartCoroutine(LoadingScreen());
    }

    private void HideMenu()
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            menuItems[i].SetActive(false);
        }
    }

    private void ShowLoadingBar()
    {
        loadingBar.SetActive(true);
    }

    private IEnumerator LoadingScreen()
    {
        float loadingProgress = 0f;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                loadingProgress += scenesToLoad[i].progress;
                loadingBarImage.fillAmount = loadingProgress / scenesToLoad.Count;
                yield return null;
            }
        }
    }
}
