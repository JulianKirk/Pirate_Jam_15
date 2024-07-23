using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithoutForceReceiever : AlchemyReceiver
{
    public Vector3 Velocity;
    public float Duration;
    public bool IsGravityEnabled = true;

    float remainingDuration;
    bool isActive = false;

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void Activate(Collider other)
    {
        rigidBody.useGravity = IsGravityEnabled;

        remainingDuration = Duration;
        rigidBody.velocity += Velocity;

        isActive = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive)
        {
            remainingDuration -= Time.deltaTime;

            if (remainingDuration <= 0)
            {
                rigidBody.velocity -= Velocity; //Subtracting instead of setting still allows external forces to affect this object if desired

                rigidBody.useGravity = true;
            }
        }
    }
}
