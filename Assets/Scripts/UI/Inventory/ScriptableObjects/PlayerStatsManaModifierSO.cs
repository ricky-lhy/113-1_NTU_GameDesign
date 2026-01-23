using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerStatsManaModifierData", menuName = "Data/PlayerStatsModifier/ManaModifier")]

public class PlayerStatsManaModifierSO : PlayerStatsModifierSO
{
    public AudioClip restoreSound;
    public override void AffectCharacter(GameObject character, float value)
    {
        Stats playerStats = character.GetComponent<Stats>();
        if (playerStats != null)
        {
            AudioSource audio = character.transform.parent.parent.GetComponent<AudioSource>();
            audio.clip = restoreSound;
            audio.loop = false;
            audio.PlayOneShot(restoreSound);
            playerStats.IncreaseMana(value);
        }
    }
}
