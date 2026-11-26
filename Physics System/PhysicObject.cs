using UnityEngine;

/// <summary>Base physic object that integrates currentSpeed into velocity.</summary>
public class PhysicObject : MonoBehaviour
{
    [SerializeField] protected float mass = 1f;

    protected Vector3 acceleration;
    protected Vector3 velocity;


    protected virtual void FixedUpdate()
    {
        velocity += acceleration * Time.fixedDeltaTime;

        // Acceleration only lasts one step, so forces must be applied every FixedUpdate
        acceleration = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        acceleration += force / mass;
    }
}
