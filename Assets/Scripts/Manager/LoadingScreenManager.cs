using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    [Header("Main Menu Objects")]
    private GameObject loadingUI;
    private GameObject loadingBackground;
    private Image loadingBarMaster;

    private AsyncOperation sceneLoader;

    private void Start()
    {
        loadingUI = GameObject.Find("LoadingUI").gameObject;
        loadingBackground = GameObject.Find("LoadingBarBackground").gameObject;
        loadingBarMaster = GameObject.Find("LoadingBarMaster").GetComponent<Image>();

        loadingUI.SetActive(false);
        loadingBackground.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneToLoad) {
        loadingUI.SetActive(true);
        loadingBackground.gameObject.SetActive(true);
        sceneLoader = SceneManager.LoadSceneAsync(sceneToLoad);
        StartCoroutine(LoadingScreen());
    }

    private IEnumerator LoadingScreen()
    {
        float loadingProgress = 0f;
        while (!sceneLoader.isDone)
        {
            loadingProgress += sceneLoader.progress;
            loadingBarMaster.fillAmount = loadingProgress;
            yield return null;
        }
        loadingUI.SetActive(false);
    }
}
