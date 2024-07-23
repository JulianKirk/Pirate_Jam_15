using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForceReceiver : AlchemyReceiver
{
    public Vector3 Force;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void Activate(Collider other)
    {
        rigidBody.AddForce(Force);
    }
}
