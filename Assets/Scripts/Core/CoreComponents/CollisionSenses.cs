using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    private Movement Movement {get => movement ??= core.GetCoreComponent<Movement>();}
    private Movement movement;
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    } 
    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }
    public Transform CeilingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
        private set => ceilingCheck = value;
    }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDist { get => wallCheckDist; set => wallCheckDist = value; }
    public LayerMask GroundLayer { get => groundLayer; set => groundLayer = value; }
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDist;
    [SerializeField] private LayerMask groundLayer;

    public bool Grounded
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayer);
    }
    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, groundLayer);
    }
    public bool WallFront
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDist, groundLayer);
    }
    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDist, groundLayer);
    }
    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDist, groundLayer);
    }
    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDist, groundLayer);
    }
}
