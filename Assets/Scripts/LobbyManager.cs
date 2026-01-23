using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LobbyManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> afterLevel1 = new List<GameObject>();
    [SerializeField] public List<GameObject> afterLevel2 = new List<GameObject>();
    [SerializeField] public List<GameObject> afterLevel3 = new List<GameObject>();
    [SerializeField] public List<GameObject> afterLevel4 = new List<GameObject>();
    private KillCounterBar killCounterBar;
    public GameObject globalLight;
    public GameObject spotLights;
    public GameObject dayBackground;
    public GameObject nightBackground;
    void Start()
    {
        killCounterBar = FindObjectOfType<KillCounterBar>();
        foreach (GameObject x in afterLevel1)
            x.SetActive(false);
        foreach (GameObject x in afterLevel2)
            x.SetActive(false);
        foreach (GameObject x in afterLevel3)
            x.SetActive(false);
        foreach (GameObject x in afterLevel4)
            x.SetActive(false);
        globalLight.GetComponent<Light2D>().intensity = 0.375f;
        spotLights.SetActive(true);
        dayBackground.SetActive(false);
        nightBackground.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (killCounterBar.Boss1Killed)
        {
            foreach (GameObject x in afterLevel1)
                x.SetActive(true);
        }
        if (killCounterBar.Boss2Killed)
        {
            foreach (GameObject x in afterLevel2)
                x.SetActive(true);
        }
        if (killCounterBar.Boss3Killed)
        {
            foreach (GameObject x in afterLevel3)
                x.SetActive(true);
        }
        if (killCounterBar.Boss4Killed)
        {
            foreach (GameObject x in afterLevel4)
                x.SetActive(true);
            spotLights.SetActive(false);
            nightBackground.SetActive(false);
            dayBackground.SetActive(true);
            globalLight.GetComponent<Light2D>().intensity = 1.2f;
        }
    }
}
