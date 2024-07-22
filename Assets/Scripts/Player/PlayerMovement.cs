using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private float camAngle = Mathf.Deg2Rad * 45;
    public Transform CamTarget;

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
        var camAngle = CamTarget.eulerAngles.y * Mathf.Deg2Rad;
        Debug.Log($"Camera angle in radians: {camAngle}");
        Debug.Log($"Original camera angle: {CamTarget.rotation.y}");

        var sinCam = Mathf.Sin(camAngle);
        var cosCam = Mathf.Cos(camAngle);
        //Debug.Log(sinCam);
        //Debug.Log(cosCam);

        Vector2 hReq = Speed * Vector2.ClampMagnitude(
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1);

        var xVel = hReq.x * cosCam + hReq.y * sinCam;
        var yVel = -hReq.x * sinCam + hReq.y * cosCam;
        //Debug.Log($"X Velocity: {xVel}");
        //Debug.Log($"Y Velocity: {yVel}");

        Vector2 hVel = new Vector2(xVel, yVel);
        playerRigidbody.velocity = new Vector3(hVel.x, playerRigidbody.velocity.y, hVel.y);
        //Debug.Log($"Velocity: {playerRigidbody.velocity}");

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