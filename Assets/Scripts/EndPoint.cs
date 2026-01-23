using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    [Header("Scenes to Load")]
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                FindObjectOfType<LoadingScreenManager>().LoadScene(sceneToLoad);
            }
        }
    }

}
