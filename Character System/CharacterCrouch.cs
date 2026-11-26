using UnityEngine;

/// <summary>Toggles the crouch state, resizing the controller around its center and changing the movement speed.</summary>
[RequireComponent(typeof(CharacterFloorMovement), typeof(CharacterGravitable), typeof(CharacterController))]
public class CharacterCrouch : MonoBehaviour
{
    public bool Crouched { get; protected set; }

    [SerializeField] float crouchedVelocity = 1f;
    [SerializeField] LayerMask ceilingLayer = ~0;

    protected CharacterFloorMovement floorMovement;
    protected CharacterGravitable charGravitable;
    protected CharacterController charController;

    protected float originalHeight;


    protected virtual void Awake()
    {
        floorMovement = GetComponent<CharacterFloorMovement>();
        charGravitable = GetComponent<CharacterGravitable>();
        charController = GetComponent<CharacterController>();
    }

    protected virtual void Start()
    {
        originalHeight = charController.height;
    }

    public void ChangeCrouchState()
    {
        if (Crouched && !CanStandUp())
            return;

        Crouched = !Crouched;

        float height = Crouched ? originalHeight / 2f : originalHeight;
        charController.height = height;
        // The center stays put, so the collider bottom moves; the ground check follows it
        charGravitable.SetGroundCheckOffset((originalHeight - height) / 2f);

        if (Crouched)
            floorMovement.ModifyVelocity(crouchedVelocity);
        else
            floorMovement.ResetVelocity();
    }

    // Sweeps the capsule's extra top height upward to detect a ceiling
    private bool CanStandUp()
    {
        float radius = charController.radius * 0.95f;
        Vector3 topSphereCenter = transform.TransformPoint(
            charController.center + Vector3.up * (charController.height / 2f - charController.radius));
        float distance = (originalHeight - charController.height) / 2f;

        return !Physics.SphereCast(
            topSphereCenter, radius, transform.up, out _, distance, ceilingLayer, QueryTriggerInteraction.Ignore);
    }
}
