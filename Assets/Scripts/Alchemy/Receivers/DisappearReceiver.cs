using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearReceiver : AlchemyReceiver
{
    public override void Activate(Collider other)
    {
        gameObject.SetActive(false);
    }
}
