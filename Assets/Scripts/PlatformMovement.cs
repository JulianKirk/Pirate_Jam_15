using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxisRaw("horizontal");
        var verticalInput = Input.GetAxisRaw("vertical");

        rigidbody.velocity = new Vector3(horizontalInput, verticalInput, 0f);
    }
}
