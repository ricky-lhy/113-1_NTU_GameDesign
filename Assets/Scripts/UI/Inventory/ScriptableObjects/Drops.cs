using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDropsList")]
public class Drops : ScriptableObject
{
    public List<GameObject> dropList = new List<GameObject>();
}
