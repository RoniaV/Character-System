using System.Collections;
using UnityEngine;

/// <summary>Moves a CharacterController along a direction at a modifiable speed.</summary>
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float velocity = 2f;

    protected CharacterController charController;

    protected Vector3 movementDirection;
    protected float currentVelocity;

    private Coroutine velocityCoroutine;


    protected virtual void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    protected virtual void Start()
    {
        currentVelocity = velocity;
    }

    protected virtual void FixedUpdate()
    {
        charController.Move(movementDirection * currentVelocity * Time.fixedDeltaTime);
    }

    public void SetMovementDirection(Vector3 direction)
    {
        movementDirection = direction;
    }

    // A non-zero duration restores the default velocity after that many seconds
    public void ModifyVelocity(float newVelocity, float duration = 0f)
    {
        CancelVelocityReset();
        currentVelocity = newVelocity;

        if (duration != 0f)
            velocityCoroutine = StartCoroutine(ResetVelocityAfterDelay(duration));
    }

    public void ResetVelocity()
    {
        CancelVelocityReset();
        currentVelocity = velocity;
    }

    protected IEnumerator ResetVelocityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentVelocity = velocity;
        velocityCoroutine = null;
    }

    private void CancelVelocityReset()
    {
        if (velocityCoroutine == null)
            return;

        StopCoroutine(velocityCoroutine);
        velocityCoroutine = null;
    }
}
