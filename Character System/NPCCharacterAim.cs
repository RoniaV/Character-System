using UnityEngine;

/// <summary>CharacterAim variant that smoothly turns the body and head toward a world direction or point.</summary>
public class NPCCharacterAim : CharacterAim
{
    [Header("NPC Settings")]
    [SerializeField] float bodyTurnSpeed = 180f;
    [SerializeField] float headTurnSpeed = 180f;

    private Vector3 lookDirection;
    private bool hasTarget;


    protected override void Update()
    {
        if (!hasTarget)
            return;

        // Yaw the body on its flatDir plane, then pitch the head toward the direction
        Vector3 flatDir = Vector3.ProjectOnPlane(lookDirection, body.up);
        if (flatDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetYaw = Quaternion.LookRotation(flatDir, body.up);
            body.rotation = Quaternion.RotateTowards(
                body.rotation, targetYaw, bodyTurnSpeed * Time.deltaTime);
        }

        Vector3 localDir = body.InverseTransformDirection(lookDirection);
        float horizontal = new Vector2(localDir.x, localDir.z).magnitude;
        float pitch = -Mathf.Atan2(localDir.y, horizontal) * Mathf.Rad2Deg;
        pitch = Mathf.Clamp(pitch, -maxHeadAngle, maxHeadAngle);

        head.localRotation = Quaternion.RotateTowards(
            head.localRotation, Quaternion.Euler(pitch, 0f, 0f),
            headTurnSpeed * Time.deltaTime);
    }

    public void LookInDirection(Vector3 worldDirection)
    {
        if (worldDirection.sqrMagnitude < 0.0001f)
            return;
        lookDirection = worldDirection.normalized;
        hasTarget = true;
    }

    public void LookAtPoint(Vector3 worldPoint)
    {
        LookInDirection(worldPoint - head.position);
    }

    public void StopLooking()
    {
        hasTarget = false;
    }
}