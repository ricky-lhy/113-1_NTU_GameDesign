using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEdibleItemData", menuName = "Data/Item Data/EdibleItem")]
public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
{
    [SerializeField]
    private List<ModifierData> modifierData = new List<ModifierData>();
    public string ActionName => "使用";
    public AudioClip actionSFX { get; private set; }
    public bool PerformAction(GameObject character)
    {
        foreach (ModifierData data in modifierData)
        {
            data.statsModifier.AffectCharacter(character, data.value);
        }
        return true;
    }
}

public interface IDestroyableItem
{

}
public interface IItemAction
{
    public string ActionName { get;}
    public AudioClip actionSFX { get;}
    bool PerformAction(GameObject character);
}
[Serializable]
public class ModifierData{
    public PlayerStatsModifierSO statsModifier;
    public float value;
}
