using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float Speed;

    public int NumberOfJumps;
    public float JumpSpeed;
    public float JumpUpTime;
    public float TypicalGravityScale;

    float remainingJumpUpTime;
    bool isJumping;

    Rigidbody2D playerRigidbody;

    public Transform GroundCheck;
    public float GroundCheckRadius;
    public LayerMask WhatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        remainingJumpUpTime = JumpUpTime;

        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        remainingJumpUpTime -= Time.deltaTime;

        var horizontalVelocity = Input.GetAxisRaw("Horizontal") * Speed;
        var verticalVelocity = playerRigidbody.velocity.y;

        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = JumpSpeed;
                remainingJumpUpTime = JumpUpTime;
                playerRigidbody.gravityScale = 0;

                isJumping = true;
            }
        }
        else
        {
            remainingJumpUpTime -= Time.deltaTime;

            if (remainingJumpUpTime <= 0)
            {
                if (isJumping)
                {
                    isJumping = false;
                }

                playerRigidbody.gravityScale = TypicalGravityScale;
            }
        }

        playerRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);
    }
}
