using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Mana")]
    public float arrowMana = 10f;
    [Header("Move State")]
    public float movementVelocity = 10f;
    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;
    [Header("Wall Jump State")]
    public float wallJumpVelocity = 15f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3.0f;
    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3.0f;
    [Header("Ledge Climb State")]
    public Vector2 startOffset, stopOffset;
    [Header("Dash State")]
    public float dashCooldown = 3f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.25f;
    public float dashVelocity = 25f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.3f;
    [Header("Crouch States")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = 0.8f;
    public float standColliderHeight = 1.6f;
}
