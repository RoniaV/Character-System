using UnityEngine;

/// <summary>Physic object that accelerates with gravity until it reaches the ground.</summary>
public class GravitableObject : PhysicObject
{
    [SerializeField] Vector3 gravity = new Vector3(0f, -9.8f, 0f);
    [SerializeField] LayerMask groundLayer;

    private CheckGrounded grounded;


    protected virtual void Awake()
    {
        grounded = new CheckGrounded(transform, groundLayer);
    }

    protected override void FixedUpdate()
    {
        if (!grounded.IsGrounded())
            acceleration += gravity;
        else
        {
            acceleration = Vector3.zero;
            velocity = Vector3.zero;
        }
        base.FixedUpdate();
    }
}
