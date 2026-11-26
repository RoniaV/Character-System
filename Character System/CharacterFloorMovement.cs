using UnityEngine;

/// <summary>Floor movement that projects the movement direction onto walkable ground.</summary>
[RequireComponent(typeof(CharacterGravitable))]
public class CharacterFloorMovement : CharacterMovement
{
    [Tooltip("The maximum slope degree a character can walk")]
    [SerializeField] [Range(0f, 90f)] float maxSlope = 30f;

    protected CharacterGravitable characterGravitable;


    protected override void Awake()
    {
        base.Awake();
        characterGravitable = GetComponent<CharacterGravitable>();
    }

    protected override void FixedUpdate()
    {
        movementDirection = ProjectMovementOnGround(movementDirection);

        Debug.DrawLine(transform.position, transform.position + (movementDirection * 2f), Color.blue);

        base.FixedUpdate();
    }

    protected Vector3 ProjectMovementOnGround(Vector3 movement)
    {
        RaycastHit groundHit = characterGravitable.GetGroundHit();

        if (IsGroundWalkable(groundHit))
            return Vector3.ProjectOnPlane(movement, groundHit.normal).normalized * Mathf.Min(movement.magnitude, 1f);
        else
            return movement;
    }

    protected bool IsGroundWalkable(RaycastHit groundHit)
    {
        if (groundHit.collider != null)
        {
            Vector3 localNormal = groundHit.normal.normalized;
            // Walkable when the normal's Y is at least cos(maxSlope)
            bool walkable = localNormal.y >= Mathf.Cos(maxSlope * Mathf.Deg2Rad);
            Debug.DrawRay(groundHit.transform.position, localNormal, Color.red);
            return walkable;
        }
        else
            return false;
    }
}
