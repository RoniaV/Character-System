using UnityEngine;

/// <summary>Rotates the body (yaw) and head (pitch) from an aim input, clamping the head pitch.</summary>
public class CharacterAim : MonoBehaviour
{
    [SerializeField] protected Transform head;
    [SerializeField] protected Transform body;
    [Header("Rotation Settings")]
    [SerializeField] protected float maxHeadAngle = 90f;
    [SerializeField] float sensitivity = 1f;
    [SerializeField] bool flipY;

    protected Vector2 aim;


    protected virtual void Update()
    {
        body.Rotate(
            0f,
            aim.x * sensitivity * Time.deltaTime,
            0f, Space.Self);
        head.Rotate(
            aim.y * sensitivity * Time.deltaTime,
            0f,
            0f, Space.Self);

        ClampHeadRotation();
    }

    public void SetAimInput(Vector2 direction)
    {
        aim = direction;

        aim.y = flipY ? -aim.y : aim.y;
    }

    private void ClampHeadRotation()
    {
        Quaternion clampedHeadRotation = head.localRotation;
        clampedHeadRotation = Quaternion.Euler(
            ClampAngle(clampedHeadRotation.eulerAngles.x, -maxHeadAngle, maxHeadAngle),
            0f, 0f);

        head.localRotation = clampedHeadRotation;
    }

    // Clamps an euler angle (0-360) to the [min, max] range around 0
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180f)
            return Mathf.Max(angle, 360f + min);
        return Mathf.Min(angle, max);
    }
}
