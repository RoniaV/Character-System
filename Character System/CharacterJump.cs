using UnityEngine;

/// <summary>Applies a jump impulse to the gravitable character when checkGrounded.</summary>
[RequireComponent(typeof(CharacterGravitable))]
public class CharacterJump : MonoBehaviour
{
    [SerializeField] float jumpHeight = 4f;

    private CharacterGravitable characterGravitable;

    protected bool doJump;


    protected virtual void Awake()
    {
        characterGravitable = GetComponent<CharacterGravitable>();
    }

    void FixedUpdate()
    {
        if (doJump)
        {
            doJump = false;

            if (characterGravitable.IsGrounded)
                characterGravitable.AddVerticalVelocity(Mathf.Sqrt(2f * jumpHeight * 9.8f));
        }
    }

    public void Jump()
    {
        doJump = true;
    }
}
