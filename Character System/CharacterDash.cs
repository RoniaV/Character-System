using UnityEngine;

/// <summary>Moves the character along a direction with a decelerating dash.</summary>
[RequireComponent(typeof(CharacterController))]
public class CharacterDash : MonoBehaviour
{
    public bool IsDashing { get; private set; }

    [SerializeField] float braking = -3f;
    [SerializeField] float dashLength = 5f;
    [SerializeField] float dashMinimumSpeed = 2f;

    private CharacterController characterController;

    private float currentSpeed;
    private Vector3 dashDirection;


    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    protected virtual void FixedUpdate()
    {
        if (IsDashing)
        {
            currentSpeed += braking * Time.fixedDeltaTime;

            if (currentSpeed <= dashMinimumSpeed)
                StopDash();

            characterController.Move(dashDirection * currentSpeed * Time.fixedDeltaTime);
        }
    }

    public void Dash(Vector3 direction)
    {
        if (!IsDashing)
        {
            // Initial speed that brakes down to dashMinimumSpeed after covering dashLength
            currentSpeed += Mathf.Sqrt(2f * dashLength * -braking + dashMinimumSpeed * dashMinimumSpeed);
            dashDirection = direction;

            IsDashing = true;
        }
    }

    public void StopDash()
    {
        currentSpeed = 0f;
        IsDashing = false;
    }
}
