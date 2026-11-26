using UnityEngine;

/// <summary>Applies gravity to a CharacterController and tracks whether it is checkGrounded.</summary>
[RequireComponent(typeof(CharacterController))]
public class CharacterGravitable : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    [SerializeField] LayerMask groundLayer;

    private CharacterController characterController;
    private CheckGrounded checkGrounded;

    private Vector3 characterVelocity;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        checkGrounded = new CheckGrounded(transform, groundLayer);
    }

    void FixedUpdate()
    {
        IsGrounded = checkGrounded.IsGrounded();

        // Cancel the accumulated fall velocity on landing
        if (IsGrounded && characterVelocity.y < 0f)
            characterVelocity = Vector3.zero;

        characterVelocity.y += -9.8f * Time.fixedDeltaTime;
        characterController.Move(characterVelocity * Time.fixedDeltaTime);
    }

    public void AddVerticalVelocity(float velocity)
    {
        characterVelocity.y += velocity;
    }

    public RaycastHit GetGroundHit()
    {
        return checkGrounded.GetLastHit();
    }

    public void SetGroundCheckOffset(float offset)
    {
        checkGrounded.OriginOffset = offset;
    }

    void OnDrawGizmos()
    {
        checkGrounded?.DrawSphereGizmos();
    }
}
