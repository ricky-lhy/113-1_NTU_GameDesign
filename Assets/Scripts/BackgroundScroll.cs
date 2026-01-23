using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private Rigidbody2D backgroundRB;
    void Start()
    {
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        backgroundRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        backgroundRB.velocity = playerRB.velocity;
    }
}
