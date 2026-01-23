using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    private static PersistentObjects instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
