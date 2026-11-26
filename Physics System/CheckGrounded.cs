using UnityEngine;

/// <summary>Sphere-cast ground check from a transform, with gizmo drawing.</summary>
public class CheckGrounded
{
    // Vertical offset of the cast origin along groundPoint.up
    public float OriginOffset { get; set; }

    private Transform groundPoint;
    private LayerMask groundMask;
    private float range = 0.1f;
    private float sphereCastRadius = 0.5f;

    private RaycastHit lastHit;


    public CheckGrounded(Transform groundPoint, LayerMask groundMask)
    {
        this.groundPoint = groundPoint;
        this.groundMask = groundMask;
    }

    public CheckGrounded(Transform groundPoint, LayerMask groundMask, float range)
    {
        this.groundPoint = groundPoint;
        this.groundMask = groundMask;
        this.range = range;
    }

    public bool IsGrounded()
    {
        RaycastHit hit;

        bool isGrounded = Physics.SphereCast(
            GetCastOrigin(),
            sphereCastRadius,
            -groundPoint.up * range,
            out hit,
            range,
            groundMask);

        lastHit = hit;

        return isGrounded;
    }

    public RaycastHit GetLastHit()
    {
        return lastHit;
    }

    public void DrawSphereGizmos()
    {
        Gizmos.DrawWireSphere(groundPoint.position + groundPoint.up * OriginOffset, range);

        Vector3 castOrigin = GetCastOrigin();
        RaycastHit hit;

        if (Physics.SphereCast(
            castOrigin,
            sphereCastRadius,
            -groundPoint.up * range,
            out hit,
            range,
            groundMask))
        {
            Gizmos.color = Color.green;
            Vector3 sphereCastMidpoint = castOrigin + (-groundPoint.up * hit.distance);
            Gizmos.DrawWireSphere(sphereCastMidpoint, sphereCastRadius);
            Gizmos.DrawSphere(hit.point, 0.1f);
            Debug.DrawLine(castOrigin, sphereCastMidpoint, Color.green);
        }
        else
        {
            Gizmos.color = Color.red;
            Vector3 sphereCastMidpoint = castOrigin + (-groundPoint.up * range);
            Gizmos.DrawWireSphere(sphereCastMidpoint, sphereCastRadius);
            Debug.DrawLine(castOrigin, sphereCastMidpoint, Color.red);
        }
    }

    private Vector3 GetCastOrigin()
    {
        return groundPoint.position + groundPoint.up * (OriginOffset + sphereCastRadius);
    }
}
