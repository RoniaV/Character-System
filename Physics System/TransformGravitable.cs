using UnityEngine;

/// <summary>GravitableObject that applies its velocity to the transform position.</summary>
public class TransformGravitable : GravitableObject
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        transform.position += velocity * Time.fixedDeltaTime;
    }
}
