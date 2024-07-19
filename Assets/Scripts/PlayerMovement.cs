using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private float camAngle = Mathf.Deg2Rad * 45;
    public float Speed;
    public int NumberOfJumps;
    public float JumpUpTime;

    private float remainingJumpUpTime;
    private int remainingJumps;
    Rigidbody playerRigidbody;

    public Transform GroundCheck;
    public float GroundCheckRadius;
    public LayerMask WhatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        remainingJumpUpTime = JumpUpTime;
        remainingJumps = NumberOfJumps;
    }

    // Update is called once per frame
    void Update()
    {
        var sinCam = Mathf.Sin(camAngle);
        var cosCam = Mathf.Cos(camAngle);

        Vector2 hReq = Speed * Vector2.ClampMagnitude(
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1);
        Vector2 hVel = new Vector2(hReq.x * cosCam + hReq.y * sinCam, -hReq.x * sinCam + hReq.y * cosCam);
        playerRigidbody.velocity = new Vector3(hVel.x, playerRigidbody.velocity.y, hVel.y);

        if (Input.GetButton("Jump") && remainingJumpUpTime > 0)
        {
            playerRigidbody.velocity += Vector3.up;
            remainingJumpUpTime -= Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump") && remainingJumps > 1)
        {
            remainingJumps--;
            remainingJumpUpTime = JumpUpTime;
        }


        // If grounded reset remaining jumps and time
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            remainingJumpUpTime = JumpUpTime;
            remainingJumps = NumberOfJumps;
        }
    }

    bool IsGrounded()
    {
        return Physics.OverlapSphere(GroundCheck.position, GroundCheckRadius, WhatIsGround).Length > 0;
    }
}
