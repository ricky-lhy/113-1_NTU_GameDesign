using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGateLocker : MonoBehaviour
{
    [SerializeField] private GameObject bossGate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bossGate.SetActive(true);
        }
    }
}
